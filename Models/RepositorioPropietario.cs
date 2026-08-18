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

    public int Baja(Propietario propietarioParams)
    {
        int filasAfectadas = 0;
        
        using var conexion = new MySqlConnection(connectionString);
        
        string consultaSql = @"UPDATE Propietario 
        SET estado = @estado WHERE dni = @dni";
        
        using var comando = new MySqlCommand(consultaSql, conexion);
        comando.Parameters.AddWithValue("@estado", propietarioParams.Estado);
        comando.Parameters.AddWithValue("@dni", propietarioParams.Dni);

        conexion.Open();
        filasAfectadas = comando.ExecuteNonQuery();
        return filasAfectadas;
    }

    public int Modificacion(Propietario propietarioParams)
    {
        int filasAfectadas = 0;

        using var conexion = new MySqlConnection(connectionString);

        string consultaSql = @"UPDATE Propietario SET nombre = @nombre, 
        apellido = @apellido, dni = @dni, telefono = @telefono, email = @email, estado = @estado 
        WHERE idPropietario = @idPropietario";

        using var comando = new MySqlCommand(consultaSql, conexion);
        comando.Parameters.AddWithValue("@nombre", propietarioParams.Nombre);
        comando.Parameters.AddWithValue("@apellido", propietarioParams.Apellido);
        comando.Parameters.AddWithValue("@dni", propietarioParams.Dni);
        comando.Parameters.AddWithValue("@telefono", propietarioParams.Telefono);
        comando.Parameters.AddWithValue("@email", propietarioParams.Email);
        comando.Parameters.AddWithValue("@estado", propietarioParams.Estado);
        comando.Parameters.AddWithValue("@idPropietario", propietarioParams.IdPropietario);

        conexion.Open();
        filasAfectadas = comando.ExecuteNonQuery();

        return filasAfectadas;
    }

    public IList<Propietario> ObtenerTodos()
    {
        var lista = new List<Propietario>();

        using var conexion = new MySqlConnection(connectionString);
        string consultaSql = @"SELECT idPropietario, nombre, apellido, dni, telefono, email, estado 
                               FROM Propietario 
                               WHERE estado = 1";

        using var comando = new MySqlCommand(consultaSql, conexion);

        conexion.Open();
        using var leerLista = comando.ExecuteReader();

        while (leerLista.Read())
        {
            var p = new Propietario
            {
                IdPropietario = leerLista.GetInt32("idPropietario"),
                Nombre = leerLista.GetString("nombre"),
                Apellido = leerLista.GetString("apellido"),
                Dni = leerLista.GetString("dni"),
                Telefono = leerLista.IsDBNull(v.GetOrdinal("telefono")) ? "" : leerLista.GetString("telefono"),
                Email = leerLista.GetString("email"),
                Estado = leerLista.GetBoolean("estado")};

            lista.Add(p);
        }

        return lista;
    }
}