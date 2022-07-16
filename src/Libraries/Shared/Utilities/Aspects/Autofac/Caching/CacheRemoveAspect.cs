using Castle.DynamicProxy;
using Shared.CrossCuttingConcerns.Caching;
using Shared.Utilities.Interceptors;

namespace Shared.Utilities.Aspects.Autofac.Caching;

public class CacheRemoveAspect:MethodInterception
{
    private readonly ICacheManager _cacheManager;
    private readonly string _pattern;
    public CacheRemoveAspect(string pattern)
    {
        _cacheManager = (ICacheManager)ServiceTool.ServiceProvider.GetService(typeof(ICacheManager));
        _pattern = pattern;
    }

    protected override void OnSuccess(IInvocation invocation)
    {
        _cacheManager.RemoveCacheByPattern(_pattern);
    }
}