using AdminExpedientePersonal.Entities;
using AdminExpedientePersonal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace AdminExpedientePersonal.Pages.InstitucionesEducativas
{
    public class EditModel : PageModel
    {
        private readonly IInstitucionService _institucionService;

        [BindProperty]
        public InstitucionEntity Institucion { get; set; }
        
        public EditModel(IInstitucionService institucionService)
        {
            _institucionService = institucionService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                HttpContext.Session.SetInt32("UserId", 1);
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserRole")))
                HttpContext.Session.SetString("UserRole", "User");

            int userId = HttpContext.Session.GetInt32("UserId").Value;
            Institucion = await _institucionService.ObtenerInstitucionPorId(id, userId);
            if (Institucion == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                int userId = HttpContext.Session.GetInt32("UserId").Value;
                await _institucionService.ActualizarInstitucion(Institucion, userId);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                ViewData["Error - Actualizar Institución"] = $"{ex.Message}";
                return Page();
            }
        }
    }
}