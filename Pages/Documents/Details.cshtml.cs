using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CadastroDocumentos.Models;

namespace CadastroDocumentos.Pages.Documents
{
    public class DetailsModel : PageModel
    {
        private readonly CadastroDocumentos.Models.DocumentContext _context;

        public DetailsModel(CadastroDocumentos.Models.DocumentContext context)
        {
            _context = context;
        }

        public DocumentoModel DocumentoModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentoModel = await _context.DocumentoModel.FirstOrDefaultAsync(m => m.ID == id);

            if (DocumentoModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
