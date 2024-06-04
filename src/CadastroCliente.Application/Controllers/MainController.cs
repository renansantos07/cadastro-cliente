using CadastroCliente.Application.DTO;
using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CadastroCliente.Application.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        protected MainController(INotifier notifier)
        {
            _notifier = notifier;
        }
        
        /// <summary>
        /// Valida se existe alguma notificação durante a chamada
        /// </summary>
        /// <returns></returns>
        protected bool OperacaoValida()
        {
            return !_notifier.HasNotification();
        }

        /// <summary>
        /// Realiza a validação e retorna o response de acordo com o status
        /// Se houver alerta retorna badRequest se não houver alerta retorna Success(Ok)
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new SuccessResponseDTO
                {
                    Success = true,
                    Data = result
                });
            }

            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        /// <summary>
        /// Adiciona um alerta/erro na lista de notificações
        /// </summary>
        /// <param name="mensagem">Mensagem a ser salva na notificação</param>
        protected void NotificarErro(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }
    }
}