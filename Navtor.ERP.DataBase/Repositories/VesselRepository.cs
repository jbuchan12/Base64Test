using Navtor.ERP.API.Models;
using Navtor.ERP.DataBase.Models;

namespace Navtor.ERP.DataBase.Repositories
{
    public class VesselRepository(NavtorErpDbContext? testContext = null) : Repository<Vessel>(testContext);
}
