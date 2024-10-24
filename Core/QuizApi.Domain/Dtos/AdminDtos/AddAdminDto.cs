using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Domain.Dtos.AdminDtos
{
    public class AddAdminDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
