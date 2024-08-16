namespace Showyfx.Core;

/// <summary>
/// 字典类型表种子数据
/// </summary>
public class SfxDictTypeSeedData : IEntitySeedData<SfxDictType>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public IEnumerable<SfxDictType> HasData()
    {
        // @formatter:wrap_lines false
        return new[]
        {
            new SfxDictType { Id = 188_000_000_001, Name = "代码生成控件类型", Code = "code_gen_effect_type", OrderNo = 100, Remark = "代码生成控件类型", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictType { Id = 188_000_000_002, Name = "代码生成查询类型", Code = "code_gen_query_type", OrderNo = 101, Remark = "代码生成查询类型", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictType { Id = 188_000_000_003, Name = "代码生成.NET类型", Code = "code_gen_net_type", OrderNo = 102, Remark = "代码生成.NET类型", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictType { Id = 188_000_000_004, Name = "代码生成方式", Code = "code_gen_create_type", OrderNo = 103, Remark = "代码生成方式", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictType { Id = 188_000_000_005, Name = "代码生成基类", Code = "code_gen_base_class", OrderNo = 104, Remark = "代码生成基类", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictType { Id = 188_000_000_006, Name = "机构类型", Code = "org_type", OrderNo = 105, Remark = "机构类型", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime }
        };
    }
}