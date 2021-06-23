using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CadastroDocumentos.Models
{
    public class DocumentContext : DbContext
    {
        public DocumentContext (DbContextOptions<DocumentContext> options)
            : base(options)
        {
        }

        public DbSet<CadastroDocumentos.Models.DocumentoModel> DocumentoModel { get; set; }      
    }
}
