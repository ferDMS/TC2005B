using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// Para las validaciones y mensajes de error
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaValidaciones.Pages;

public class IndexModel : PageModel
{
    // If we used basic variables instead of class object
    /*
    [BindProperty]
    [Required(ErrorMessage = "Nombre es requerido")]
    public string Nombre { get; set; }

    [BindProperty]
    // If the value is null, throw error
    [Required(ErrorMessage = "Edad es requrido")]
    // If the value is between 18 and 110 then the input is correct, else throw error
    [Range(18, 110, ErrorMessage = "Eres menor de edad, no puedes votar")]
    public int? Edad { get; set; }
    */


    // Using a class object instead of basic variables
    [BindProperty]
    public Usuario usuarioX { get; set; }

    public string Mensaje { get; set; }

    public void OnGet()
    {
        // Initialize a new User object to save the variables
        usuarioX = new Usuario();
        Mensaje = "";
    }

    public void OnPost()
    {
        Mensaje = "eres mayor de edad";
    }
}

