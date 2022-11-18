using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using VideoTeca.Servicos.Dtos;

namespace VideoTeca.WebApi.Filters
{
    public class ValidacaoModelState : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errosDtos = new ErrosDto(context.ModelState.SelectMany(sm => sm.Value.Errors).Select(s => s.ErrorMessage));
                context.Result = new BadRequestObjectResult(errosDtos);
            }
        }
    }
}
