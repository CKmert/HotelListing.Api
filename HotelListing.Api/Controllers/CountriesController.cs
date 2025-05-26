using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Api.Data;
using HotelListing.Api.Models.Country;
using HotelListing.Api.Configurations;
using AutoMapper;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly HotelListlingDbContext _context;
        private readonly IMapper _mapper;

        public CountriesController(HotelListlingDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Countries // Das Attribut, das diese Methode als GET-Endpunkt markiert
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetCountries() // IEnumerable = Sammlung Datentyp für mehrer Objekte
        {
            // SELECT * from Countries
            var countries = await _context.Countries.ToListAsync(); // RÜckgabe wird in countries gespeichert
            var records = _mapper.Map<List<GetCountryDTO>>(countries);
            return Ok(records); // Erstellt Rückgabewert für countries 200 Code
        }

        // GET: api/Countries/5 = Methode: Endpunkt basierend auf die gewünschte ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDTO>> GetCountry(int id) // Action gibt hier nur ein Rückgabewert 1 Objeklt raus
        {
            var country = await _context.Countries.Include(q => q.Hotels) // Gehe in die Datenbank -> Tabele Countries -> füge die Liste mit Hotels der Countries
                .FirstOrDefaultAsync(q => q.Id == id);   // suche mir nach der Id und gebe mir diesen aus

            if (country == null)
            {
                return NotFound();
            }

            var countryDto = _mapper.Map<CountryDTO>(country); // mapping zum Datentyp CountryDTO 

            return Ok(country);
        }

        // PUT: api/Countries/5 Diese Methode wird ausgeführt, wenn ein HTTP PUT mit einer id kommt
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO updateCountryDTO)
        {
            if (id != updateCountryDTO.Id)  // Passt die ID in der URL zu der ID im mitgeschickten Objekt?
            {
                return BadRequest("Invalid Record ID");
            }

            //_context.Entry(country).State = EntityState.Modified;  // Der State ändert sich deswegen Modified (update) 

            var country = await _context.Countries.FindAsync(id); // Suche nach der mitgeschickten ID (Argument)
            if (country == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCountryDTO, country); // Update die zu änderten Daten von updateCountryDTO und füge sie in das zu ändernde country Datensatz

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        [HttpPost]  // Das Attribut, das diese Methode als POST-Endpunkt markiert

        //  Asynchrone Methode, die ein ActionResult mit Country-Objekt zurückgibt (Rückgabetyp)
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDTO createCountryDto) // Das zu erstellende DTO mit 2 Props, automatisch aus dem Request-Body / Parameter deserialisiert vom Typ Country
        {

            var country = _mapper.Map<Country>(createCountryDto); // DTO in Countrydateityp Mappen

            _context.Countries.Add(country);       // DBContext: wird oben über DI initialisiert - gehe zu Countries Tabelle und füge das Land hinzu
            await _context.SaveChangesAsync();     // Speichert die Änderungen in der Datenbank

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
