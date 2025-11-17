using System.ComponentModel.DataAnnotations;

namespace Nordik_Aventure.Objects.Models.Finance;

public class Taxes
{
    [Required]
    public double ValueTps { get; init; }
    
    [Required]
    public double ValueTvq { get; init; }
}