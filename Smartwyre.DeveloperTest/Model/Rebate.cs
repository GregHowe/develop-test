using Smartwyre.DeveloperTest.Types;
using System.ComponentModel.DataAnnotations;

namespace Smartwyre.DeveloperTest.Model;

public class Rebate
{
    [Key]
    public string Identifier { get; set; }
    public IncentiveType Incentive { get; set; }
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }
}
