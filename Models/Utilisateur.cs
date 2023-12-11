using System.ComponentModel.DataAnnotations;

namespace OttoFlights.Models;

public class Utilisateur
{
    public int Id { get; set; }

    [Display(Name = "Email Utilisateur")]
    [Required(ErrorMessage = "L'email est requis.")]
    [EmailAddress(ErrorMessage = "L'email n'est pas dans un format valide.")]
    public string? Email { get; set; }

    [Display(Name = "Mot de Passe")]
    [Required(ErrorMessage = "Le mot de passe est requis.")]
    [DataType(DataType.Password)]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Le mot de passe doit avoir entre 8 et 20 caractères.")]
    public string? MotDePasse { get; set; }

    public string? Role { get; set; }
}

