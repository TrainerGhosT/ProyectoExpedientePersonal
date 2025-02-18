namespace AdminExpedientePersonal.Services.Interfaces;

public interface IBitacoraService
{
    Task RegistrarBitacora(BitacoraEvent bitacoraEvent);
}