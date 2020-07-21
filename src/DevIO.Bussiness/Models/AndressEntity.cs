using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppMvcBasic.Models
{
    public class AndressEntity: Entity
    {
        public Guid SupplierId { get; set; }
        public SupplierEntity Supplier { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string PostCode { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        


    }
}
