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

        // Variable para guardar el nombre generado por la primera letra del nombre provisto
        [BindProperty]
        public string nameGenerationOutput { get; set; }

        // Variable para guardar el nombre generado por el mes de nacimiento
        public string monthGenerationOutput { get; set; }

        // Variable para mostrar el resultado o error por primera vez
        public bool FirstSubmission { get; set; } = false;

        // Variable para mostrar el resultado o error
        public bool CorrectSubmission { get; set; } = false;

        // Variable para cambiar la primera letra del nombre
        static Dictionary<char, string> nameGeneration = new Dictionary<char, string>()
        {
            {'A', "Captain"},
            {'B', "Wonder"},
            {'C', "Super"},
            {'D', "Phantom"},
            {'E', "Dark"},
            {'F', "Incredible"},
            {'G', "Professor"},
            {'H', "Iron"},
            {'I', "Hawk"},
            {'J', "Archer"},
            {'K', "Steel"},
            {'L', "Bolt"},
            {'M', "Atomic"},
            {'N', "Torch"},
            {'O', "Space"},
            {'P', "Mega"},
            {'Q', "Turbo"},
            {'R', "Fantastic"},
            {'S', "Invisible"},
            {'T', "Night"},
            {'U', "Silver"},
            {'V', "Aqua"},
            {'W', "Amazing"},
            {'X', "Giant"},
            {'Y', "Rock"},
            {'Z', "Power"}
        };

        // Variable para cambiar el mes
        static Dictionary<string, string> monthGeneration = new Dictionary<string, string>()
        {
            {"Enero", "Shield"},
            {"Febrero", "Arrow"},
            {"Marzo", "Justice"},
            {"Abril", "Thunder"},
            {"Mayo", "Rider"},
            {"Junio", "Falcon"},
            {"Julio", "Ninja"},
            {"Agosto", "Spider"},
            {"Septiembre", "Beast"},
            {"Octubre", "Blade"},
            {"Noviembre", "Hulk"},
            {"Diciembre", "Doom"}
        };

        public void OnPost()
        {
            FirstSubmission = true;
            if (CheckValidInput())
            {
                UpdateNameInput();
                UpdateMonthInput();
            }
        }

        public bool CheckValidInput()
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(month)
                && char.IsLetter(name[0]) && nameGeneration.ContainsKey(char.ToUpper(name[0])))
            {
                CorrectSubmission = true;
                return true;
            }
            return false;
        }

        public void UpdateNameInput()
        {
            nameGenerationOutput = nameGeneration[char.ToUpper(name[0])];
        }

        public void UpdateMonthInput()
        {
            monthGenerationOutput = monthGeneration[month];
        }
    }
}
