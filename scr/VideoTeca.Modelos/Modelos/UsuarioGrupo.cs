using Microsoft.AspNetCore.Identity;
using System;

namespace VideoTeca.Modelos.Modelos
{
    public class UsuarioGrupo
    {
        public int IdUsuario { get; set; }
        public int IdGrupo { get; set; }
        public DateTime dtcAtualizacao { get; set; }
    }
}