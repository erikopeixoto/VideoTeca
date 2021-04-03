using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.Modelos.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string NumDocumento { get; set; }
        public byte TipoPessoa { get; set; }
        public string NomCliente { get; set; }
        public string DesMunicipio { get; set; }
        public string NumTelefone { get; set; }
        public string FoneFormatado { get; set; }
        public string NumDocumentoFormatado { get; set; }
    }
}
