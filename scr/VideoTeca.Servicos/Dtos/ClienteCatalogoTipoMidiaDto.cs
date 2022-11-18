namespace VideoTeca.Servicos.Dtos
{
    public class ClienteCatalogoTipoMidiaDto
    {
        public int Id { get; set; }
        public int IdCatalogo { get; set; }
        public int IdCatalogoTipoMidia { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoMidia { get; set; }
        public int QtdTitulo { get; set; }
        public int QtdDisponivel { get; set; }
        public string AnoLancamento { get; set; }
        public string Codigo { get; set; }
        public string DesTitulo { get; set; }
        public string NomCliente { get; set; }
        public string NomAutor { get; set; }
        public string DesGenero { get; set; }
        public string DesTipoMidia { get; set; }
        public string DtcLocacao { get; set; }
        public string DtcEntrega { get; set; }
        public string DtcDevolucao { get; set; }
    }
}
