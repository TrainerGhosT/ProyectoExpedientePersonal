namespace AdminExpedientePersonal.Services;

public class BitacoraEvent
{
    public int UserId { get; set; }
    public string Action { get; set; } = string.Empty; // Ej: 'Creacion', 'Actualizacion', 'Eliminacion', 'Consulta'
    public string JsonData { get; set; } = string.Empty;
    public string ModuloAfectado { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Now;
}