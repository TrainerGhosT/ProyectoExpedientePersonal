using AdminExpedientePersonal.Entities;
using AdminExpedientePersonal.Repository.DatabaseConnection;
using Dapper;

namespace AdminExpedientePersonal.Repository;

public class BitacoraRepository : IBitacoraRepository
{
    private readonly IDbConnectionFactory _dbConnection;

    public BitacoraRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnection = dbConnectionFactory;
    }

    public async Task RegistrarBitacora(BitacoraEntity bitacora)
    {
        using var connection = _dbConnection.GetConnection();
       const string query = @"
                INSERT INTO Bitacora (IdUsuario, Descripcion, ModuloAfectado, Fecha) 
                VALUES (@IdUsuario, @Descripcion, @ModuloAfectado, @Fecha);";

        await connection.ExecuteAsync(query, bitacora);
    }
}