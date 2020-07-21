using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.IO.App.ViewModels
{
    public class AndressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [HiddenInput]
        public Guid SupplierId { get; set; }
        public SupplierViewModel Supplier { get; set; }

        public IEnumerable<SupplierViewModel> Suppliers { get; set; }

        [DisplayName("Logradouro")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(200, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 3)]
        public string Street { get; set; }

        [DisplayName("Número")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(8, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 1)]
        public string Number { get; set; }

        [DisplayName("Complemento")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(500, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.")]
        public string Complement { get; set; }

        [DisplayName("CEP")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(8, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 8)]
        public string PostCode { get; set; }

        [DisplayName("Bairro")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 2)]
        public string Neighborhood { get; set; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 2)]
        public string City { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(50, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 2)]
        public string State { get; set; }

    }
}
