using System.Security;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.Common.Messages;
using Shared.Extensions;
using Shared.Utilities.Interceptors;

namespace Shared.Utilities.Aspects.Autofac.Authorization;

public class SecuredOperation : MethodInterception
{
    private readonly string[] _roles;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public SecuredOperation(string roles)
    {
        _roles = roles.Split(",");
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }

    protected override void OnBefore(IInvocation invocation)
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            List<string> claimRoles = _httpContextAccessor.HttpContext.User.ClaimRoles();

            foreach (string role in _roles)
            {
                if(claimRoles.Contains(role))
                {
                    return;
                }
            }
        }

        throw new SecurityException(Messages.AuthorizationDenied);
    }
}