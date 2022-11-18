using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoTeca.Servicos.Dtos;
using VideoTeca.Servicos.Interfaces;
using VideoTeca.WebApi.Controllers.Base;
using VideoTeca.WebApi.Enum;

namespace VideoTeca.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoMidiaController : Controller
    {
        private readonly IModeloCRUDInterface<TipoMidiaDto> _servico;
        public TipoMidiaController(IModeloCRUDInterface<TipoMidiaDto> servico)
        {
            _servico = servico;
        }
        [HttpGet, Route("buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            try
            {
                TipoMidiaDto tipoConteudo = await _servico.BuscarId(id);
                if (tipoConteudo == null)
                {
                    MessageResultData resultado = MessageResult.Message(Constantes.Constantes.ALERTA, "Tipo mídia não encontrada.", MessageTypeEnum.warning);
                    return Ok(resultado);
                }

                return Ok(tipoConteudo);
            }
            catch (Exception ex)
            {
                return BadRequest(MessageResult.Mensagem(ex));
            }
        }
        [HttpGet, Route("listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                List<TipoMidiaDto> tipoConteudo = await _servico.Listar();
                if (tipoConteudo.Count == 0)
                {
                    MessageResultData resultado = MessageResult.Message(Constantes.Constantes.ALERTA, "Tipo mídia não encontradas.", MessageTypeEnum.warning);
                    return Ok(resultado);
                }
                return Ok(tipoConteudo);
            }
            catch (Exception ex)
            {
                return BadRequest(MessageResult.Mensagem(ex));
            }
        }
        [HttpPut, Route("alterar")]
        public async Task<IActionResult> Alterar(TipoMidiaDto tipoMidia)
        {
            try
            {
                await _servico.Alterar(tipoMidia);
                MessageResultData _resultado = MessageResult.Message(Constantes.Constantes.SUCESSO, "Alteração realizada com sucesso.", MessageTypeEnum.success);
                return Ok(_resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(MessageResult.Mensagem(ex));
            }
        }

        [HttpPost, Route("incluir")]
        public async Task<IActionResult> Incluir(TipoMidiaDto tipoMidia)
        {
            try
            {
                TipoMidiaDto aplic = await _servico.Incluir(tipoMidia);
                MessageResultData _resultado = MessageResult.Message(Constantes.Constantes.SUCESSO, "Inclusão realizada com sucesso.", MessageTypeEnum.success);
                return Ok(_resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(MessageResult.Mensagem(ex));
            }
        }

        [HttpDelete, Route("excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                await _servico.Excluir(id);
                MessageResultData _resultado = MessageResult.Message(Constantes.Constantes.SUCESSO, "Exclusão realizada com sucesso.", MessageTypeEnum.success);
                return Ok(_resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(MessageResult.Mensagem(ex));
            }
        }
    }
}
