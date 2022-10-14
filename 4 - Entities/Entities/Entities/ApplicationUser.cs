using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column("USR_CPF")]
        public string CPF { get; set; }

        [Column("USR_TIPO")]
        public int Tipo { get; set; }
    }
}