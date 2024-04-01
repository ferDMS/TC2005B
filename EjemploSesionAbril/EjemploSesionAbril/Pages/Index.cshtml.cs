using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace EjemploSesionAbril.Pages;

public class IndexModel : PageModel
{



    public void OnGet()
    {
        HttpContext.Session.SetString("username", "cgonzalez");
        HttpContext.Session.SetInt32("edad", 20);
    }
}

