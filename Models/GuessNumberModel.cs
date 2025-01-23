namespace Moment2_MVC.Models
{
    // Skapa en ny modell
    public class GuessNumberModel
    {
        public int RandomNumber { get; set; } // Det slumpade numret
        public int Attempts { get; set; } // Antalet försök
        public string? Message { get; set; } // Meddelande som visas i vyn
        public bool CanPlayAgain { get; set; } // Om användaren kan spela igen
    }
}