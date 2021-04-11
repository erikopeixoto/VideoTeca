using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.Modelos.Dtos
{
    public class CatalogoDto
    {
        public int Id { get; set; }
        public int IdGenero { get; set; }
        public string NomAutor { get; set; }
        public string DesTitulo { get; set; }
        public string AnoLancamento { get; set; }
        public string DesGenero { get; set; }
        public string Codigo { get; set; }
    }
}
