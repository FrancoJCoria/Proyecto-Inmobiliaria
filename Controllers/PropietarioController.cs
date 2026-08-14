using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;

namespace Inmobiliaria.Controllers;

public class PropietarioController : Controller
{
    private readonly IRepositorioPropietario _repositorio;

    public PropietarioController(IRepositorioPropietario repositorio)
    {
        _repositorio = repositorio;
    }

    //  depende del que se necesite: post, put, get
    [HttpPost]
    public IActionResult Create([FromBody] Propietario propie) 
    {
        if (propie == null)
        {
            return BadRequest("los datos del propietario son nulos");
        }

        int idGenerado = _repositorio.Alta(propie);

        // Devolver json
        return Ok(new {
            mensaje = "propietario creado",
            id = idGenerado,
            propietario = propie});
    }
}