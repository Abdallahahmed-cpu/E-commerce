﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commers.Application.DTOS.Identity
{
    public class CreateUser : BaseModel
    {
        public required string FullName {  get; set; }
        public required string ConfirmPassword {  get; set; }
    }
}
