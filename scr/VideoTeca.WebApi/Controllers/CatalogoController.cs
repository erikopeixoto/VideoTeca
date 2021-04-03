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
    public class CatalogoController : Controller
    {
        private readonly CatalogoServico _servico;
        private IMapper _mapper;
        public CatalogoController(DataContext contexto, IMapper mapper)
        {
            _servico = new CatalogoServico(contexto, mapper);
            _mapper = mapper;
        }

        [HttpGet, Route("buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            try
            {
                Catalogo catalogo = await _servico.BuscarId(id);
                if (catalogo == null)
                {
                    MessageResultData _resultado = MessageResult.Message(Constantes.Constantes.ALERTA, "Catalogo não encontrado.", MessageTypeEnum.warning);
                    return Ok(_resultado);
                }
                return Ok(catalogo);
            }
            catch (Exception ex)
            {
                return BadRequest(MessageResult.Mensagem(ex));
            }
        }

        [HttpPost, Route("pesquisar")]
        public async Task<IActionResult> Pesquisar(FiltroCatalogoDto filtro)
        {
            try
            {
                List<CatalogoDto> catalogo = await _servico.Pesquisar(filtro);
                if (catalogo.Count == 0)
                {
                    MessageResultData _resultado = MessageResult.Message(Constantes.Constantes.ALERTA, "Catalogos não encontrados.", MessageTypeEnum.warning);
                    return Ok(_resultado);
                }
                return Json(catalogo);
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
                List<CatalogoDto> catalogo = await _servico.Listar();
                if (catalogo.Count == 0)
                {
                    MessageResultData _resultado = MessageResult.Message(Constantes.Constantes.ALERTA, "Catalogos não encontrados.", MessageTypeEnum.warning);
                    return BadRequest(_resultado);
                }
                return Ok(catalogo);
            }
            catch (Exception ex)
            {
                return BadRequest(MessageResult.Mensagem(ex));
            }
        }

        [HttpPut, Route("alterar")]
        public async Task<IActionResult> Alterar(Catalogo catalogo)
        {
            try
            {
                await _servico.Alterar(catalogo);
                MessageResultData _resultado = MessageResult.Message(Constantes.Constantes.SUCESSO, "Alteração realizada com sucesso.", MessageTypeEnum.success);
                return Ok(_resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(MessageResult.Mensagem(ex));
            }
        }

        [HttpPost, Route("incluir")]
        public async Task<IActionResult> Incluir(Catalogo catalogo)
        {
            try
            {
                CatalogoDto aplic = await _servico.Incluir(catalogo);
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
