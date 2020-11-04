using DevIO.Bussiness.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Dev.IO.App.Controllers
{
    public abstract class BaseController: Controller
    {
        private readonly INotify _notify;
       
        public BaseController(INotify notify)
        {
            _notify = notify;
        }
        protected bool ValidOperation()
        {
            return !_notify.HasNotification();
        }

        public async Task<bool> UploadImage(IFormFile formFile, string prefix) 
        {
            if (formFile.Length < 1) return false;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image" , prefix + formFile.FileName );


            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com esse nome!");
                return false;
            }

            using(var stream = new FileStream(path, FileMode.Create))
            {

                await formFile.CopyToAsync(stream);
                
            }

            return true;
        }

    }
}
