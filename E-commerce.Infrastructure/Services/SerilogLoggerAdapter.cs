﻿using E_commers.Application.Service.Interfaces.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Infrastructure.Services
{
    public class SerilogLoggerAdapter<T>(ILogger logger) : IAppLogger<T>
    {
        public void LogError(Exception ex, string message)=>logger.LogError(ex, message);
       

        public void LogInformation(string message)=>logger.LogInformation(message);
       

        public void LogWarning(string message)=>logger.LogWarning(message);
       
    }
}
