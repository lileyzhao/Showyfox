namespace Showyfx.Core;

/// <summary>
/// 实体更多字段的基接口，需实现 <see cref="Remark"/>、<see cref="OrderNo"/>、<see cref="ExtJson"/> 字段
/// </summary>
public interface IEntityFieldMore
{
    /// <summary>
    /// 备注
    /// </summary>
    string? Remark { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    int OrderNo { get; set; }

    /// <summary>
    /// 扩展信息，必须Json格式
    /// </summary>
    string? ExtJson { get; set; }
}