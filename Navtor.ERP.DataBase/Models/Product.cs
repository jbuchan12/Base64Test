using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Navtor.ERP.DataBase.Models;
public class Product
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


