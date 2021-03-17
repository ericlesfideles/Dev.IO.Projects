using System;
using System.Linq;
using Dev.IO.Data.Repository;
using DevIO.Bussiness.Interfaces;
using DevIO.Bussiness.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace DevIO.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotify _notify;

        public MainController(INotify notify)
        {
            _notify = notify;
        }
        protected bool ValidOperation()
        {
            return !_notify.HasNotification();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(value: new
                {
                    sucess = true,
                    data = result
                });
            }

            return BadRequest(error: new
            {
                sucess = false,
                erros = _notify.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse (ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyErroModelState(modelState);
            return CustomResponse();
        }

        private void NotifyErroModelState(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var item in erros)
            {
                var erroMessage = item.Exception == null ? item.ErrorMessage : item.Exception.Message;

                NotifyErro(erroMessage);
            }
        }

        private void NotifyErro(string erroMessage)
        {
            _notify.Handle(new Notification(erroMessage));
        }
    }

}
