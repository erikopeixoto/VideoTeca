using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Dtos;
using VideoTeca.Servicos.Interfaces;
using VideoTeca.WebApi.Controllers.Base;
using VideoTeca.WebApi.Enum;
using VideoTeca.WebApi.Filters;

namespace VideoTeca.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly IClienteInterface _clienteServico;
        private readonly IClienteCatalogoTipoMidiaInterface _clienteCatalogoServico;
        public ClienteController(IClienteInterface clienteServico, IClienteCatalogoTipoMidiaInterface clienteCatalogoServico)
        {
            _clienteServico = clienteServico;
            _clienteCatalogoServico = clienteCatalogoServico;
        }

        [HttpGet, Route("buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            try
            {
                Cliente cliente = await _clienteServico.BuscarId(id);
                if (cliente == null)
                {
                    MessageResultData _resultado = MessageResult.Message(Constantes.Constantes.ALERTA, "Cliente não encontrado.", MessageTypeEnum.warning);
                    return Ok(_resultado);
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(MessageResult.Mensagem(ex));
            }
        }

        [HttpPost, Route("pesquisar")]
        public async Task<IActionResult> Pesquisar(FiltroClienteDto filtro)
        {
            try
            {
                List<ClienteDto> cliente = await _clienteServico.Pesquisar(filtro);
                if (cliente.Count == 0)
                {
                    MessageResultData _resultado = MessageResult.Message(Constantes.Constantes.ALERTA, "Clientes não encontrados.", MessageTypeEnum.warning);
                    return Ok(_resultado);
                }
                return Json(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(MessageResult.Mensagem(ex));
            }
        }

        [HttpPost, Route("pesquisarCatalogo")]
        public IActionResult Pesquisar(FiltroCatalogoDto filtro)
        {
            try
            {
                List<ClienteCatalogoTipoMidiaDto> catalogo = _clienteCatalogoServico.Pesquisar(filtro);
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
                List<ClienteDto> cliente = await _clienteServico.Listar();
                if (cliente.Count == 0)
                {
                    MessageResultData _resultado = MessageResult.Message(Constantes.Constantes.ALERTA, "Clientes não encontrados.", MessageTypeEnum.warning);
                    return BadRequest(_resultado);
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(MessageResult.Mensagem(ex));
            }
        }

        [HttpPut, Route("alterar")]
        [ValidacaoModelState]
        public async Task<IActionResult> Alterar(Cliente cliente)
        {
            try
            {
                await _clienteServico.Alterar(cliente);
                MessageResultData _resultado = MessageResult.Message(Constantes.Constantes.SUCESSO, "Alteração realizada com sucesso.", MessageTypeEnum.success);
                return Ok(_resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(MessageResult.Mensagem(ex));
            }
        }

        [HttpPost, Route("incluir")]
        [ValidacaoModelState]
        public async Task<IActionResult> Incluir(Cliente cliente)
        {
            try
            {
                ClienteDto aplic = await _clienteServico.Incluir(cliente);
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
                await _clienteServico.Excluir(id);
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
