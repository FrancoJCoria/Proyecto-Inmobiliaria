using MySqlConnector;
using Microsoft.Extensions.Configuration;

namespace Inmobiliaria.Models;


public class RepositorioInquilino : RepositorioBase
{
    public RepositorioInquilino(IConfiguration configuration) : base(configuration) { }

    public int Alta(Inquilino inquilino)
    {
        int idGenerado = 0;
        using var connection = new MySqlConnection(connectionString);
        string consultaSql = @"INSERT INTO Inquilino (dni, nombre, apellido, telefono, email, estado)
        VALUES (@dni, @nombre, @apellido, @telefono, @email, @estado);
        SELECT LAST_INSERT_ID();";
        using var command = new MySqlCommand(consultaSql, connection);
        BindParams(command, inquilino);
        connection.Open();
        idGenerado = Convert.ToInt32(command.ExecuteScalar());
        inquilino.Id = idGenerado;
        return idGenerado;
    }

    public int Baja(Inquilino inquilino)
    {
        int filasAfectadas = 0;
        using var connection = new MySqlConnection(connectionString);
        string consultaSql = @"UPDATE Inquilino SET estado = @estado WHERE dni = @dni";
        using var command = new MySqlCommand(consultaSql, connection);
        BindId(command, inquilino);
        command.Parameters.AddWithValue("@estado", inquilino.Estado);
        command.Parameters.AddWithValue("@dni", inquilino.Dni);
        connection.Open();
        filasAfectadas = command.ExecuteNonQuery();
        return filasAfectadas;
    }

    public int Modificacion(Inquilino inquilino)
    {
        int filasAfectadas = 0;
        using var connection = new MySqlConnection(connectionString);
        string consultaSql = @"UPDATE Inquilino SET dni = @dni, nombre = @nombre, apellido = @apellido, telefono = @telefono, email = @email, estado = @estado WHERE id = @id";
        using var command = new MySqlCommand(consultaSql, connection);
        BindParams(command, inquilino);
        BindId(command, inquilino);
        connection.Open();
        filasAfectadas = command.ExecuteNonQuery();
        return filasAfectadas;
    }


    private static void BindId(MySqlCommand cmd, Inquilino inquilino)
    {
        cmd.Parameters.AddWithValue("@id", inquilino.Id);
    }

    private static void BindParams(MySqlCommand cmd, Inquilino inquilino)
    {
        cmd.Parameters.AddWithValue("@dni", inquilino.Dni);
        cmd.Parameters.AddWithValue("@nombre", inquilino.Nombre);
        cmd.Parameters.AddWithValue("@apellido", inquilino.Apellido);
        cmd.Parameters.AddWithValue("@telefono", inquilino.Telefono);
        cmd.Parameters.AddWithValue("@email", inquilino.Email);
        cmd.Parameters.AddWithValue("@estado", inquilino.Estado);
    }
}