using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Showyfx.Core;
using SqlSugar;

namespace Showyfx.Application;

/// <summary>
/// 
/// </summary>
public class TestService(SqlSugarScope db) : IDynamicApiController, ITransient
{
    /// <summary>
    /// GetAll测试 SfxUser
    /// </summary>
    public void GetAll()
    {
        db.Queryable<SfxUser>().ToList();
    }
}