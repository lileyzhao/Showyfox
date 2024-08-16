namespace Showyfx.Core;

/// <summary>
/// 基础实体抽象类，
/// 内置了 <see cref="Id"/>、<see cref="CreateTime"/>、<see cref="UpdateTime"/>、
/// <see cref="CreateUserId"/>、<see cref="CreateUserName"/>、
/// <see cref="UpdateUserId"/>、<see cref="UpdateUserName"/>、
/// <see cref="Version"/>、<see cref="IsDeleted"/> 字段定义
/// </summary>
public abstract class EntityBase : IEntityBase, IDeletedFilter
{
    /// <inheritdoc cref="IEntityBase.Id"/>
    [Key, Description("主键Id")]
    [SugarColumn(ColumnDescription = "主键Id", IsPrimaryKey = true, CreateTableFieldSort = -1)]
    public virtual long Id { get; set; }

    /// <inheritdoc cref="IEntityBase.CreateTime"/>
    [Description("创建时间")]
    [SugarColumn(ColumnDescription = "创建时间", CreateTableFieldSort = 992, IsOnlyIgnoreUpdate = true)]
    [SplitField] // 用作分表的时间字段
    public virtual DateTime CreateTime { get; set; } = DateTime.Now;

    /// <inheritdoc cref="IEntityBase.UpdateTime"/>
    [Description("更新时间")]
    [SugarColumn(ColumnDescription = "更新时间", CreateTableFieldSort = 993, IsOnlyIgnoreInsert = true)]
    public virtual DateTime UpdateTime { get; set; }

    /// <inheritdoc cref="IEntityBase.CreateUserId"/>
    [Description("创建者Id")]
    [SugarColumn(ColumnDescription = "创建者Id", CreateTableFieldSort = 994, IsOnlyIgnoreUpdate = true)]
    public virtual Guid? CreateUserId { get; set; }

    /// <inheritdoc cref="IEntityBase.CreateUserName"/>
    [MaxLength(64)]
    [Description("创建者姓名")]
    [SugarColumn(ColumnDescription = "创建者姓名", CreateTableFieldSort = 995, IsOnlyIgnoreUpdate = true)]
    public virtual string? CreateUserName { get; set; }

    /// <inheritdoc cref="IEntityBase.UpdateUserId"/>
    [Description("修改者Id")]
    [SugarColumn(ColumnDescription = "修改者Id", CreateTableFieldSort = 996, IsOnlyIgnoreInsert = true)]
    public virtual Guid? UpdateUserId { get; set; }

    /// <inheritdoc cref="IEntityBase.UpdateUserName"/>
    [Description("修改者姓名"), MaxLength(64)]
    [SugarColumn(ColumnDescription = "修改者姓名", CreateTableFieldSort = 997, IsOnlyIgnoreInsert = true)]
    public virtual string? UpdateUserName { get; set; }

    /// <inheritdoc cref="IEntityBase.Version"/>
    [Description("数据版号"), ConcurrencyCheck, JsonIgnore]
    [SugarColumn(ColumnDescription = "数据版号", CreateTableFieldSort = 998, DefaultValue = "1", IsEnableUpdateVersionValidation = true)]
    public virtual long Version { get; set; } = 1;

    /// <inheritdoc cref="IEntityBase.IsDeleted"/>
    [Description("软删除标")]
    [SugarColumn(ColumnDescription = "软删除标", DefaultValue = "0", CreateTableFieldSort = 999)]
    public virtual bool IsDeleted { get; set; } = false;
}