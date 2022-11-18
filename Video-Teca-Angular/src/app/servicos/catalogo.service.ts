import { Injectable } from '@angular/core';
import { Catalogo } from '../modelos/catalogo';
import { CatalogoTipoMidiaDto} from '../dtos/catalogo-tipo-midia-dto';
import { BaseService } from './base.service';
import { GenericHttpService } from './generic.service';
import { CatalogoDto } from '../dtos/catalogo-dto';
import { FiltroCatalogoDto } from '../dtos/filtro-catalogo-dto';
@Injectable({
  providedIn: 'root'
})
export class CatalogoService extends BaseService{
  public catalogoDto: CatalogoDto = null;
  public catalogo: Catalogo = null;
  public cargaDados: boolean;
  public id: number;
  public catalogoTipoMidiaDto: CatalogoTipoMidiaDto;

  constructor(
    private readonly servicoHttpGenerico: GenericHttpService<Catalogo, CatalogoDto>,
    private readonly servicoHttpFiltro: GenericHttpService<FiltroCatalogoDto, CatalogoDto>,
  ) {
    super();
  }

  public listar(): Promise<Array<any>> {
    return this.servicoHttpGenerico.getAll(null, `${BaseService.getBaseAPI()}Catalogo/listar`);
  }

  public pesquisar(filtro: FiltroCatalogoDto): Promise<Array<any>> {
    return this.servicoHttpFiltro.post(filtro, 
      `${BaseService.getBaseAPI()}Catalogo/pesquisar`);
  }

  public incluir(catalogo: Catalogo): Promise<CatalogoDto> {
    return this.servicoHttpGenerico.post(catalogo, `${BaseService.getBaseAPI()}Catalogo/incluir`);
  }

  public excluir(id: number): Promise<CatalogoDto> {
    return this.servicoHttpGenerico.delete(`${BaseService.getBaseAPI()}Catalogo/excluir/${id}`);
  }

  public buscar(id: number): Promise<Catalogo> {
    return this.servicoHttpGenerico.getByIdCli(`${BaseService.getBaseAPI()}Catalogo/buscar/${id}`);
  }

  public alterar(catalogo: Catalogo): Promise<CatalogoDto> {
    return this.servicoHttpGenerico.put(catalogo, `${BaseService.getBaseAPI()}Catalogo/alterar`);
  }

}
