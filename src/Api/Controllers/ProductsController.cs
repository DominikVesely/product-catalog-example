using Api.Contracts.Requests;
using Api.Controllers.Base;
using Asp.Versioning;
using Business.Dto;
using Business.Services;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers;

[ApiVersion(1.0)]
public class ProductsController : ApiController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    // v1: return all available products (no pagination)
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAllV1()
    {
        List<ProductDto> data = await _productService.GetAll(HttpContext.RequestAborted);
        return Ok(data);
    }

    [HttpGet]
    [ApiVersion(2.0)]
    public async Task<ActionResult<List<ProductDto>>> GetAllV2([FromQuery][Required] PaginationDto pagination)
    {
        var data = await _productService.GetAll(pagination, HttpContext.RequestAborted);
        return Ok(data);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        ProductDto? product = await _productService.GetById(id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPatch]
    [Route("{id:guid}/description")]
    public async Task<ActionResult<ProductDto>> UpdateDescription(Guid id, [FromBody][Required] UpdateProductDescriptionRequest request)
    {
        var product = await _productService.UpdateDescription(id, request.Description, HttpContext.RequestAborted);

        return Ok(product);
    }


}
