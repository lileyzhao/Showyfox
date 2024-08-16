namespace Showyfx.Core;

/// <summary>
/// 系统缓存服务
/// </summary>
public class SfxCacheService(ICache cache, IOptions<CacheOptions> cacheOptions) : IDynamicApiController, ISingleton
{
    private readonly CacheOptions _cacheOptions = cacheOptions.Value;

    /// <summary>
    /// 获取缓存键名集合
    /// </summary>
    /// <returns></returns>
    public List<string> GetKeyList()
    {
        return cache == Cache.Default
            ? cache.Keys.Where(q => q.StartsWith(_cacheOptions.Prefix!)).Select(q => q[_cacheOptions.Prefix!.Length..]).OrderBy(q => q).ToList()
            : ((FullRedis)cache).Search($"{_cacheOptions.Prefix}*", int.MaxValue).Select(q => q[_cacheOptions.Prefix!.Length..]).OrderBy(q => q).ToList();
    }

    /// <summary>
    /// 增加缓存
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    [NonAction]
    public bool Set(string key, object? value)
    {
        return cache.Set($"{_cacheOptions.Prefix}{key}", value);
    }

    /// <summary>
    /// 增加缓存并设置过期时间
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expire"></param>
    /// <returns></returns>
    [NonAction]
    public bool Set(string key, object? value, TimeSpan expire)
    {
        return cache.Set($"{_cacheOptions.Prefix}{key}", value, expire);
    }

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    [NonAction]
    public T? Get<T>(string key)
    {
        return cache.Get<T>($"{_cacheOptions.Prefix}{key}");
    }

    /// <summary>
    /// 删除缓存
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public int Remove(string key)
    {
        return cache.Remove($"{_cacheOptions.Prefix}{key}");
    }

    /// <summary>
    /// 检查缓存是否存在
    /// </summary>
    /// <param name="key">键</param>
    /// <returns></returns>
    [NonAction]
    public bool ExistKey(string key)
    {
        return cache.ContainsKey($"{_cacheOptions.Prefix}{key}");
    }

    /// <summary>
    /// 根据键名前缀删除缓存
    /// </summary>
    /// <param name="prefixKey">键名前缀</param>
    /// <returns></returns>
    [HttpPost("DeleteByPreKey")]
    public int RemoveByPrefixKey(string prefixKey)
    {
        var delKeys = cache == Cache.Default
            ? cache.Keys.Where(u => u.StartsWith($"{_cacheOptions.Prefix}{prefixKey}")).ToArray()
            : ((FullRedis)cache).Search($"{_cacheOptions.Prefix}{prefixKey}*", int.MaxValue).ToArray();

        return cache.Remove(delKeys);
    }

    /// <summary>
    /// 根据键名前缀获取键名集合
    /// </summary>
    /// <param name="prefixKey">键名前缀</param>
    /// <returns></returns>
    public List<string> GetKeysByPrefixKey(string prefixKey)
    {
        return cache == Cache.Default
            ? cache.Keys.Where(u => u.StartsWith($"{_cacheOptions.Prefix}{prefixKey}")).Select(u => u[_cacheOptions.Prefix!.Length..]).ToList()
            : ((FullRedis)cache).Search($"{_cacheOptions.Prefix}{prefixKey}*", int.MaxValue).Select(u => u[_cacheOptions.Prefix!.Length..]).ToList();
    }

    /// <summary>
    /// 获取缓存值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public object? GetValue(string key)
    {
        return cache == Cache.Default
            ? cache.Get<object>($"{_cacheOptions.Prefix}{key}")
            : cache.Get<string>($"{_cacheOptions.Prefix}{key}");
    }

    /// <summary>
    /// 获取或添加缓存，在数据不存在时执行委托请求数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    /// <param name="expire">过期时间，单位秒</param>
    /// <returns></returns>
    [NonAction]
    public T? GetOrAdd<T>(string key, Func<string, T> callback, int expire = -1)
    {
        return cache.GetOrAdd($"{_cacheOptions.Prefix}{key}", callback, expire);
    }
}