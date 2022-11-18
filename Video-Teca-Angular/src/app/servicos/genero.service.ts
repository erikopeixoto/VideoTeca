import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { GenericHttpService } from './generic.service';
import { Genero } from '../modelos/genero';

@Injectable({
  providedIn: 'root'
})
export class GeneroService extends BaseService{

  public genero: Genero = null;
  public cargaDados: boolean;
  public id: number;

  constructor(
    private readonly servicoHttpGenerico: GenericHttpService<Genero, Genero>
  ) {
    super();
  }

  public listar(): Promise<Array<Genero>> {
    return this.servicoHttpGenerico.getAll(null, `${BaseService.getBaseAPI()}genero/listar`);
  }

  public incluir(genero: Genero): Promise<Genero> {
    return this.servicoHttpGenerico.post(genero, `${BaseService.getBaseAPI()}genero/incluir`);
  }

  public excluir(id: number): Promise<Genero> {
    return this.servicoHttpGenerico.delete(`${BaseService.getBaseAPI()}genero/excluir/${id}`);
  }

  public buscar(id: number): Promise<Genero> {
    return this.servicoHttpGenerico.getByIdCli(`${BaseService.getBaseAPI()}genero/buscar/${id}`);
  }

  public alterar(genero: Genero): Promise<Genero> {
    return this.servicoHttpGenerico.put(genero, `${BaseService.getBaseAPI()}Genero/alterar`);
  }
}
