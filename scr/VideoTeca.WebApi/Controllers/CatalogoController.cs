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
    public class CatalogoController : Controller
    {
        private readonly ICatalogoInterface _servico;
        public CatalogoController(ICatalogoInterface servico)
        {
            _servico = servico;
        }

        [HttpGet, Route("buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            try
            {
                CatalogoDto catalogoDto = await _servico.BuscarId(id);
                if (catalogoDto == null)
                {
                    MessageResultData _resultado = MessageResult.Message(Constantes.Constantes.ALERTA, "Catalogo não encontrado.", MessageTypeEnum.warning);
                    return Ok(_resultado);
                }
                return Ok(catalogoDto);
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
        public async Task<IActionResult> Alterar(CatalogoDto catalogo)
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
        public async Task<IActionResult> Incluir(CatalogoDto catalogo)
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
