using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.Modelos.Dtos
{
    public class CatalogoTipoMidiaDto
    {
        public int Id { get; set; }
        public int IdCatalogo { get; set; }
        public int IdTipoMidia { get; set; }
        public int QtdTitulo { get; set; }
        public string Descricao { get; set; }
    }
}
