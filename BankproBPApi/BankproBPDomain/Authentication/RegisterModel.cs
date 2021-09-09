using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPDomain.Authentication
{
      public class RegisterModel
      {
            [Required]
            public string Account { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public string UserName { get; set; }
            [EmailAddress]
            [Required]
            public string Email { get; set; }
		public int CompanyId { get; set; }
	}
}
