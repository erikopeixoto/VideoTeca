using System.Collections.Generic;

namespace VideoTeca.Servicos.Dtos
{
    public class CatalogoDto
    {
        public int Id { get; set; }
        public int IdGenero { get; set; }
        public string NomAutor { get; set; }
        public string DesTitulo { get; set; }
        public string AnoLancamento { get; set; }
        public string DesGenero { get; set; }
        public string Codigo { get; set; }
        public string DtcAtualizacao { get; set ; }
        public virtual List<CatalogoTipoMidiaDto> CatalogoTipoMidiasDto { get; set; }
    }
}
