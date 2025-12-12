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

    // In-memory sample product store for demo purposes.
    private static readonly List<ProductDto> _products = new List<ProductDto>
    {
        new()
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "USB-C Charger",
            ImgUri = "https://example.com/imgs/charger.jpg",
            Price = 19.99m,
            Description = "Fast 30W USB-C charger."
        },
        new()
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Name = "Wireless Mouse",
            ImgUri = "https://example.com/imgs/mouse.jpg",
            Price = 29.50m,
            Description = "Ergonomic wireless mouse with long battery life."
        },
        new()
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Name = "Mechanical Keyboard",
            ImgUri = "https://example.com/imgs/keyboard.jpg",
            Price = 89.00m,
            Description = "Compact mechanical keyboard with RGB."
        }
    };

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
