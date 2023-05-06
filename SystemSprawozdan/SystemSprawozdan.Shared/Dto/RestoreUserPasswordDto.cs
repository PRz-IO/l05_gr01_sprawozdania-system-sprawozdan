using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemSprawozdan.Shared.Dto
{
    public class RestoreUserPasswordDto
    {
        [Required(ErrorMessage = "Pole wymagane")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Pole wymagane")]
        public string Email { get; set; }
    }
}
