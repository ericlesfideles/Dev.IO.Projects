
using Dev.IO.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.IO.App.Extensions
{
    public class CustomErroDetail
    {
        public ErrorViewModel GetErroDetailByid(int id)
        {
            ErrorViewModel errorViewModel = new ErrorViewModel();

            switch (id)
            {
                case 404:
                    errorViewModel.ErroCode = id;
                    errorViewModel.Title = "Ops!  Página não encontrada.";
                    errorViewModel.Message = "A página que está procurando não existe! <br/> Em caso de dúvidas entre em contato com nosso suporte.";
                    break;

                case 403:
                    errorViewModel.ErroCode = id;
                    errorViewModel.Title = "Acesso negado!";
                    errorViewModel.Message = "Você não tem permissão para fazer isto!";
                    break;
                case 500:
                default:
                    errorViewModel.ErroCode = id;
                    errorViewModel.Title = "Ocorreu um erro!";
                    errorViewModel.Message = "Ocorreu um erro! Tente novamente mais tarde ou entre em contato com o suporte.";
                    break;
            }

            return errorViewModel;

        }

    }
}
