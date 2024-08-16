namespace Showyfx.Core;

/// <summary>
/// 实体租户的基接口，需实现 <see cref="TenantId"/> 字段
/// </summary>
public interface IEntityFieldTenant
{
    /// <summary>
    /// 租户Id
    /// </summary>
    long TenantId { get; set; }
}