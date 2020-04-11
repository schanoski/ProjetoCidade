using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CidadeWeb.Models
{
    public class Usuario
    {
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}