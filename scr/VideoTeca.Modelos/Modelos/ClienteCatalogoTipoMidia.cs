using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace VideoTeca.Modelos.Modelos
{
    public class ClienteCatalogoTipoMidia
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdCatalogoTipoMidia { get; set; }
        public int QtdTitulo { get; set; }
        public DateTime? DtcLocacao { get; set; }
        public DateTime? DtcEntrega { get; set; }
        public DateTime? DtcDevolucao { get; set; }
        public DateTime? DtcAtualizacao { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }
        [ForeignKey("IdCatalogoTipoMidia")]
        public CatalogoTipoMidia CatalogoTipoMidia { get; set; }
    }
}
