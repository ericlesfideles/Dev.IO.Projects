using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.API.ViewModel
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(200, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(1000, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 3)]
        public string Description { get; set; }

        public string Image { get; set; }

        public string ImageUpload { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public decimal Value { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public bool IsActive { get; set; }
    }
}
