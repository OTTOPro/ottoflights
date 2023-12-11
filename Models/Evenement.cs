using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OttoFlights.Models;

public class Evenement
{
    public int EvenementId { get; set; }

    [Required(ErrorMessage = "La date revisée est obligatoire.")]
    [DataType(DataType.Date)]
    public DateTime DateRevisée { get; set; }

    [ForeignKey("Vol")]
    public int VolId { get; set; }
    public Vol? Vol { get; set; }

    [Required(ErrorMessage = "L'état du vol est obligatoire.")]
    [StringLength(50, ErrorMessage = "Le statut de l'evenement ne peut pas dépasser 50 caractères.")]
    public string? Statut { get; set; }

}
