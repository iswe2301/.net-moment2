using System.ComponentModel.DataAnnotations; // Importera DataAnnotations

namespace Moment2_MVC.Models
{
    // Skapa en ny modell
    public class GuessNumberModel
    {
        public int RandomNumber { get; set; } // Det slumpade numret
        public int Attempts { get; set; } // Antalet försök
        public string? Message { get; set; } // Meddelande som visas i vyn
        public bool CanPlayAgain { get; set; } // Om användaren kan spela igen
        [Required(ErrorMessage = "Du måste ange en gissning.")] // Gör fältet obligatoriskt
        [Range(1, 100, ErrorMessage = "Gissningen måste vara mellan 1 och 100.")] // Kontrollera att gissningen är mellan 1 och 100
        public int? Guess { get; set; } // Användarens gissning
    }
}