using AdminExpedientePersonal.Entities;
using AdminExpedientePersonal.Repository;
using AdminExpedientePersonal.Services.Interfaces;

namespace AdminExpedientePersonal.Services;

public class BitacoraService : IBitacoraService
{
    private readonly IBitacoraRepository _bitacoraRepository;

    public BitacoraService(IBitacoraRepository bitacoraRepository)
    {
        _bitacoraRepository = bitacoraRepository;
    }

    public async Task RegistrarBitacora(BitacoraEvent bitacoraEvent)
    {
        var bitacora = new BitacoraEntity
        {
            IdUsuario = bitacoraEvent.UserId,
            Descripcion = bitacoraEvent.JsonData,
            Fecha = bitacoraEvent.Fecha,
            ModuloAfectado = bitacoraEvent.ModuloAfectado
        };
        try
        {
            await _bitacoraRepository.RegistrarBitacora(bitacora);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al registrar en la bitacora: {ex.Message}");
        }
    }
}