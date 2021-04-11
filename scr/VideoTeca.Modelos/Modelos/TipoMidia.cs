using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoTeca.Modelos.Modelos
{
    public class TipoMidia
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime? DtcAtualizacao { get; set; }
        public List<CatalogoTipoMidia> CatalogoTipoMidias { get; set; }
        public ICollection<Catalogo> Catalogos { get; set; }
    }
}
