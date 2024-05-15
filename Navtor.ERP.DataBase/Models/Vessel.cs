using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Navtor.ERP.API.Models;

namespace Navtor.ERP.DataBase.Models;

public class Vessel : IApiModel
{
    [Key]
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("companyId")]
    public int CompanyId { get; set; }
    [JsonPropertyName("imoNumber")]
    public int ImoNumber { get; set; }

    [JsonIgnore] 
    public List<Product> Products { get; set; } = [];
}

