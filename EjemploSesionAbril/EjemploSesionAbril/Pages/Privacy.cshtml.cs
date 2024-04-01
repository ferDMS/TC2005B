using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// Paquete para agregar variables de sesión
using Microsoft.AspNetCore.Http;

// Paquete para agregar valores de configuración de app (para connection string)
using Microsoft.Extensions.Configuration;

namespace EjemploSesionAbril.Pages;

public class PrivacyModel : PageModel
{
    // Propiedad para guardar el valor de la variable de sesión
    public string userName { get; set; }

    // Propiedad para guardar el string de conexión a la base de datos
    public string stringConexion { get; set; }

    // Propiedad de solo lectura para guardar el objeto de configuración de la app
    private readonly IConfiguration _configuration;

    // Constructor de la clase PrivacyModel, esta clase que estamos editando
    // Cuando se crea la clase se importa la configuración de appsettings.json
    public PrivacyModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

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

        stringConexion = _configuration.GetConnectionString("myDb1");
    }
}


