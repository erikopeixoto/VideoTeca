import { CatalogoTipoMidia } from './catalogo-tipo-midia';
export class Catalogo{
  id: number;
  idGenero: number;
  desTitulo: string;
  nomAutor: string;
  anoLancamento: string;
  codigo: string;

  catalogoTipoMidias: CatalogoTipoMidia[];
}
