using System.ComponentModel.DataAnnotations;
using Navtor.ERP.API.Models;

namespace Navtor.ERP.DataBase.Models;

public class ProductGroup : IApiModel
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}