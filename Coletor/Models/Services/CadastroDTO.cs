using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotSpotAPI.Models.Services
{
    public class CadastroDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
    }
}