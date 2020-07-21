using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMvcBasic.Models
{
    public  class ProductEntity: Entity
    {
        public Guid SupplierId { get; set; }
        public SupplierEntity Supplier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Value { get; set; }
        public  DateTime Date { get; set; }
        public bool IsActive { get; set; }
      
    }

}
