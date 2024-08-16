namespace Showyfx.Core;

/// <summary>
/// 含数据范围的租户基础实体抽象类，
/// 继承 <see cref="EntityTenant"/> 并增加了 <see cref="CreateOrgId"/>、<see cref="CreateOrgName"/> 字段定义
/// </summary>
public abstract class EntityTenantScope : EntityTenant, IEntityFieldScope, IScopeFilter
{
    /// <inheritdoc cref="IEntityFieldScope.CreateOrgId"/>
    [Description("创建部门Id")]
    [SugarColumn(ColumnDescription = "创建部门Id", CreateTableFieldSort = 981, DefaultValue = "-1", IsOnlyIgnoreUpdate = true)]
    public virtual long CreateOrgId { get; set; } = -1;

    /// <inheritdoc cref="IEntityFieldScope.CreateOrgName"/>
    [SugarColumn(ColumnDescription = "创建部门名称", CreateTableFieldSort = 982, Length = 64, IsOnlyIgnoreUpdate = true)]
    public virtual string? CreateOrgName { get; set; }
}