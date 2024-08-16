namespace Showyfx.Core;

/// <summary>
/// 租户基础实体抽象类，
/// 继承 <see cref="EntityBase"/> 并增加了 <see cref="TenantId"/> 字段定义
/// </summary>
public abstract class EntityTenant : EntityBase, IEntityFieldTenant, ITenantFilter
{
    /// <inheritdoc cref="IEntityFieldTenant.TenantId"/>
    [Required(ErrorMessage = "缺少必须的租户Id"), Description("租户Id")]
    [SugarColumn(ColumnDescription = "租户Id", CreateTableFieldSort = 991, DefaultValue = "-1", IsOnlyIgnoreUpdate = true)]
    public virtual long TenantId { get; set; } = -1;
}