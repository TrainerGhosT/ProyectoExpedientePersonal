using AdminExpedientePersonal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace AdminExpedientePersonal.Pages.InstitucionesEducativas
{
    public class DeleteModel : PageModel
    {
        private readonly IInstitucionService _institucionService;

        public DeleteModel(IInstitucionService institucionService)
        {
            _institucionService = institucionService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                int userId = HttpContext.Session.GetInt32("UserId").Value;
                await _institucionService.EliminarInstitucion(id, userId);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToPage("Index");
        }
    }
}