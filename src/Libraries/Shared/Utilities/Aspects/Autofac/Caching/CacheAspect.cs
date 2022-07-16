using Castle.DynamicProxy;
using Shared.CrossCuttingConcerns.Caching;
using Shared.Utilities.Interceptors;

namespace Shared.Utilities.Aspects.Autofac.Caching;

public class CacheAspect : MethodInterception
{
    int _duration;
    ICacheManager _cacheManager;

    public CacheAspect(int duration = 60)
    {
        _cacheManager = (ICacheManager)ServiceTool.ServiceProvider.GetService(typeof(ICacheManager));
        _duration = duration;
    }

    public override void Intercept(IInvocation invocation)
    {
        if (invocation.Method.ReflectedType != null)
        {
            var methodName = $"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}";
            var arg = invocation.Arguments.ToList();

            var key = $"{methodName}({string.Join(",",arg.Select(a => a?.ToString()??"<Null>"))})";

            if (_cacheManager.IsAddedCache(key))
            {
                invocation.ReturnValue = _cacheManager.GetCache(key);
                return;
            }

            invocation.Proceed();
            _cacheManager.AddCache(key, invocation.ReturnValue, _duration);
        }
    }
}