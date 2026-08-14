using MySqlConnector;
using Microsoft.Extensions.Configuration;

namespace Inmobiliaria.Models;

public class RepositorioPropietario : RepositorioBase, IRepositorioPropietario
{
    public RepositorioPropietario(IConfiguration configuration) : base(configuration) { }

    public int Alta(Propietario propietarioParams)
    {
        int idGenerado = 0;
        using var conexion = new MySqlConnection(connectionString);
        
        string consultaSql = @"INSERT INTO Propietario (dni, nombre, apellido, telefono, email, estado)
        VALUES (@dni, @nombre, @apellido, @telefono, @email, @estado);
        SELECT LAST_INSERT_ID();"; 

        using var comando = new MySqlCommand(consultaSql, conexion);
        comando.Parameters.AddWithValue("@dni", propietarioParams.Dni);
        comando.Parameters.AddWithValue("@nombre", propietarioParams.Nombre);
        comando.Parameters.AddWithValue("@apellido", propietarioParams.Apellido);
        comando.Parameters.AddWithValue("@telefono", propietarioParams.Telefono);
        comando.Parameters.AddWithValue("@email", propietarioParams.Email);
        comando.Parameters.AddWithValue("@estado", propietarioParams.Estado);

        conexion.Open();
        idGenerado = Convert.ToInt32(comando.ExecuteScalar());
        propietarioParams.IdPropietario = idGenerado;

        return idGenerado;
    }
}