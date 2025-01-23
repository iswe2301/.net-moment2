using Microsoft.AspNetCore.Mvc;

namespace Moment2_MVC.Controllers
{
    // Skapa en ny kontroller
    public class SiteController : Controller
    {
        private static Random random = new Random(); // Instans av Random

        // Skapa en ny action
        public IActionResult Start()
        {
            return View(); // Returnera vy
        }

        // Action för att gissa nummer
        [HttpGet]
        public IActionResult GuessNumber()
        {
            // Kontrollera om cookien med det slumpade numret finns
            if (!HttpContext.Request.Cookies.ContainsKey("RandomNumber"))
            {
                // Skapa ett slumpat nummer och spara i cookien
                int randomNumber = random.Next(1, 101);
                HttpContext.Response.Cookies.Append("RandomNumber", randomNumber.ToString());

                // Spara antalet försök i cookien och sätt till 0 som default
                HttpContext.Response.Cookies.Append("Attempts", "0");

            }
            ViewBag.Message = "Gissa ett nummer mellan 1 och 100"; // Skapa meddelande
            ViewBag.CanPlayAgain = false; // Dölj knappen för att spela igen
            return View(); // Returnera vy
        }

        // Action för att gissa nummer
        [HttpPost]
        public IActionResult GuessNumber(int guess)
        {
            // Hämta slumpnumret samt antalet försök från cookien
            string? randomNumberCookie = HttpContext.Request.Cookies["RandomNumber"];
            string? attemptsCookie = HttpContext.Request.Cookies["Attempts"];

            // Kontrollera om cookiesen saknas
            if (string.IsNullOrEmpty(randomNumberCookie) || string.IsNullOrEmpty(attemptsCookie))
            {
                // Om cookies saknas, ge ett felmeddelande
                ViewBag.Message = "Ett fel inträffade. Starta om spelet.";
                ViewBag.CanPlayAgain = true; // Visa knappen för att spela igen
                return View(); // Returnera vy
            }

            // Konvertera slumpnumret från sträng till heltal
            int randomNumber = int.Parse(randomNumberCookie);
            int attempts = int.Parse(attemptsCookie);

            // Öka antalet försök med 1 och spara i cookien
            attempts++;
            HttpContext.Response.Cookies.Append("Attempts", attempts.ToString());

            // Kontrollera om gissningen är rätt och ge feedback
            if (guess == randomNumber)
            {
                // Visa meddelande beroende på antalet försök
                ViewBag.Message = attempts == 1 ? $"Wow! Du gissade rätt på första försöket!" : $"Grattis! Du gissade rätt på {attempts} försök!";
                ViewBag.CanPlayAgain = true; // Visa knappen för att spela igen
                HttpContext.Response.Cookies.Delete("RandomNumber"); // Ta bort cookien för numret
                HttpContext.Response.Cookies.Delete("Attempts"); // Ta bort cookien för antal försök
            }
            // Kontrollera om gissningen är mindre än det slumpade talet och ge feedback
            else if (guess < randomNumber)
            {
                ViewBag.Message = $"Fel! Talet är större än {guess}. Försök igen.";
                ViewBag.CanPlayAgain = false; // Dölj knappen för att spela igen

            }
            // Kontrollera om gissningen är större än det slumpade talet och ge feedback
            else
            {
                ViewBag.Message = $"Fel! Talet är mindre än {guess}. Försök igen.";
                ViewBag.CanPlayAgain = false; // Dölj knappen för att spela igen
            }

            return View(); // Returnera vy
        }

        // Action för att spela igen
        [HttpPost]
        public IActionResult PlayAgain()
        {
            int randomNumber = random.Next(1, 101); // Skapa ett nytt slumpat nummer
            HttpContext.Response.Cookies.Append("RandomNumber", randomNumber.ToString()); // Spara numret i cookien
            HttpContext.Response.Cookies.Append("Attempts", "0"); // Spara antalet försök i cookien och sätt till 0 som default
            ViewBag.Message = "Gissa ett nummer mellan 1 och 100!"; // Återställ meddelandet
            ViewBag.CanPlayAgain = false; // Dölj knappen för att spela igen
            return View("GuessNumber"); // Returnera vy
        }
    }
}