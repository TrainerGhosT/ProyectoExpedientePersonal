using AdminExpedientePersonal.Entities;
namespace AdminExpedientePersonal.Repository;

public interface IInstitucionRepository
{
    Task<IEnumerable<InstitucionEntity>> ObtenerInstituciones();
    Task<InstitucionEntity?> ObtenerInstitucionPorId(int id);
    Task<int> CrearInstitucion(InstitucionEntity institucion);
    Task<int> ActualizarInstitucion(InstitucionEntity institucion);
    Task<bool> EliminarInstitucion(int id);
    Task<bool> InstitucionRelacionada(int id);
}