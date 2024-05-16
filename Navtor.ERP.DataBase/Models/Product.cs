using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Navtor.ERP.API.Models;

namespace Navtor.ERP.DataBase.Models;
public class Product : IApiModel
{
    [Key]
    public Guid Id { get; set; }
    public int VesselId { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public string ProductGroupCode { get; set; } = string.Empty;
    public string ProductTypeCode { get; set; } = string.Empty;
    public DateTime EditionDate { get; set; }

    [JsonIgnore] 
    public ProductGroup ProductGroup { get; set; } = null!;
}


