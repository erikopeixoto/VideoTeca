using VideoTeca.Modelos.Modelos;

namespace VideoTeca.Servicos.Dtos
{
    public class CatalogoTipoMidiaDto
    {
        public int Id { get; set; }
        public int IdCatalogo { get; set; }
        public int IdTipoMidia { get; set; }
        public int QtdTitulo { get; set; }
        public int QtdDisponivel { get; set; }
        public string Descricao { get; set; }
    }
}
