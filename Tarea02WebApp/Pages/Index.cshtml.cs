using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tarea02WebApp.Pages
{
    public class IndexModel : PageModel
    {
        // Variable para guardar el nombre
        [BindProperty]
        public string name { get; set; }

        // Variable para guardar el mes de nacimiento
        [BindProperty]
        public string month { get; set; }

        // Variable para mostrar el resultado o error por primera vez
        public bool FirstSubmission { get; set; } = false;

        // Variable para mostrar el resultado o error
        public bool CorrectSubmission { get; set; } = false;

        public void OnPost()
        {
            FirstSubmission = true;
            CheckValidInput();
            CheckNameInput();
            CheckMonthInput();
        }

        public void CheckValidInput()
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(month))
            {
                CorrectSubmission = true;
            }
        }

        public void CheckNameInput()
        {

        }

        public void CheckMonthInput()
        {

        }
    }
}
