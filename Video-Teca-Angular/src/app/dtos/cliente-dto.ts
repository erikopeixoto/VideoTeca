import { Catalogo } from '../modelos/catalogo';
import { Cliente } from '../modelos/cliente';

export interface ClienteDto{
  cliente: Cliente;
  Id: string;
  NomCliente: string;
  DesMunicipio: string;
  NumTelefone: string;
  FoneFormatado: string;
  NumDocumentoFormatado: string;
}
