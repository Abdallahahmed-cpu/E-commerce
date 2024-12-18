using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commers.Application.DTOS
{
    public record LoginResponse
        (
        bool Success=false,
        string Message = null,
        string Token = null,
        string RefreshToken = null!
        );
    

    
}
