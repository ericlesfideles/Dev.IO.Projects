using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppMvcBasic.Models
{
    public class SupplierEntity: Entity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public SupplierTypeEnum SupplierType { get; set; }
        public AndressEntity Andress { get; set; }
        public bool IsActive { get; set; }

        #region EF RELATIONS

        public List<ProductEntity> Products { get; set; }

        #endregion

    }
}
