using AdminExpedientePersonal.Entities;
using AdminExpedientePersonal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminExpedientePersonal.Pages.InstitucionesEducativas
{
    public class CreateModel : PageModel
    {
        private readonly IInstitucionService _institucionService;

        [BindProperty]
        public InstitucionEntity Institucion { get; set; }
        
        public CreateModel(IInstitucionService institucionService)
        {
            _institucionService = institucionService;
        }

        public void OnGet()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                HttpContext.Session.SetInt32("UserId", 1);
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserRole")))
                HttpContext.Session.SetString("UserRole", "User");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                int userId = HttpContext.Session.GetInt32("UserId").Value;
                await _institucionService.CrearInstitucion(Institucion, userId);
                ViewData["Success"] = "Institucion Educativa creada correctamente.";
                return Page(); // Se queda en la página para mostrar el alert y redireccionar con JS
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}