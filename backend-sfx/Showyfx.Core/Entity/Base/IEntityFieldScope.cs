namespace Showyfx.Core;

/// <summary>
/// 实体数据范围的基接口，需实现 <see cref="CreateOrgId"/>、<see cref="CreateOrgName"/> 字段
/// </summary>
public interface IEntityFieldScope
{
    /// <summary>
    /// 创建部门Id
    /// </summary>
    long CreateOrgId { get; set; }

    /// <summary>
    /// 创建部门名称
    /// </summary>
    string? CreateOrgName { get; set; }
}