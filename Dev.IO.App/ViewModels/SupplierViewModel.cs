using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.IO.App.ViewModels
{
    public class SupplierViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(200, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 3)]
        public string Name { get; set; }

        [DisplayName("Documento")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [StringLength(14, ErrorMessage = "O Campo {0} Precisa ter entre {2} e {1} Caracteres.", MinimumLength = 11)]
        public string Document { get; set; }

        [DisplayName("Tipo")]
        public int SupplierType { get; set; }

        public AndressViewModel Andress { get; set; }

        [DisplayName("Ativo?")]
        public bool IsActive { get; set; }

        #region EF RELATIONS

        public List<ProductViewModel> Products { get; set; }

        #endregion
    }
}
