using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotSpotAPI.Models.Models
{
    public class Cadastro
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? DtoField { get; set; }
    }
}