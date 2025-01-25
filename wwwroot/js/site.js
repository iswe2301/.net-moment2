"use strict";

// Funktion för att visa/dölja menyn
function toggleMenu() {
    const menu = document.querySelector("nav ul"); // Hämtar ul-elementet i nav
    menu.classList.toggle("active"); // Lägger till eller tar bort klassen "active" på ul-elementet
}
