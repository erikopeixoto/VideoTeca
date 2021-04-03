using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.Modelos.Dtos
{
    public class FiltroClienteDto
    {
        public string Nome { get; set; }
        public byte? CodTipoPessoa { get; set; }
        public string NumDocumento { get; set; }
    }
}
