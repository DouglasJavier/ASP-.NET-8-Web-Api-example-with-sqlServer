using Backend.DTOs;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    private readonly ICommonService<BrandGetDto, BrandInsertDto, BrandUpdateDto> _brandService;
    private IValidator<BrandInsertDto> _brandInsertValidator;
    private IValidator<BrandUpdateDto> _brandUpdateValidator;

    // Cambiar el parámetro al tipo de la interfaz ICommonService
    public BrandController(
        ICommonService<BrandGetDto, BrandInsertDto, BrandUpdateDto> brandService,
        IValidator<BrandInsertDto> validatorInsert,
        IValidator<BrandUpdateDto> validatorUpdate
        )
    {
        _brandService = brandService;
        _brandInsertValidator = validatorInsert;
        _brandUpdateValidator = validatorUpdate;
    }
    [HttpGet]
    public async Task<IEnumerable<BrandGetDto>> Get()
    {
        return await _brandService.Get();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BrandGetDto>> GetById(int id)
    {
        return await _brandService.GetById(id);
    }

    [HttpPost]
    public async Task<ActionResult<BrandGetDto>> Post(BrandInsertDto brandDto)
    {
        var validationResult = await _brandInsertValidator.ValidateAsync(brandDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var result = await _brandService.Add(brandDto);
        return Ok(result);
    }

}