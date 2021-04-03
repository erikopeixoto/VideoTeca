import { Catalogo } from './catalogo';

export class Cliente{
  id: number;
  nomCliente: string;
  tipoPessoa: number;
  numDocumento: string;
  numCep: string;
  desLogradouro: string;
  numTelefone: string;
  desComplemento: string;
  numEndereco: string;
  desBairro: string;
  desMunicipio: string;
  dtcNascimento: string;

  catalogo: Catalogo;

}
