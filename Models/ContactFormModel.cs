using System.ComponentModel.DataAnnotations; // Importera dataannotations för validering

namespace Moment2_MVC.Models
{
    // Modell för kontaktformuläret
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Namn är obligatoriskt")] // Validering för namn (obligatorisk)
        public string? Name { get; set; } // Property för namn

        [Required(ErrorMessage = "E-postadress är obligatoriskt")] // Validering för e-postadress (obligatorisk)
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress")] // Validering för e-postadress (av typen e-post)
        public string? Email { get; set; } // Property för e-postadress

        [Required(ErrorMessage = "Du måste ange ett meddelande")] // Validering för meddelande (obligatorisk)
        [StringLength(250, ErrorMessage = "Meddelandet får inte vara längre än 250 tecken")] // Validering för meddelande (längd)
        public string? Message { get; set; } // Property för meddelande

        [Required(ErrorMessage = "Du måste välja ett ämne")] // Validering för ämne (obligatorisk)
        public string? Subject { get; set; } // Property för ämne
    }
}
