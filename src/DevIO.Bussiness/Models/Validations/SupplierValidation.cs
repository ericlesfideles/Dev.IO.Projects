using AppMvcBasic.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DevIO.Bussiness.Validations
{
    public class SupplierValidation: AbstractValidator<SupplierEntity>
    {
        public SupplierValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("O Campo (PropertyName) precisa ser fornecido")
                .Length(min: 2, max: 100).WithMessage("O Campo (PropertyName) precisa ter entre (MinLength) e (MaxLength) caracteres");

            When(f => f.SupplierType == SupplierTypeEnum.PESSOAFISICA, action: () =>
            { 
                //RuleFor(f => f.Document.Length)
           
            });

            When(f => f.SupplierType == SupplierTypeEnum.PESSOAJURIDICA, action: () => { 
            });
        }
    }
}
