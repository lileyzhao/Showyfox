namespace Showyfx.Core;

/// <summary>
/// 含数据范围和更多字段的租户基础实体抽象类，
/// 继承 <see cref="EntityTenant"/> 并增加了 <see cref="Remark"/>、<see cref="OrderNo"/>、<see cref="ExtJson"/> 字段定义
/// </summary>
public abstract class EntityTenantScopeMore : EntityTenantScope, IEntityFieldMore
{
    /// <inheritdoc cref="IEntityFieldMore.Remark"/>
    [StringLength(512)]
    public virtual string? Remark { get; set; }

    /// <inheritdoc cref="IEntityFieldMore.OrderNo"/>
    [DefaultValue(100)]
    public virtual int OrderNo { get; set; } = 100;

    /// <inheritdoc cref="IEntityFieldMore.ExtJson"/>
    [SugarColumn(ColumnDataType = StaticConfig.CodeFirst_BigString, IsJson = true)]
    public virtual string? ExtJson { get; set; }
}