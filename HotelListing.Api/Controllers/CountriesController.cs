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
using HotelListing.Api.Contracts;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;

        public CountriesController(IMapper mapper, ICountriesRepository countriesRepository )
        {
            this._mapper = mapper;
            this._countriesRepository = countriesRepository;
        }

        // GET: api/Countries // Das Attribut, das diese Methode als GET-Endpunkt markiert
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetCountries() // IEnumerable = Sammlung Datentyp für mehrer Objekte
        {
            // SELECT * from Countries
            var countries = await _countriesRepository.GetAllAsync(); // RÜckgabe wird in countries gespeichert
            var records = _mapper.Map<List<GetCountryDTO>>(countries);
            return Ok(records); // Erstellt Rückgabewert für countries 200 Code
        }

        // GET: api/Countries/5 = Methode: Endpunkt basierend auf die gewünschte ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDTO>> GetCountry(int id) // Action gibt hier nur ein Rückgabewert 1 Objeklt raus
        {
            var country = await _countriesRepository.GetDetails(id);

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

            var country = await _countriesRepository.GetAllAsync(id); // Suche nach der mitgeschickten ID (Argument)
            if (country == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCountryDTO, country); // Update die zu änderten Daten von updateCountryDTO und füge sie in das zu ändernde country Datensatz

            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
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

            await _countriesRepository.AddAsync();       // DBContext: wird oben über DI initialisiert - gehe zu Countries Tabelle und füge das Land hinzu

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}
