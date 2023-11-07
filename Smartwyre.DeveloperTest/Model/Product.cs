using Smartwyre.DeveloperTest.Types;
using System.ComponentModel.DataAnnotations;

namespace Smartwyre.DeveloperTest.Model;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Identifier { get; set; }
    public decimal Price { get; set; }
    public string Uom { get; set; }
    public SupportedIncentiveType SupportedIncentives { get; set; }
}
