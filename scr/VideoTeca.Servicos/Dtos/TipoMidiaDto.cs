using System;
using System.Collections.Generic;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.Servicos.Dtos
{
    public class TipoMidiaDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string DtcAtualizacao { get; set; }
        public virtual List<CatalogoTipoMidia> CatalogoTipoMidias { get; set; }
        public ICollection<Catalogo> Catalogos { get; set; }
    }
}
