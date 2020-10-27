using AppMvcBasic.Models;
using DevIO.Bussiness.Interfaces;
using DevIO.Bussiness.Notifications;
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
        private readonly INotify _notify;

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                Notify(item.ErrorMessage);
            }
        }

        protected void Notify(string message)
        {
            _notify.Handle(new Notification(message));

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
