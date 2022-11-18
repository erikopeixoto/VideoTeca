using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace VideoTeca.Modelos.Modelos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DtcAtualizacao { get; set; }
        // public List<UserRole> UserRoles { get; set; }
    }
}