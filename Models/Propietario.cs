using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Models;

[Table("Propietario")]
public class Propietario{
    
    [Key]
    [Column("id_propietario")]
    public int IdPropietario { get; set; }

    [Column("dni")]
    public string Dni { get; set; } = string.Empty;

    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("apellido")]
    public string Apellido { get; set; } = string.Empty;

    [Column("telefono")]
    public string Telefono { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("estado")]
    public bool Estado { get; set; }
}