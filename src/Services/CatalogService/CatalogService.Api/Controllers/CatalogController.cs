using CatalogService.Application.ViewModels;
using CatalogService.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CatalogService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator mediator;

        public CatalogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<CatalogItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ItemsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            return Ok(await mediator.Send(new GetItemsQueryRequest(new PaginateSettings(pageIndex, pageSize))));
        }


        // GET api/v1/[controller]/items/1
        [HttpGet]
        [Route("items/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CatalogItem>> ItemByIdAsync(int id)
        {
            var result = await mediator.Send(new GetItemByIdQueryRequest(id));
            if (result is null)
                NotFound();
            return Ok(result);
        }

        // GET api/v1/[controller]/items/withname/samplename[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items/withname/{name:minlength(1)}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<CatalogItem>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<CatalogItem>>> ItemsWithNameAsync(string name,
            [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await mediator.Send(new GetItemsByNameQueryRequest(name, new PaginateSettings(pageIndex, pageSize)));
            return Ok(result);
        }

        // GET api/v1/[controller]/items/type/1/brand[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items/type/{catalogTypeId}/brand/{catalogBrandId:int?}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<CatalogItem>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<CatalogItem>>> ItemsByTypeIdAndBrandIdAsync(
            int catalogTypeId, int? catalogBrandId, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await mediator.Send(new GetItemsByTypeAndBrandQueryRequest(catalogTypeId, catalogBrandId,
                new PaginateSettings(pageIndex, pageSize)));

            return Ok(result);
        }

        // GET api/v1/[controller]/items/type/all/brand[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items/type/all/brand/{catalogBrandId:int?}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<CatalogItem>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<CatalogItem>>> ItemsByBrandIdAsync(int? catalogBrandId,
            [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await mediator.Send(new GetItemsByBrandQueryRequest(catalogBrandId,new PaginateSettings(pageIndex,pageSize)));
            return Ok(result);
        }

        // GET api/v1/[controller]/CatalogTypes
        [HttpGet]
        [Route("catalogtypes")]
        [ProducesResponseType(typeof(List<CatalogType>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<CatalogType>>> CatalogTypesAsync()
        {
            return await mediator.Send(new GetCatalogTypesQueryRequest());
        }

        // GET api/v1/[controller]/CatalogBrands
        [HttpGet]
        [Route("catalogbrands")]
        [ProducesResponseType(typeof(List<CatalogBrand>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<CatalogBrand>>> CatalogBrandsAsync()
        {
            return await mediator.Send(new GetCatalogBrandsQueryRequest());
        }
    }
}