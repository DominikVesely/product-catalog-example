using Api.Classes;
using Api.Contracts.Requests;
using Api.Controllers.Base;
using Asp.Versioning;
using Business.Dto;
using Business.Services;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers;

/// <summary>
/// Defines a controller that provides API endpoints for retrieving and updating product information.
/// </summary>
[ApiVersion(1.0)]
public class ProductsController : ApiController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    /// <summary>
    /// Retrieves a list of all products.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAllV1()
    {
        List<ProductDto> data = await _productService.GetAll(HttpContext.RequestAborted);
        return Ok(data);
    }

    /// <summary>
    /// Retrieves a paginated list of products using API version 2.0.
    /// </summary>
    [HttpGet]
    [ApiVersion(2.0)]
    public async Task<ActionResult<List<ProductDto>>> GetAllV2([FromQuery][Required] PaginationDto pagination)
    {
        var data = await _productService.GetAll(pagination, HttpContext.RequestAborted);
        return Ok(data);
    }

    /// <summary>
    /// Retrieves the product with the specified unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to retrieve.</param>
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DefaultProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        ProductDto? product = await _productService.GetById(id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    /// <summary>
    /// Updates the description of the specified product.
    /// </summary>
    /// <param name="id">The unique identifier of the product to update.</param>
    /// <param name="request">The request containing the new product description. Cannot be null.</param>
    [HttpPatch]
    [Route("{id:guid}/description")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DefaultProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> UpdateDescription(Guid id, [FromBody][Required] UpdateProductDescriptionRequest request)
    {
        var product = await _productService.UpdateDescription(id, request.Description, HttpContext.RequestAborted);

        return Ok(product);
    }


}
