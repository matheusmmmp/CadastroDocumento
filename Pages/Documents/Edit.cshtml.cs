using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroDocumentos.Models;

namespace CadastroDocumentos.Pages.Documents
{
    public class EditModel : PageModel
    {
        private readonly CadastroDocumentos.Models.DocumentContext _context;

        public EditModel(CadastroDocumentos.Models.DocumentContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DocumentoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoModelExists(DocumentoModel.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DocumentoModelExists(int id)
        {
            return _context.DocumentoModel.Any(e => e.ID == id);
        }
    }
}
