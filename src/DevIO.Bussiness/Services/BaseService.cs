using AppMvcBasic.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevIO.Bussiness.Services
{
    public abstract class BaseService
    {

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                Notify(item.ErrorMessage);
            }
        }

        protected void Notify(string message)
        {

        }

        protected bool ExecuteValidation<TValidation,TEntity>(TValidation validation, TEntity entity) where TValidation: AbstractValidator<TEntity> where TEntity: Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);
            return false;

        }
    }
}
