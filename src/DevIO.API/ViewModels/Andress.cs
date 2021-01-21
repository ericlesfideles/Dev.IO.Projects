using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.API.ViewModel
{
    public class Andress
    {
        [Key]
        public Guid Id { get; set; }

        [HiddenInput]
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(200, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 3)]
        public string Street { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(8, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 1)]
        public string Number { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(500, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(8, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 8)]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 2)]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(50, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 2)]
        public string State { get; set; }
    }
}
