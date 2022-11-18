import { CatalogoTipoMidia } from './catalogo-tipo-midia';
import { CatalogoTipoMidiaDto } from './../dtos/catalogo-tipo-midia-dto';
export class Catalogo{
  id: number;
  idGenero: number;
  desTitulo: string;
  nomAutor: string;
  anoLancamento: string;
  codigo: string;
  dtcAtualizacao: string;
  desGenero: string;

  catalogoTipoMidias: CatalogoTipoMidia[];
  catalogoTipoMidiasDto: CatalogoTipoMidiaDto[];
}
