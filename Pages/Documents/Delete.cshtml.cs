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
    public class DeleteModel : PageModel
    {
        private readonly CadastroDocumentos.Models.DocumentContext _context;

        public DeleteModel(CadastroDocumentos.Models.DocumentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DocumentoModel DocumentoModel { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentoModel = await _context.DocumentoModel.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (DocumentoModel == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Erro ao deletar. Tente Novamente!";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentoModel = await _context.DocumentoModel.FindAsync(id);

            if (DocumentoModel != null)
            {
                _context.DocumentoModel.Remove(DocumentoModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
