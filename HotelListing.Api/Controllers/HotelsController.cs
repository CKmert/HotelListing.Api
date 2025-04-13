// Ein Namespace in C# ist eine Art von Container, der dazu dient, Klassen, Interfaces, Strukturen und andere Typen zu organisieren und zu gruppieren.
// Man kann sich einen Namespace wie einen Ordner auf deinem Computer vorstellen, in dem du verschiedene Dateien ablegst

using HotelListing.Api.Data; // Importiert den Namespace, der die Hotel-Klasse und evtl. weitere Datenmodelle enthält
using Microsoft.AspNetCore.Mvc; // Bietet Funktionen zur Erstellung von Web API-Controllern, Routing, HTTP-Attribute usw.

namespace HotelListing.Api.Controllers // Organisiert den Code logisch unter dem Namespace "HotelListing.Api.Controllers"
{
    [Route("api/[controller]")] // Bestimmt die Basisroute des Controllers. [controller] wird durch den Klassennamen ersetzt, hier "Hotels" => api/hotels
    [ApiController] // Markiert diese Klasse als Web API-Controller (fügt automatisches Model Binding, Validierung, usw. hinzu)
    public class HotelsController : ControllerBase // Der Controller erbt von ControllerBase, was grundlegende Funktionen für APIs bereitstellt (ohne MVC-spezifische Views)
    {
        // In-Memory Liste von Hotels (anstelle einer Datenbank). Wird zur Laufzeit erstellt und verwaltet.
        private static List<Hotel> hotels = new List<Hotel>
        {
            new Hotel { Id = 1, Name = "Grand Plaza", Address = "123 Main St", Rating = 3.4},
            new Hotel { Id = 2, Name = "Ocean View", Address = "321 Sec St", Rating = 4.4} // Korrigierte doppelte Id
        };

        // GET: api/hotels
        [HttpGet] // Kennzeichnet diese Methode als GET-Endpunkt (wird bei HTTP-GET-Anfragen aufgerufen)
        public ActionResult<IEnumerable<Hotel>> Get() // Gibt eine Liste aller Hotels zurück
        {
            return Ok(hotels); // Gibt HTTP-Statuscode 200 (OK) mit der Liste der Hotels zurück
        }

        // GET: api/hotels/{id}
        [HttpGet("{id}")] // GET-Endpunkt mit URL-Parameter id
        public ActionResult<Hotel> Get(int id) // Gibt ein einzelnes Hotel mit der angegebenen Id zurück
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == id); // Sucht das erste Hotel mit passender Id
            if (hotel == null)
            {
                return NotFound(); // Gibt HTTP-Statuscode 404 zurück, wenn kein Hotel gefunden wurde
            }
            return Ok(hotel); // Gibt HTTP-Statuscode 200 mit dem gefundenen Hotel zurück
        }

        // POST: api/hotels
        [HttpPost] // Kennzeichnet diese Methode als POST-Endpunkt (zum Erstellen von neuen Einträgen)
        public ActionResult<Hotel> Post([FromBody] Hotel newHotel) // Nimmt ein Hotel-Objekt aus dem Body der Anfrage entgegen
        {
            if (hotels.Any(h => h.Id == newHotel.Id)) // Prüft, ob bereits ein Hotel mit der gleichen Id existiert
            {
                return BadRequest("Hotel with this Id already exists."); // Gibt Fehlerstatus 400 zurück
            }

            hotels.Add(newHotel); // Fügt das neue Hotel zur Liste hinzu
            return CreatedAtAction(nameof(Get), new { id = newHotel.Id }, newHotel); // Gibt Status 201 (Created) mit Ort des neuen Eintrags zurück
        }

        // PUT: api/hotels/{id}
        [HttpPut("{id}")] // PUT-Endpunkt zum Aktualisieren eines Eintrags
        public ActionResult Put(int id, [FromBody] Hotel updatedHotel)
        {
            var existingHotel = hotels.FirstOrDefault(h => h.Id == id); // Sucht das zu aktualisierende Hotel
            if (existingHotel == null)
            {
                return NotFound(); // Falls nicht gefunden => 404
            }

            // Aktualisiere die Eigenschaften des Hotels
            existingHotel.Name = updatedHotel.Name;
            existingHotel.Address = updatedHotel.Address;
            existingHotel.Rating = updatedHotel.Rating;
            return NoContent(); // Gibt 204 zurück (Update erfolgreich, aber keine Rückgabe nötig)
        }

        // DELETE: api/hotels/{id}
        [HttpDelete("{id}")] // DELETE-Endpunkt
        public ActionResult Delete(int id)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == id); // Sucht das Hotel
            if (hotel == null)
            {
                return NotFound(new { message = "Hotel not found" }); // Falls nicht gefunden => 404 mit Nachricht
            }

            hotels.Remove(hotel); // Entfernt das Hotel aus der Liste
            return NoContent(); // 204 No Content bei erfolgreichem Löschen
        }
    }
}
