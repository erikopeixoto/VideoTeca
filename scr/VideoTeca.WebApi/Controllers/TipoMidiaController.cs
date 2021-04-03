using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Modelos.Dtos;
using VideoTeca.AcessoDados.Contexto;
using AutoMapper;
using System.Threading.Tasks;
using VideoTeca.Servicos.Servicos;
using VideoTeca.WebApi.Controllers.Base;
using VideoTeca.WebApi.Enum;
using Microsoft.AspNetCore.Http;

namespace VideoTeca.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoMidiaController : Controller
    {
        private readonly TipoMidiaServico _servico;
        public TipoMidiaController(DataContext contexto, IMapper mapper)
        {
            _servico = new TipoMidiaServico(contexto);
        }
        [HttpGet, Route("buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            try
            {
                TipoMidia tipoConteudo = await _servico.BuscarId(id);
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
                List<TipoMidia> tipoConteudo = await _servico.Listar();
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
        public async Task<IActionResult> Alterar(TipoMidia tipoMidia)
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
        public async Task<IActionResult> Incluir(TipoMidia tipoMidia)
        {
            try
            {
                TipoMidia aplic = await _servico.Incluir(tipoMidia);
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
