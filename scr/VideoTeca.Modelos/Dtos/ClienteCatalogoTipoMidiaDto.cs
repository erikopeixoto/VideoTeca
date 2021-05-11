using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.Modelos.Dtos
{
    public class ClienteCatalogoTipoMidiaDto
    {
        public int Id { get; set; }
        public int IdCatalogo { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoMidia { get; set; }
        public int QtdTitulo { get; set; }
        public string DesTitulo { get; set; }
        public string NomCliente { get; set; }
        public string DesTipoMidia { get; set; }
        public string DtcLocacao { get; set; }
        public string DtcDevolucao { get; set; }
    }
}
