using Microsoft.AspNetCore.Mvc;

namespace Moment2_MVC.Controllers
{
    // Skapa en ny kontroller
    public class SiteController : Controller
    {
        private static Random random = new Random(); // Instans av Random
        private static int randomNumber = random.Next(1, 101); // Slumpad siffra mellan 1 och 100

        // Skapa en ny action
        public IActionResult Start()
        {
            return View(); // Returnera vy
        }

        // Action för att gissa nummer
        [HttpGet]
        public IActionResult GuessNumber()
        {
            ViewBag.Message = "Gissa ett nummer mellan 1 och 100"; // Skapa meddelande
            ViewBag.CanPlayAgain = false; // Dölj knappen för att spela igen
            return View(); // Returnera vy
        }

        // Action för att gissa nummer
        [HttpPost]
        public IActionResult GuessNumber(int guess)
        {
            // Kontrollera om gissningen är rätt och ge feedback
            if (guess == randomNumber)
            {
                ViewBag.Message = "Grattis! Du gissade rätt!";
                ViewBag.CanPlayAgain = true; // Visa knappen för att spela igen
            }
            // Kontrollera om gissningen är mindre än det slumpade talet och ge feedback
            else if (guess < randomNumber)
            {
                ViewBag.Message = "Fel! Talet är större än " + guess + ". Försök igen.";
                ViewBag.CanPlayAgain = false; // Dölj knappen för att spela igen

            }
            // Kontrollera om gissningen är större än det slumpade talet och ge feedback
            else
            {
                ViewBag.Message = "Fel! Talet är mindre än " + guess + ". Försök igen.";
                ViewBag.CanPlayAgain = false; // Dölj knappen för att spela igen
            }

            return View(); // Returnera vy
        }

        // Action för att spela igen
        [HttpPost]
        public IActionResult PlayAgain()
        {
            randomNumber = random.Next(1, 101); // Generera ett nytt nummer
            ViewBag.Message = "Gissa ett nummer mellan 1 och 100!"; // Återställ meddelandet
            ViewBag.CanPlayAgain = false; // Dölj knappen för att spela igen
            return View("GuessNumber"); // Returnera vy
        }
    }
}