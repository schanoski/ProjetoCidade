using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CidadeWeb.Models
{
    public class Cidade
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
    }
}