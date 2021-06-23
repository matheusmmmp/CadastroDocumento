using CadastroDocumentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CadastroDocumentos.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DocumentContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.DocumentoModel.Any())
            {
                return;   // DB has been seeded
            }

            var Document = new DocumentoModel[]
            {
            //new DocumentoModel{Code="31313",Title="Alexander",Process="2005-09-01",Category=Category.F,File="sadas"},
            new DocumentoModel{Code="31223",Title="Alexander",Process="2005-09-01"},
			new DocumentoModel{Code="31313",Title="Alexander",Process="2005-09-01"},
			new DocumentoModel{Code="32223",Title="Alexander",Process="2005-09-01"},
			new DocumentoModel{Code="311123",Title="Alexander",Process="2005-09-01"}
			};
            foreach (DocumentoModel s in Document)
            {
                context.DocumentoModel.Add(s);
            }
           context.SaveChanges();
        }
    }
}


