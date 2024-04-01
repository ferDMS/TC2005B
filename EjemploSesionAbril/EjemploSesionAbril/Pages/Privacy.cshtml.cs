using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace EjemploSesionAbril.Pages;

public class PrivacyModel : PageModel
{

    public string userName { get; set; }

    public void OnGet()
    {
        // Darle un valor a la propiedad vacío por default
        userName = "";

        // Primero checamos que la variable de sesión exista
        // Por ello, buscamos que no sea nulo o vacío
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")) == false)
        {
            // Si sí existe, entonces vamos a actualizar la variable
            userName = HttpContext.Session.GetString("username");
        }
        else
        {
            // Mandar un mensaje de error genérico
            Response.Redirect("Error");
        }
    }
}


