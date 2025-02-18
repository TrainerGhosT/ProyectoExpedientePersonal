using System.Data;

namespace AdminExpedientePersonal.Repository.DatabaseConnection;

public interface IDbConnectionFactory
{
    IDbConnection GetConnection();
}