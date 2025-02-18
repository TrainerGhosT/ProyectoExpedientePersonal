using AdminExpedientePersonal.Entities;

namespace AdminExpedientePersonal.Services.Interfaces;

public interface IInstitucionService
{
    Task<IEnumerable<InstitucionEntity>> ObtenerInstituciones(int userId);
    Task<InstitucionEntity?> ObtenerInstitucionPorId(int id, int userId);
    Task<int> CrearInstitucion(InstitucionEntity institucion, int userId);
    Task<int> ActualizarInstitucion(InstitucionEntity institucion, int userId);
    Task<bool> EliminarInstitucion(int id, int userId);
}