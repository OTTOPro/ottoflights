
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OttoFlights.Models;

public class Vol
{
    public int VolId { get; set; }

    [Required(ErrorMessage = "La date prévue est obligatoire.")]
    [DataType(DataType.Date)]
    public DateTime DatePrevue { get; set; }

    [Required(ErrorMessage = "La date révisée est obligatoire.")]
    [DataType(DataType.Date)]
    public DateTime? DateRevue { get; set; }

    [Required(ErrorMessage = "Le numéro de vol est obligatoire.")]
    [StringLength(50, ErrorMessage = "Le numéro de vol ne peut pas dépasser 50 caractères.")]
    public string? NumeroVol { get; set; }

    [Required(ErrorMessage = "L'agence est obligatoire.")]
    [StringLength(100, ErrorMessage = "Le nom de l'agence ne peut pas dépasser 100 caractères.")]
    public string? Agence { get; set; }

    [Required(ErrorMessage = "La provenance est obligatoire.")]
    [StringLength(100, ErrorMessage = "La provenance ne peut pas dépasser 100 caractères.")]
    public string? Provenance { get; set; }

    [Required(ErrorMessage = "L'état du vol est obligatoire.")]
    [StringLength(50, ErrorMessage = "L'état du vol ne peut pas dépasser 50 caractères.")]
    public string? Etat { get; set; }

    public ICollection<Evenement>? Evenements { get; set; }
}