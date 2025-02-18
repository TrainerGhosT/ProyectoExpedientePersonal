using AdminExpedientePersonal.Entities;

namespace AdminExpedientePersonal.Repository;

public interface IBitacoraRepository
{
    Task RegistrarBitacora(BitacoraEntity bitacora);
}