namespace Showyfx.Core;

/// <summary>
/// SqlSugar 实体仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public class SqlSugarRepository<T> : SimpleClient<T> where T : class, new()
{
    // protected ITenant iTenant = null;
    //
    // public SqlSugarRepository()
    // {
    //     iTenant = App.GetRequiredService<SqlSugarScope>().AsTenant();
    //     base.Context = iTenant.GetConnectionScope(DbConst.MainConfigId);
    //
    //     // 若实体贴有多库特性，则返回指定库连接
    //     if (typeof(T).IsDefined(typeof(TenantAttribute), false))
    //     {
    //         base.Context = iTenant.GetConnectionScopeWithAttr<T>();
    //         return;
    //     }
    //
    //     // 若实体贴有日志表特性，则返回日志库连接
    //     if (typeof(T).IsDefined(typeof(LogTableAttribute), false))
    //     {
    //         base.Context = iTenant.IsAnyConnection(DbConst.LogConfigId)
    //             ? iTenant.GetConnectionScope(DbConst.LogConfigId)
    //             : iTenant.GetConnectionScope(DbConst.MainConfigId);
    //         return;
    //     }
    //
    //     // 若实体贴有系统表特性，则返回默认库连接
    //     if (typeof(T).IsDefined(typeof(SysTableAttribute), false))
    //     {
    //         base.Context = iTenant.GetConnectionScope(DbConst.MainConfigId);
    //         return;
    //     }
    //
    //     // 若未贴任何表特性或当前未登录或是默认租户Id，则返回默认库连接
    //     var tenantId = App.User?.FindFirst(ClaimConst.TenantId)?.Value;
    //     if (string.IsNullOrWhiteSpace(tenantId) || tenantId == DbConst.MainConfigId) return;
    //
    //     // 根据租户Id切换库连接, 为空则返回默认库连接
    //     var sqlSugarScopeProvider = App.GetRequiredService<SysTenantService>().GetTenantDbConnectionScope(long.Parse(tenantId));
    //     if (sqlSugarScopeProvider == null) return;
    //
    //     base.Context = sqlSugarScopeProvider;
    // }
}