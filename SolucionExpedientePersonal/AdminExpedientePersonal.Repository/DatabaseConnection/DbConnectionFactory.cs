using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AdminExpedientePersonal.Repository.DatabaseConnection;

public class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string? _connectionString = configuration.GetConnectionString("MySqlConnection");

    public IDbConnection GetConnection()
    {
        try
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
        catch (Exception ex)
        {
            throw new Exception("Error al conectar la base de datos:", ex);
        }
    }
}