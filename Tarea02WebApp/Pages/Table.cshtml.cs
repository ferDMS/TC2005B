﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tarea02WebApp.Pages
{
    public class TableModel : PageModel
    {
        private readonly ILogger<TableModel> _logger;

        public TableModel(ILogger<TableModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}
