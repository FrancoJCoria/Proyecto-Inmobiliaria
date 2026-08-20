using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;

namespace Inmobiliaria.Controllers;

public class InquilinoController : Controller
{
    public readonly IRepositorioInquilino _repositorio;

    public InquilinoController(IRepositorioInquilino repositorio)
    {
        _repositorio = repositorio;
    }

    [HttpPost]
    public IActionResult Create([FromBody] Inquilino inquilino)
    {
        if(inquilino == null)
        {
            return BadRequest("Los datos del inquilino son nulos");
        }
        int idGenerado = _repositorio.Alta(inquilino);
        return Ok(new
        {
            mensaje = "Inquilino creado",
            id = idGenerado,
            inquilino = inquilino
        });
    }

    [HttpPatch]
    public IActionResult Delete([FromBody] Inquilino inquilino)
    {
        if(inquilino == null || string.IsNullOrEmpty(inquilino.Dni))
        {
            return BadRequest(new { error = "Se requiere el DNI para dar de baja al inquilino." });
        }
        int filasAfectadas = _repositorio.Baja(inquilino);
        if(filasAfectadas == 0)
        {
            return NotFound(new { error = $"No se encontró ningún inquilino con el DNI {inquilino.Dni}." });
        }
        return Ok(new
        {
            mensaje = "Inquilino dado de baja con éxito",
            filasAfectadas = filasAfectadas
        });
    }

    [HttpPut]
    public IActionResult Edit([FromBody] Inquilino inquilino)
    {
        if(inquilino == null)
        {
            return BadRequest(new { error = "Se requiere un inquilino válido con un ID mayor a cero para la modificación." });
        }
        int filasAfectadas = _repositorio.Modificacion(inquilino);
        if(filasAfectadas == 0)
        {
            return NotFound(new { error = $"No se encontró ningún inquilino con el ID {inquilino.Id_inquilino}." });
        }
        return Ok(new
        {
            mensaje = "Inquilino modificado con éxito",
            filasAfectadas = filasAfectadas
        });
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var inquilinos = _repositorio.ObtenerTodos();
        return Ok(inquilinos);
    }
}

