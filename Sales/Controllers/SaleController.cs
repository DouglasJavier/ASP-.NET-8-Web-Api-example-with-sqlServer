using Backend.DTOs;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SaleController : ControllerBase
{
    private readonly ICommonService<SaleGetDto, SaleInsertUpdateDto, SaleInsertUpdateDto> _saleService;
    private IValidator<SaleInsertUpdateDto> _saleInsertUpdateValidator;
    // Cambiar el parámetro al tipo de la interfaz ICommonService
    public SaleController(
        ICommonService<SaleGetDto, SaleInsertUpdateDto, SaleInsertUpdateDto> saleService,
        IValidator<SaleInsertUpdateDto> validatorInsertUpdate
        )
    {
        _saleService = saleService;
        _saleInsertUpdateValidator = validatorInsertUpdate;
    }
    [HttpGet]
    public async Task<IEnumerable<SaleGetDto>> Get()
    {
        return await _saleService.Get();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SaleGetDto>> GetById(int id)
    {
        return await _saleService.GetById(id);
    }

    [HttpPost]
    public async Task<ActionResult<SaleGetDto>> Post([FromBody] SaleInsertUpdateDto saleDto)
    {
        var validationResult = await _saleInsertUpdateValidator.ValidateAsync(saleDto);
        if (!validationResult.IsValid)
        {
            Console.WriteLine("Entro aqui");
            Console.WriteLine(validationResult.Errors);
            return BadRequest(validationResult.Errors);
        }
        try
        {
            // Intentar agregar la venta
            var result = await _saleService.Add(saleDto);

            // Verificar si hay errores relacionados con el stock
            if (_saleService.Errors.Any())
            {
                // Devolver los errores de stock en la respuesta de BadRequest
                return BadRequest(new { Message = "Stock validation failed", Errors = _saleService.Errors });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            // Manejar excepciones generales y devolver un InternalServerError
            return StatusCode(500, new { Message = "An error occurred while processing the request", Details = ex.Message });
        }
    }

    /* [HttpPost]
    public async Task<ActionResult<SaleGetDto>> Post(SaleInsertDto saleDto)
    {
        var validationResult = await _saleInsertValidator.ValidateAsync(saleDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var result = await _saleService.Add(saleDto);
        return Ok(result);
    } */
}