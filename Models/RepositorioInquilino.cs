using System.Data.Common;
using MySqlConnector;

namespace Proyecto_Inmobiliaria.Models;

public class RepositorioInquilino(MySqlDataSource database)
{
    public async Task AltaAsync(Inquilino inquilino)
    {
        await using var connection = await database.OpenConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Inquilino (Dni, Nombre, Apellido, Telefono, Email, Estado) VALUES (@Dni, @Nombre, @Apellido, @Telefono, @Email, @Estado)";
        BindParams(command, inquilino);
        await command.ExecuteNonQueryAsync();
        inquilino.Id = (int)command.LastInsertedId;
    }

    public async Task ModificacionAsync(Inquilino inquilino)
    {
        await using var connection = await database.OpenConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = "UPDATE Inquilino SET Dni = @Dni, Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono, Email = @Email, Estado = @Estado WHERE Id = @Id";
        BindParams(command, inquilino);
        BindId(command, inquilino);
        await command.ExecuteNonQueryAsync();
    }

    public async Task BajaAsync(int id)
    {
        await using var connection  = await database.OpenConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Inquilino WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        await command.ExecuteNonQueryAsync();
    }
    
    private static void BindId(MySqlCommand cmd, Inquilino inquilino)
    {
        cmd.Parameters.AddWithValue("@id", inquilino.Id);
    }

    private static void BindParams(MySqlCommand cmd, Inquilino inquilino)
    {
        cmd.Parameters.AddWithValue("@Dni", inquilino.Dni);
        cmd.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
        cmd.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
        cmd.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
        cmd.Parameters.AddWithValue("@Email", inquilino.Email);
        cmd.Parameters.AddWithValue("@Estado", inquilino.Estado);
    }
}

