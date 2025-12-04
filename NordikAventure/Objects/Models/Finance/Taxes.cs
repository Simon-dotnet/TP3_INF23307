using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordik_Aventure.Objects.Models.Finance;

public class Taxes
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaxesId { get; set; }
    
    [Required]
    public double ValueTps { get; set; }
    
    [Required]
    public double ValueTvq { get; set; }
}