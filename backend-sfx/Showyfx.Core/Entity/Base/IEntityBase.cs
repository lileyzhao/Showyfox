namespace Showyfx.Core;

/// <summary>
/// 基础实体接口
/// 需实现 <see cref="Id"/>、<see cref="CreateTime"/>、<see cref="UpdateTime"/>、
/// <see cref="CreateUserId"/>、<see cref="CreateUserName"/>、
/// <see cref="UpdateUserId"/>、<see cref="UpdateUserName"/>、
/// <see cref="Version"/>、<see cref="IsDeleted"/> 字段
/// </summary>
public interface IEntityBase : IEntity
{
    /// <summary>
    /// 主键Id，可使用 雪花Id 或 数据库自增
    /// </summary>
    long Id { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    DateTime UpdateTime { get; set; }

    /// <summary>
    /// 创建者Id
    /// </summary>
    Guid? CreateUserId { get; set; }

    /// <summary>
    /// 创建者姓名
    /// </summary>
    string? CreateUserName { get; set; }

    /// <summary>
    /// 修改者Id
    /// </summary>
    Guid? UpdateUserId { get; set; }

    /// <summary>
    /// 修改者姓名
    /// </summary>
    string? UpdateUserName { get; set; }

    /// <summary>
    /// 数据版号
    /// </summary>
    long Version { get; set; }

    /// <summary>
    /// 软删除标
    /// </summary>
    bool IsDeleted { get; set; }
}