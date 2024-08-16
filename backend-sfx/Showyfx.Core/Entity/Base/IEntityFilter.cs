namespace Showyfx.Core;

/// <summary>
/// 软删除过滤器接口
/// </summary>
public interface IDeletedFilter
{
    /// <summary>
    /// 软删除标
    /// </summary>
    bool IsDeleted { get; set; }
}

/// <summary>
/// 租户过滤器接口
/// </summary>
public interface ITenantFilter
{
    /// <summary>
    /// 租户Id
    /// </summary>
    long TenantId { get; set; }
}

/// <summary>
/// 数据范围过滤器接口
/// </summary>
public interface IScopeFilter
{
    /// <summary>
    /// 创建部门Id
    /// </summary>
    long CreateOrgId { get; set; }
}