using Navtor.ERP.API.Models;
using Navtor.ERP.DataBase.Models;

namespace Navtor.ERP.DataBase.Repositories
{
    public class ProductRepository(NavtorErpDbContext? testContext = null) : Repository<Product>(testContext);
}
