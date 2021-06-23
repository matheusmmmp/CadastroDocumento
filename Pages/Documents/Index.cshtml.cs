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
    public class IndexModel : PageModel
    {
        private readonly CadastroDocumentos.Models.DocumentContext _context;

        public IndexModel(CadastroDocumentos.Models.DocumentContext context)
        {
            _context = context;
        }

        public string CodeSort { get; set; }
        public string TitleSort { get; set; }
        public string ProcessSort { get; set; }
        public string CategorySort { get; set; }
        public string CurrentFilter { get; set; }
   
        

        public IList<DocumentoModel> DocumentoModel { get;set; }

        //public async Task OnGetAsync()
        //{
        //    DocumentoModel = await _context.DocumentoModel.ToListAsync();
        //}

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            CodeSort = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ProcessSort = String.IsNullOrEmpty(sortOrder) ? "process_desc" : "";
            CategorySort = String.IsNullOrEmpty(sortOrder) ? "category_desc" : "";
            CurrentFilter = searchString;

            IQueryable<DocumentoModel> documentIQ = from d in _context.DocumentoModel
                                                   select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                documentIQ = documentIQ.Where(d => d.Code.Contains(searchString)
                                       || d.Title.Contains(searchString)
                                       || d.Process.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "code_desc":
                    documentIQ = documentIQ.OrderByDescending(d => d.Code);
                    break;
                case "title_desc":
                    documentIQ = documentIQ.OrderByDescending(d => d.Title);
                    break;
                case "process_desc":
                    documentIQ = documentIQ.OrderByDescending(d => d.Process);
                    break;
                case "category_desc":
                    documentIQ = documentIQ.OrderByDescending(d => d.Category);
                    break;
                default:
                    documentIQ = documentIQ.OrderBy(d => d.Title);
                    break;
            }

            DocumentoModel = await documentIQ.AsNoTracking().ToListAsync();
        }
    }
}
