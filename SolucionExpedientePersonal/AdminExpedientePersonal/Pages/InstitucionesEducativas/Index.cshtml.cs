using AdminExpedientePersonal.Entities;
using AdminExpedientePersonal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminExpedientePersonal.Pages.InstitucionesEducativas
{
    public class IndexModel : PageModel
    {
        private readonly IInstitucionService _institucionService;
        public IEnumerable<InstitucionEntity> Instituciones { get; set; }
        
        public IndexModel(IInstitucionService institucionService)
        {
            _institucionService = institucionService;
        }

        public async Task OnGetAsync()
        {
            // Si no existen, se inicializan las variables de sesión.
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                HttpContext.Session.SetInt32("UserId", 1); // Valor quemado
            }
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserRole")))
            {
                HttpContext.Session.SetString("UserRole", "Admin");
            }
            
            int userId = HttpContext.Session.GetInt32("UserId").Value;
            Instituciones = await _institucionService.ObtenerInstituciones(userId);
        }
    }
}

