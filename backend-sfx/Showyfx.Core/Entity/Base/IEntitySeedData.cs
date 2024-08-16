namespace Showyfx.Core;

/// <summary>
/// 实体种子数据接口
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IEntitySeedData<out TEntity> : IEntitySeedData
    where TEntity : class, IEntity, new()
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    IEnumerable<TEntity> HasData();
}

/// <summary>
/// 实体种子数据基接口
/// </summary>
public interface IEntitySeedData;