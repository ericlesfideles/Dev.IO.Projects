using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.IO.App.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Fornecedor")]
        public Guid SupplierId { get; set; }
        public SupplierViewModel Supplier { get; set; }

        public IEnumerable<SupplierViewModel> Suppliers { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(200, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 3)]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(1000, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 3)]
        public string Description { get; set; }

        [DisplayName("Imagem")]
        public string Image { get; set; }

        [DisplayName("Imagem")]
        public IFormFile ImageUpload { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public decimal Value { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Date { get; set; }

        [DisplayName("Ativo?")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public bool IsActive { get; set; }

    }
}
