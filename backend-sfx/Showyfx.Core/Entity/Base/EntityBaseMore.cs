namespace Showyfx.Core;

/// <summary>
/// 含更多字段的基础实体抽象类，
/// 继承 <see cref="EntityBase"/> 并增加了 <see cref="Remark"/>、<see cref="OrderNo"/>、<see cref="ExtJson"/> 字段定义
/// </summary>
public abstract class EntityBaseMore : EntityBase, IEntityFieldMore
{
    /// <inheritdoc cref="IEntityFieldMore.Remark"/>
    [SugarColumn(CreateTableFieldSort = 961, Length = 512)]
    public virtual string? Remark { get; set; }

    /// <inheritdoc cref="IEntityFieldMore.OrderNo"/>
    [DefaultValue(100)]
    [SugarColumn(CreateTableFieldSort = 961, DefaultValue = "100")]
    public virtual int OrderNo { get; set; } = 100;

    /// <inheritdoc cref="IEntityFieldMore.ExtJson"/>
    [SugarColumn(ColumnDataType = StaticConfig.CodeFirst_BigString, IsJson = true)]
    public virtual string? ExtJson { get; set; }
}