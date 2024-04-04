using System;
// Para las validaciones y mensajes de error
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PruebaValidaciones
{
	public class Usuario
	{
        // The validations are put directly in the class definition instead of backend

        [Required(ErrorMessage = "Nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Edad es requrido")]
        // If the value is between 18 and 110 then the input is correct, else throw error
        [Range(18, 110, ErrorMessage = "Eres menor de edad, no puedes votar")]
        public int? Edad { get; set; }

        public Usuario()
		{

		}
	}
}

