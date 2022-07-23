# Projektuppgift

Ni kommer att jobba tillsammans i grupper om 3-4. Varje person i gruppen ska bidra med commits.
Om ni är 3 i gruppen så kan t.ex. varje person utveckla var sin frontend del, och lägga till ett test för den backend hjälpmetod som används.
Om ni är 4 så sikta på att varje person skriver ett test var och lägg kanske till lite extra features på en frontend, eller lägg till ett loginsystem som en eller flera frontends kan använda sig av.

## Scenario

Ni i teamet är inga fullstack utvecklare ännu, men ni vet hur man bygger en redig backend med EFCore.
Allt som saknas nu är en frontend så att man kan använda tjänsten, och en testsuite
så att ni kan lita på att er backend faktiskt fungerar!

Ni kommer att använda ett av era DataLayer projekt bygga tre klient appar ovanpå (tre projekt). Vi har inte gått igenom HTML/CSS ännu så i stället så kan ni använda konsollen för det visuella.

<img src="img\12341234.png"></img>

Samma gäller som tidigare. Det finns tre olika typer av användare som vill interagera med våran tjänst.

1. Kunden som loggar in på vår app och väljer en matlåda att köpa ur en sorterad lista av
   möjliga alternativ. Denne användare är vår primära inkomstkälla.
2. Servitör på resturangen. Denne lägger dagligen till nya matlådor som blivit över
   under resturangens namn.
3. Adminisitrativ personal som vi själva anställer. Dessa sköter tilläggning av nya
   resturanger som vi slutit kontrakt med.

Varje typ av användare behöver sin egna frontend app, eller klient. Men alla apparna
kommer att ansluta till och interagera med samma databas.

## Uppdrag

Utgå ifrån någon av era lösningar från inlämningsuppgift 2 och lägg till tre nya
(kanske Console App) projekt. Ett för varje frontend program.

### Klienter

Nedan är de tre frontend klienter som behöver implementeras.

1. "CustomerClient" där kunden genom input/UI kan:
   - titta på en lista på alla oköpta matlådor i alla restauranger av en viss typ (kött/fisk/vego), sorterade på pris, lägst först
   - välja att köpa en av de lunchlådor som visas
   - välja att se de matlådor denne köpt hittils
2. "RestaurantClient" där en servitör genom input/UI kan:
   - titta på alla sålda matlådor för dennes restaurang
   - titta på alla osålda matlådor för dennes restaurang
   - knappa in info för, och lägga till en ny matlåda för dennes restaurang
3. "AdminClient" där man genom input/UI kan:
   - resetta databasen
   - titta på alla användare
   - titta på alla resturanger
   - knappa in info för, och lägga till en ny restaurang

Frontendklienterna ska använda backendprojektet för sin funktionalitet. Man får ändra på backenden vid behov.
Frontendklienterna ska inte behöva använda DbContext eller ens känna till att det finns en databas med i bilden.
De innehåller bara logik för att interagera med användaren och kallar på hjälpmetoder i backend.
(Be om inloggning, hålla koll på och visa resultat, komma ihåg vad användaren gjort för val, etc.)

Det är ok att bygga varje frontend klient som något annat än en Console App. Rekommendationer är WinForms/Wpf eller Asp.Net Core.

### Testning

Det ska finnas en testsuite med tester som testar backend metoderna. Minst ett test skrivet per person.

Tänk på att varje test ska vara oberoende. Om två tester använder samma databas så är de inte oberoende. Data får inte ligga kvar och skräpa i databasen när vi kör våra tester. Detta innebär oftast att databasen behöver rensas och sen seedas om innan varje test kör.

### Inloggning (valfritt)

Om ni är 4 i gruppen så skulle den fjärde personen kunna sitta med detta. Man kan ju skriva ett test för att inloggning ska fungera med.

Bygg gärna något inloggningssystem via databasen om ni har tid över.
Användarnamn och lösen kan lagras för alla användare i customertabellen till exempel.
