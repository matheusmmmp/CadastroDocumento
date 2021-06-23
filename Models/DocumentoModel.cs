using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroDocumentos.Models
{
	[Table("documentomodel")]
	public class DocumentoModel
    {
		[Key]
		public int ID { get; set; }

		//[Required]		
		[RegularExpression("^[0-9]*$", ErrorMessage = "O Código deve ser númerico!")]
		[Display(Name = "Código")]
		[Column("Code")]
		public string? Code { get; set; }

		//[Required]
		[Display(Name = "Título")]
		[Column("Title")]
		public string? Title { get; set; }

		//[Required]
		[Display(Name = "Processo")]
		[Column("Process")]
		public string? Process { get; set; }

		//[Required]
		[Display(Name = "Categoria")]
		[Column("Category")]
		public Category? Category { get; set; }

		//[Required]
		[Display(Name = "Arquivo")]
		[Column("FileName")]
		public string? FileName { get; set; }      
        public string FilePath { get; set; }
     
    }
    public enum Category
    {
        A, B, C, D, F
    }
}
