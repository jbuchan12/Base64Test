using Navtor.ERP.API.Models;
using Navtor.ERP.DataBase.Models;
using Navtor.ERP.DataBase.Repositories;

namespace Navtor.ERP.API;

internal class Program
{
    private static readonly VesselApiService VesselApiService = new ();
    private static readonly VesselRepository VesselRepository = new ();
    
    private static async Task Main(string[] args)
    {
        if(VesselRepository.Any())
            return;
        
        List<Vessel> vessels = await GetVesselDataFromApi();
        VesselRepository.Add(vessels);
    }

    private static async Task<List<Vessel>> GetVesselDataFromApi()
    {
        List<Vessel> vessels = await VesselApiService.GetVessels(true);
        List<ProductGroup> productCodes = await VesselApiService.GetProductGroups();

        IEnumerable<Product> products = vessels
            .SelectMany(x => x.Products);

        foreach (Product product in products)
        {
            product.ProductGroup = productCodes
                .Single(x => x.Code == product.ProductGroupCode);
        }

        return vessels;
    }
}