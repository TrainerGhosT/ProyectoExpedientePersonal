using System.Data;
using AdminExpedientePersonal.Entities;
using AdminExpedientePersonal.Repository.DatabaseConnection;
using Dapper;

namespace AdminExpedientePersonal.Repository
{
    public class InstitucionRepository : IInstitucionRepository
    {
        private readonly IDbConnectionFactory _connection;

        public InstitucionRepository(IDbConnectionFactory connectionFactory)
        {
            _connection = connectionFactory;
        }

        public async Task<IEnumerable<InstitucionEntity>> ObtenerInstituciones()
        {
            using IDbConnection connection = _connection.GetConnection();
            string query = "SELECT IdInstitucion, Nombre FROM institucion";
            return await connection.QueryAsync<InstitucionEntity>(query);
        }
        public async Task<InstitucionEntity?> ObtenerInstitucionPorId(int id)
        {
            using IDbConnection connection = _connection.GetConnection();
            string query = "SELECT IdInstitucion, Nombre FROM institucion WHERE IdInstitucion = @IdInstitucion";
            return await connection.QueryFirstOrDefaultAsync<InstitucionEntity>(query, new { IdInstitucion = id });
        }

        public async Task<int> CrearInstitucion(InstitucionEntity institucion)
        {
            using IDbConnection connection = _connection.GetConnection();
            const string query = @"INSERT INTO institucion (Nombre) VALUES (@Nombre);";
            return await connection.ExecuteScalarAsync<int>(query, institucion);
            
        }

        public async Task<int> ActualizarInstitucion(InstitucionEntity institucion)
        {
            using IDbConnection connection = _connection.GetConnection();
            string query = "UPDATE institucion SET Nombre = @Nombre WHERE IdInstitucion = @IdInstitucion";
            return await connection.ExecuteAsync(query, institucion);
            
        }

        public async Task<bool> EliminarInstitucion(int id)
        {
            using IDbConnection connection = _connection.GetConnection();
            string query = "DELETE FROM institucion WHERE IdInstitucion = @Id";
            int affected = await connection.ExecuteAsync(query, new { Id = id });
            return affected > 0;
        }

        // Consulta para verificar si la institución tiene registros relacionados (por ejemplo, en 'preparacion')
        public async Task<bool> InstitucionRelacionada(int id)
        {
            using IDbConnection connection = _connection.GetConnection();
            string query = "SELECT COUNT(1) FROM preparacion WHERE IdInstitucion = @Id";
            int count = await connection.ExecuteScalarAsync<int>(query, new { Id = id });
            return count > 0;
        }

    }
}
