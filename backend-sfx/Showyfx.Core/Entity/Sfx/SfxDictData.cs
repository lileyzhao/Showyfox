namespace Showyfx.Core;

/// <summary>
/// 字典值表
/// </summary>
[SfxTable]
public class SfxDictData : EntityBaseMore
{
    /// <summary>
    /// 字典类型Id
    /// </summary>
    public long TypeId { get; set; }

    /// <summary>
    /// 字典类型
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(TypeId))]
    public SfxDictType? RefDictType { get; set; }

    /// <summary>
    /// 字典值
    /// </summary>
    [Required, StringLength(128)]
    public string? Value { get; set; }

    /// <summary>
    /// 值编码
    /// </summary>
    [Required, MaxLength(64)]
    public string? Code { get; set; }

    /// <summary>
    /// 显示样式-标签颜色
    /// </summary>
    [StringLength(16)]
    public string? TagType { get; set; }

    /// <summary>
    /// 显示样式-Style(控制显示样式)
    /// </summary>
    [StringLength(512)]
    public string? StyleSetting { get; set; }

    /// <summary>
    /// 显示样式-Class(控制显示样式)
    /// </summary>
    [StringLength(512)]
    public string? ClassSetting { get; set; }

    /// <summary>
    /// 启停状态
    /// </summary>
    [SugarColumn(ColumnDescription = "启停状态")]
    public EnabledStatusEnum Status { get; set; } = EnabledStatusEnum.Enabled;
}