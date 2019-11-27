using System;
using Microsoft.AspNetCore.Mvc;
using Sales.Api.Models.Requests;
using Sales.Api.Models.Responses;
using Sales.Data.Entities;
using Sales.Data.Repositories;
using System.Linq.Expressions;
using System.Linq;

namespace Sales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : Controller
    {
        private readonly IUnitOfWork _unit;
        private readonly int _pageSize = 10;

        public SalesController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet("paginated")]
        public IActionResult GetPaginated([FromQuery]int page, [FromQuery]SearchRequest searchRequest = null, [FromQuery]OrderRequest orderRequest = null)
        {
            Expression<Func<Sale, bool>> searchCriteria = s => true;
            if (searchRequest != null)
            {
                searchCriteria = searchRequest.ComposeSearch();
            }

            var totalSales = _unit.SalesRepository.CountTotal(searchCriteria);
            var totalPages = (int)Math.Ceiling((decimal)(totalSales / _pageSize));

            Func<IQueryable<Sale>, IOrderedQueryable<Sale>> orderCriteria = null;
            if (orderRequest != null)
            {
                orderCriteria = orderRequest.ComposeOrdering();
            }

            var sales = _unit.SalesRepository.Get(
                filter: searchCriteria, 
                orderBy: orderCriteria, 
                skip: page * _pageSize, 
                take: _pageSize
            );            
            var paginatedResponse = new PaginatedSalesResponse
            {
                CurrentPage = page,
                TotalPages = totalPages,
                PageContent = sales
            };

            return Ok(paginatedResponse);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var sale = _unit.SalesRepository.GetById(id);
            if (sale != null)
            {
                return Ok(sale);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create(Sale newSale)
        {
            newSale.TotalRevenue = newSale.UnitPrice * newSale.UnitsSold;
            newSale.TotalCost = newSale.UnitPrice * newSale.UnitCost;
            newSale.TotalProfit = newSale.TotalRevenue - newSale.TotalCost;
            _unit.SalesRepository.Create(newSale);
            _unit.Complete();

            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Sale sale)
        {
            _unit.SalesRepository.Update(sale);
            _unit.Complete();

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            _unit.SalesRepository.Delete(id);
            _unit.Complete();

            return NoContent();
        }

        protected override void Dispose(bool disposing)
        {
            _unit.Dispose();
            base.Dispose(disposing);
        }
    }
}