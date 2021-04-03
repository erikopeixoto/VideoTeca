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
    public class ClienteController : Controller
    {
        private readonly ClienteServico _servico;
        private IMapper _mapper;
        public ClienteController(DataContext contexto, IMapper mapper)
        {
            _servico = new ClienteServico(contexto, mapper);
            _mapper = mapper;
        }

        [HttpGet, Route("buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            try
            {
                Cliente cliente = await _servico.BuscarId(id);
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
                List<ClienteDto> cliente = await _servico.Pesquisar(filtro);
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

        [HttpGet, Route("listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                List<ClienteDto> cliente = await _servico.Listar();
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
        public async Task<IActionResult> Alterar(Cliente cliente)
        {
            try
            {
                await _servico.Alterar(cliente);
                MessageResultData _resultado = MessageResult.Message(Constantes.Constantes.SUCESSO, "Alteração realizada com sucesso.", MessageTypeEnum.success);
                return Ok(_resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(MessageResult.Mensagem(ex));
            }
        }

        [HttpPost, Route("incluir")]
        public async Task<IActionResult> Incluir(Cliente cliente)
        {
            try
            {
                ClienteDto aplic = await _servico.Incluir(cliente);
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
