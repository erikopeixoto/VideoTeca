import { Injectable } from '@angular/core';
import { Cliente } from '../modelos/cliente';
import { BaseService } from './base.service';
import { GenericHttpService } from './generic.service';
import { ClienteDto } from '../dtos/cliente-dto';
import { ClienteCatalogoTipoMidiaDto } from '../dtos/cliente-catalogo-tipo-midia-dto';
import { FiltroClienteDto } from '../dtos/filtro-cliente-dto';
import { FiltroCatalogoDto } from '../dtos/filtro-catalogo-dto';

@Injectable({
  providedIn: 'root'
})
export class ClienteService extends BaseService{
  public clienteDto: ClienteDto = null;
  public cliente: Cliente = null;
  public cargaDados: boolean;
  public id: number;

  constructor(
    private readonly servicoHttpGenerico: GenericHttpService<Cliente, ClienteDto>,
    private readonly servicoHttpFiltro: GenericHttpService<FiltroClienteDto, ClienteDto>,
    private readonly servicoHttpFiltroCatalogo: GenericHttpService<FiltroCatalogoDto, ClienteCatalogoTipoMidiaDto>,
  ) {
    super();
  }

  public listar(): Promise<Array<any>> {
    return this.servicoHttpGenerico.getAll(null, `${BaseService.getBaseAPI()}Cliente/listar`);
  }

  public pesquisar(filtro: FiltroClienteDto): Promise<Array<any>> {
    return this.servicoHttpFiltro.post(filtro, 
      `${BaseService.getBaseAPI()}Cliente/pesquisar`);
  }

  public pesquisarCatalogo(filtro: FiltroCatalogoDto): Promise<Array<any>> {
    return this.servicoHttpFiltroCatalogo.post(filtro, 
      `${BaseService.getBaseAPI()}Cliente/pesquisarCatalogo`);
  }

  public incluir(cliente: Cliente): Promise<ClienteDto> {
    return this.servicoHttpGenerico.post(cliente, `${BaseService.getBaseAPI()}Cliente/incluir`);
  }

  public excluir(id: number): Promise<ClienteDto> {
    return this.servicoHttpGenerico.delete(`${BaseService.getBaseAPI()}Cliente/excluir/${id}`);
  }

  public buscar(id: number): Promise<Cliente> {
    return this.servicoHttpGenerico.getByIdCli(`${BaseService.getBaseAPI()}Cliente/buscar/${id}`);
  }

  public alterar(cliente: Cliente): Promise<ClienteDto> {
    return this.servicoHttpGenerico.put(cliente, `${BaseService.getBaseAPI()}Cliente/alterar`);
  }

}
