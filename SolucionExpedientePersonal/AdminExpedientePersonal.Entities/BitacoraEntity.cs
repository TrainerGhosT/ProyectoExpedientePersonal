namespace AdminExpedientePersonal.Entities;

public class BitacoraEntity
{
    public int IdUsuario { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string ModuloAfectado { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } 
}