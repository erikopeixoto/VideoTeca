using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace VideoTeca.Modelos.Modelos
{
    public class Catalogo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int IdGenero { get; set; }
        public string DesTitulo { get; set; }
        public string NomAutor { get; set; }
        public string AnoLancamento { get; set; }
        public DateTime? DtcAtualizacao { get; set; }

        //public ICollection<TipoConteudo> TiposConteudos { get; set; }
    }
}
