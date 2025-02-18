using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminExpedientePersonal.Pages.Account;

public class SetRoleModel : PageModel
{
    public IActionResult OnGet(string role)
    {
        if (!string.IsNullOrEmpty(role))
        {
            HttpContext.Session.SetString("UserRole", role);
        }
        // Redireccionar a la página anterior si está disponible, de lo contrario a la página de inicio.
        string returnUrl = Request.Headers["Referer"].ToString();
        if (string.IsNullOrEmpty(returnUrl))
            return RedirectToPage("/Index");
        return Redirect(returnUrl);
    }
}