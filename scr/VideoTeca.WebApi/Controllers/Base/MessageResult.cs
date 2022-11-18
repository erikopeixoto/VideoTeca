using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VideoTeca.WebApi.Enum;

namespace VideoTeca.WebApi.Controllers.Base
{
    public class MsgResult : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MessageResultData
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public MessageTypeEnum TypeInt { get; set; }
        public string Type { get { return this.TypeInt.ToString(); } }
    }

    public static class MessageResult
    {
        public static MessageResultData Message(string title, string message, MessageTypeEnum messageType)
        {
            MessageResultData result = new MessageResultData();
            result.Message = message;
            result.Title = title;
            result.TypeInt = messageType;
            return result;
        }
        public static MessageResultData Mensagem(Exception ex)
        {
            MessageResultData result = new MessageResultData();
            result.Message = ex.Message;
            if (ex.GetType() == typeof(ArgumentException))
            {
                result.Title = Constantes.Constantes.ALERTA;
                result.TypeInt = MessageTypeEnum.warning;
            }
            else
            {
                result.Title = Constantes.Constantes.ERRO;
                result.TypeInt = MessageTypeEnum.danger;
            }
            return result;
        }
    }

}

