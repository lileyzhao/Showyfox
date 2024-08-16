namespace Showyfx.Core;

/// <summary>
/// SqlSugar二级缓存
/// </summary>
public class SqlSugarCache : ICacheService
{
    // 系统缓存服务
    private static readonly SfxCacheService Cache = App.GetService<SfxCacheService>();

    /// <summary>
    /// 添加缓存
    /// </summary>
    /// <param name="key">缓存的Key</param>
    /// <param name="value">缓存的值</param>
    /// <typeparam name="TV">缓存值的类型</typeparam>
    public void Add<TV>(string key, TV value) => Cache.Set($"{CacheConst.SqlSugarKeyPrefix}{key}", value);

    /// <summary>
    /// 添加缓存
    /// </summary>
    /// <param name="key">缓存的Key</param>
    /// <param name="value">缓存的值</param>
    /// <param name="cacheDurationInSeconds">缓存过期时间，单位 秒</param>
    /// <typeparam name="TV">缓存值的类型</typeparam>
    public void Add<TV>(string key, TV value, int cacheDurationInSeconds)
    {
        Cache.Set($"{CacheConst.SqlSugarKeyPrefix}{key}", value, TimeSpan.FromSeconds(cacheDurationInSeconds));
    }

    /// <summary>
    /// 判断指定Key的缓存是否存在
    /// </summary>
    /// <param name="key">缓存的Key</param>
    /// <typeparam name="TV">缓存值的类型</typeparam>
    /// <returns></returns>
    public bool ContainsKey<TV>(string key) => Cache.ExistKey($"{CacheConst.SqlSugarKeyPrefix}{key}");

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <param name="key">缓存的Key</param>
    /// <typeparam name="TV">缓存值的类型</typeparam>
    /// <returns></returns>
    public TV? Get<TV>(string key) => Cache.Get<TV>($"{CacheConst.SqlSugarKeyPrefix}{key}");

    /// <summary>
    /// 获取所有缓存的Key
    /// </summary>
    /// <typeparam name="TV">缓存值的类型</typeparam>
    /// <returns></returns>
    public IEnumerable<string> GetAllKey<TV>() => Cache.GetKeysByPrefixKey(CacheConst.SqlSugarKeyPrefix);

    /// <summary>
    /// 获取缓存，如果不存在则添加缓存
    /// </summary>
    /// <param name="key">缓存的Key</param>
    /// <param name="create">添加缓存的函数体</param>
    /// <param name="cacheDurationInSeconds">缓存过期时间，单位 秒，默认 int.MaxValue</param>
    /// <typeparam name="TV">缓存值的类型</typeparam>
    /// <returns></returns>
    public TV? GetOrCreate<TV>(string key, Func<TV> create, int cacheDurationInSeconds = int.MaxValue)
    {
        return Cache.GetOrAdd<TV>($"{CacheConst.SqlSugarKeyPrefix}{key}", _ => create(), cacheDurationInSeconds);
    }

    /// <summary>
    /// 删除指定Key缓存
    /// </summary>
    /// <param name="key">缓存的Key</param>
    /// <typeparam name="TV">缓存值的类型</typeparam>
    public void Remove<TV>(string key) => Cache.Remove(key);
}