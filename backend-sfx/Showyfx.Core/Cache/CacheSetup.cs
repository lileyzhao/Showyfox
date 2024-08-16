namespace Showyfx.Core;

/// <summary>
/// 缓存注册（NewLife.Redis组件）
/// </summary>
public static class CacheSetup
{
    /// <inheritdoc cref="CacheSetup"/>
    public static void AddCache(this IServiceCollection services)
    {
        var cache = Cache.Default;

        var cacheOptions = App.GetConfig<CacheOptions>("Cache", true);
        if (cacheOptions.CacheType == CacheTypeEnum.Redis.ToString() && cacheOptions.RedisConfig != null)
        {
            cache = new FullRedis(new RedisOptions
            {
                Configuration = cacheOptions.RedisConfig.Configuration,
                Prefix = cacheOptions.RedisConfig.Prefix
            });

            ((FullRedis)cache).MaxMessageSize = cacheOptions.RedisConfig.MaxMessageSize > 0 ? cacheOptions.RedisConfig.MaxMessageSize : 1024 * 1024;
        }

        services.AddSingleton(cache);
    }
}