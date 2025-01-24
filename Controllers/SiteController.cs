using Microsoft.AspNetCore.Mvc;
using Moment2_MVC.Models; // Importera modellen

namespace Moment2_MVC.Controllers
{
    // Skapa en ny kontroller
    public class SiteController : Controller
    {
        private static Random random = new Random(); // Instans av Random

        // Skapa en ny action
        public IActionResult Start()
        {
            ViewData["Title"] = "Start"; // Skapa en titel
            ViewBag.Welcome = "Välkommen till gissningsspelet!"; // Skapa en välkomsthälsning
            ViewBag.Message = "Börja spela"; // Skapa ett meddelande
            return View(); // Returnera vy
        }

        // Metod för att skapa en instans av GuessNumberModel
        private GuessNumberModel CreateModel()
        {
            // Hämta cookies
            string? randomNumberCookie = HttpContext.Request.Cookies["RandomNumber"];
            string? attemptsCookie = HttpContext.Request.Cookies["Attempts"];
            string? messageCookie = HttpContext.Request.Cookies["Message"];

            // Kontrollera om cookiesen saknas
            if (string.IsNullOrEmpty(randomNumberCookie) || string.IsNullOrEmpty(attemptsCookie))
            {
                // Om cookies saknas, skapa nya värden
                int randomNumber = random.Next(1, 101);
                HttpContext.Response.Cookies.Append("RandomNumber", randomNumber.ToString());
                HttpContext.Response.Cookies.Append("Attempts", "0");
                HttpContext.Response.Cookies.Append("Message", "Gissa ett nummer mellan 1 och 100!");

                // Skapa en ny instans av GuessNumberModel med värdena och returnera modellen
                return new GuessNumberModel
                {
                    RandomNumber = randomNumber,
                    Attempts = 0,
                    Guess = null,
                    Message = "Gissa ett nummer mellan 1 och 100!",
                    CanPlayAgain = false
                };
            }

            // Skapa en ny instans av GuessNumberModel med värden från cookiesen och returnera modellen
            return new GuessNumberModel
            {
                RandomNumber = int.Parse(randomNumberCookie),
                Attempts = int.Parse(attemptsCookie),
                Guess = null,
                Message = string.IsNullOrEmpty(messageCookie) ? "Gissa ett nummer mellan 1 och 100!" : messageCookie, // Om meddelandet är tomt, sätt default-värdet
                CanPlayAgain = false
            };
        }

        // Action för att gissa nummer
        [HttpGet]
        [Route("spela")] // Ändra URL till /spela
        public IActionResult GuessNumber()
        {
            GuessNumberModel model = CreateModel(); // Hämta modellen från CreateModel-metoden
            return View(model); // Returnera vy med modellen
        }

        // Action för att gissa nummer
        [HttpPost]
        [Route("spela")] // Ändra URL till /spela
        public IActionResult GuessNumber(GuessNumberModel model)
        {
            // Kontrollera om modellen är ogiltig
            if (!ModelState.IsValid)
            {
                GuessNumberModel invalidGuess = CreateModel(); // Hämta modellen från CreateModel-metoden
                invalidGuess.Message = "Ogiltig gissning! Försök igen."; // Visa felmeddelande
                return View(invalidGuess); // Returnera vy med modellen
            }

            GuessNumberModel currentGuess = CreateModel(); // Hämta modellen från CreateModel-metoden
            currentGuess.Guess = model.Guess; // Sätt gissningen till användarens gissning

            // Öka antalet försök med 1 och spara i cookien
            currentGuess.Attempts++;
            HttpContext.Response.Cookies.Append("Attempts", currentGuess.Attempts.ToString());

            // Kontrollera om gissningen är rätt och ge feedback
            if (currentGuess.Guess == currentGuess.RandomNumber)
            {
                // Visa meddelande beroende på antalet försök
                currentGuess.Message = currentGuess.Attempts == 1 ? $"Wow! Du gissade rätt på första försöket!" : $"Grattis! Du gissade rätt på {currentGuess.Attempts} försök!";
                currentGuess.CanPlayAgain = true; // Visa knappen för att spela igen

                // Ta bort cookies när spelet är klart
                HttpContext.Response.Cookies.Delete("RandomNumber");
                HttpContext.Response.Cookies.Delete("Attempts");
                HttpContext.Response.Cookies.Delete("Message");
            }
            // Kontrollera om gissningen är mindre än det slumpade talet och ge feedback
            else if (currentGuess.Guess < currentGuess.RandomNumber)
            {
                currentGuess.Message = $"Fel! Talet är större än {currentGuess.Guess}. Försök igen.";
            }
            // Kontrollera om gissningen är större än det slumpade talet och ge feedback
            else
            {
                currentGuess.Message = $"Fel! Talet är mindre än {currentGuess.Guess}. Försök igen.";
            }

            HttpContext.Response.Cookies.Append("Message", currentGuess.Message); // Spara det nya meddelandet i cookien

            return View(currentGuess); // Returnera vy med gissningen
        }

        // Action för att spela igen
        [HttpPost]
        [Route("spela-igen")] // Ändra URL till /spela-igen
        public IActionResult PlayAgain()
        {
            int randomNumber = random.Next(1, 101); // Skapa ett nytt slumpat nummer
            HttpContext.Response.Cookies.Append("RandomNumber", randomNumber.ToString()); // Spara numret i cookien
            HttpContext.Response.Cookies.Append("Attempts", "0"); // Spara antalet försök i cookien och sätt till 0 som default

            // Hämta modellen från CreateModel-metoden
            GuessNumberModel model = CreateModel();

            return View("GuessNumber", model); // Returnera vy med modellen
        }
    }
}