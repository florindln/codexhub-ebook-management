﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Dtos
{
    public class UpdateUserDto
    {
        public string Email { get; set; }

        public List<string> Interests { get; set; }
    }
}
