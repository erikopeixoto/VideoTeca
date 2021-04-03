import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { GenericHttpService } from './generic.service';
import { TipoMidia } from '../modelos/tipo-midia';

@Injectable({
  providedIn: 'root'
})
export class TipoMidiaService extends BaseService{

  public tipomidia: TipoMidia = null;
  public cargaDados: boolean;
  public id: number;

  constructor(
    private readonly servicoHttpGenerico: GenericHttpService<TipoMidia, TipoMidia>
  ) {
    super();
  }

  public listar(): Promise<Array<TipoMidia>> {
    return this.servicoHttpGenerico.getAll(null, `${BaseService.getBaseAPI()}tipomidia/listar`);
  }

  public incluir(tipoMidia: TipoMidia): Promise<TipoMidia> {
    return this.servicoHttpGenerico.post(tipoMidia, `${BaseService.getBaseAPI()}tipomidia/incluir`);
  }

  public excluir(id: number): Promise<TipoMidia> {
    return this.servicoHttpGenerico.delete(`${BaseService.getBaseAPI()}tipomidia/excluir/${id}`);
  }

  public buscar(id: number): Promise<TipoMidia> {
    return this.servicoHttpGenerico.getByIdCli(`${BaseService.getBaseAPI()}tipomidia/buscar/${id}`);
  }

  public alterar(tipoMidia: TipoMidia): Promise<TipoMidia> {
    return this.servicoHttpGenerico.put(tipoMidia, `${BaseService.getBaseAPI()}tipomidia/alterar`);
  }
}
