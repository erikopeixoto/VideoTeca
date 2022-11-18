using System.Collections.Generic;

namespace VideoTeca.Servicos.Dtos
{
    public class ErrosDto
    {
        public IEnumerable<string> Erros { get; private set; }
        public ErrosDto(IEnumerable<string> erros)
        {
            Erros = erros;
        }
    }
}
