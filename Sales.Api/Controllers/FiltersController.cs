using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Sales.Api.Models.Responses;
using Sales.Data.Repositories;

namespace Sales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FiltersController: Controller
    {
        private readonly IUnitOfWork _unit;

        public FiltersController(IUnitOfWork unit)
        {
            _unit = unit;
        }
        
        [HttpGet]
        public IActionResult GetAllFilters()
        {
            var aggregate = new FiltersAggregateResponse
            {
                Countries = _unit.CountriesRepository.GetAll().OrderBy(item => item.Name).Select(item => item.Name),
                ItemTypes = _unit.ItemTypesRepository.GetAll().OrderBy(item => item.Name).Select(item => item.Name),
                OrderPriorities = _unit.OrderPrioritiesRepository.GetAll().OrderBy(item => item.Name).Select(item => item.Name),
                Regions = _unit.RegionsRepository.GetAll().OrderBy(item => item.Name).Select(item => item.Name),
                RegionsCountries = _unit.RegionsCountriesRepository.GetAll().OrderBy(item => item.Region).ThenBy(item => item.Country).Select(item => $"{ item.Region }: { item.Country }"),
                SalesChannels = _unit.SalesChannelsRepository.GetAll().OrderBy(item => item.Name).Select(item => item.Name)
            };

            return Ok(aggregate);
        }
    }
}