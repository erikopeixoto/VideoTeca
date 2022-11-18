using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace VideoTeca.Modelos.Modelos
{
    public class Role
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime MyProperty { get; set; }
        public List<UsuarioGrupo> UserRoles { get; set; }
    }
}