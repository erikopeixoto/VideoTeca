using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace VideoTeca.Modelos.Modelos
{
    public class CatalogoTipoMidia
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdCatalogo { get; set; }
        public int IdTipoMidia { get; set; }
        public int QtdTitulo { get; set; }
        public int QtdDisponivel { get; set; }
        public DateTime? DtcAtualizacao { get; set; }
        [ForeignKey("IdCatalogo")]
        public Catalogo Catalogo { get; set; }
        [ForeignKey("IdTipoMidia")]
        public TipoMidia TipoMidia { get; set; }
        public List<ClienteCatalogoTipoMidia> ClienteCatalogoTipoMidias { get; set; }
    }
}
