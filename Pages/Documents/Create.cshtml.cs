using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CadastroDocumentos.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CadastroDocumentos.Pages.Documents
{
    public class CreateModel : PageModel
    {
        private readonly CadastroDocumentos.Models.DocumentContext _context;
        private readonly IWebHostEnvironment _iweb;

        public CreateModel(CadastroDocumentos.Models.DocumentContext context, IWebHostEnvironment iweb)
        {
            _context = context;
            _iweb = iweb;
        }

		public IActionResult OnGet(bool? saveChangesError = false)
		{
            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again";
            }
            return Page();
		}

		[BindProperty]
        public DocumentoModel DocumentoModel { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync(IFormFile uploadFile, DocumentoModel doc)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            ModelState.ClearValidationState(nameof(DocumentoModel));
            if (!TryValidateModel(DocumentoModel, nameof(DocumentoModel)))
            {
                return Page();
            }

            if (uploadFile != null)
			{
                string imgText = Path.GetExtension(uploadFile.FileName);

                if (imgText == ".pdf" || imgText == ".doc" || imgText == ".xls" || imgText == ".docx" || imgText == ".xlsx")
                {
                    var imgSave = Path.Combine(_iweb.WebRootPath, "Images", uploadFile.FileName);
                    var stream = System.IO.File.Create(imgSave);
                    await uploadFile.CopyToAsync(stream);
                    stream.Close();
                    doc.Category = DocumentoModel.Category;
                    doc.Title = DocumentoModel.Title;
                    doc.Process = DocumentoModel.Process;
                    doc.Code = DocumentoModel.Code;
                    doc.FileName = uploadFile.FileName;
                    doc.FilePath = imgSave;
                    try
                    {
                        await _context.DocumentoModel.AddAsync(doc);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException /* ex */)
                    {                        
                        return RedirectToAction("./Create",
                                             new { saveChangesError = true });
                    }
                }
            }else
			{
                return NotFound();
            }
         
			
            return RedirectToPage("./Index");
        }
       
    }
}