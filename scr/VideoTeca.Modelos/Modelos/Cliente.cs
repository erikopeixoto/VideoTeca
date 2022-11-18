using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace VideoTeca.Modelos.Modelos
{
    public class Cliente
    {
        //private List<ClienteCatalogoTipoMidia> _clienteCatalogoTipoMidias;
        //private ILazyLoader LazyLoader { get; set; }
        //public Cliente()
        //{
        //}

        //private Cliente(ILazyLoader lazyLoader)
        //{
        //    LazyLoader = lazyLoader;
        //}
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NumDocumento { get; set; }
        public byte TipoPessoa { get; set; }
        public string NomCliente { get; set; }
        public string NumTelefone { get; set; }
        public string DesLogradouro { get; set; }
        public string DesComplemento { get; set; }
        public string NumEndereco { get; set; }
        public string NumCep { get; set; }
        public string DesBairro { get; set; }
        public string DesMunicipio { get; set; }
        public DateTime? DtcNascimento { get; set; }
        public DateTime? DtcAtualizacao { get; set; }
        public virtual List<ClienteCatalogoTipoMidia> ClienteCatalogoTipoMidias { get; set; }
        //public List<ClienteCatalogoTipoMidia> ClienteCatalogoTipoMidias
        //{
        //    get => LazyLoader.Load(this, ref _clienteCatalogoTipoMidias);
        //    set => _clienteCatalogoTipoMidias = value;
        //}
    }
}
