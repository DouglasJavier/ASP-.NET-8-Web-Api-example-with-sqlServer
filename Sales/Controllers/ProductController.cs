using Backend.DTOs;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ICommonService<ProductGetDto, ProductInsertDto, ProductUpdateDto> _productService;
    private IValidator<ProductInsertDto> _productInsertValidator;
    private IValidator<ProductUpdateDto> _productUpdateValidator;

    // Cambiar el parámetro al tipo de la interfaz ICommonService
    public ProductController(
        ICommonService<ProductGetDto, ProductInsertDto, ProductUpdateDto> productService,
        IValidator<ProductInsertDto> validatorInsert,
        IValidator<ProductUpdateDto> validatorUpdate
        )
    {
        _productService = productService;
        _productInsertValidator = validatorInsert;
        _productUpdateValidator = validatorUpdate;
    }
    [HttpGet]
    public async Task<IEnumerable<ProductGetDto>> Get()
    {
        return await _productService.Get();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductGetDto>> GetById(int id)
    {
        return await _productService.GetById(id);
    }

    [HttpPost]
    public async Task<ActionResult<ProductGetDto>> Post(ProductInsertDto productDto)
    {
        var validationResult = await _productInsertValidator.ValidateAsync(productDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var result = await _productService.Add(productDto);
        return Ok(result);
    }

    /* [HttpPost]
    public async Task<ActionResult<ProductGetDto>> Post(ProductInsertDto productDto)
    {
        var validationResult = await _productInsertValidator.ValidateAsync(productDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var result = await _productService.Add(productDto);
        return Ok(result);
    } */
}