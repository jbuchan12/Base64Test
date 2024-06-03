using Navtor.ERP.API.Models;
using Navtor.ERP.DataBase.Models;
using Newtonsoft.Json;

namespace Navtor.ERP.API;

internal class VesselApiService : NavtorApiService
{
    public async Task<List<Vessel>> GetVessels(bool includeProducts = false)
    {
        string data = await HttpGetAsync("vessels");
        List<Vessel> vessels = JsonToObjects<Vessel>(data);

        if (!includeProducts) 
            return vessels;

        IEnumerable<Task> tasks = vessels
            .Select(async vessel =>
        {
            vessel.Products = await GetProductsForVessel(vessel.Id);
        });

        await Task.WhenAll(tasks);

        return vessels;
    }

    public async Task<List<Product>> GetProductsForVessel(int vesselId)
    {
        string data = await HttpGetAsync($"vessels/{vesselId}/products");
        return JsonToObjects<Product>(data);
    }

    public async Task<List<ProductGroup>> GetProductGroups()
    {
        string data = await HttpGetAsync("product-groups");
        return JsonToObjects<ProductGroup>(data);
    }

    public async Task<List<Product>> GetProducts()
    {
        string data = await HttpGetAsync("products");
        return JsonToObjects<Product>(data);
    }
}