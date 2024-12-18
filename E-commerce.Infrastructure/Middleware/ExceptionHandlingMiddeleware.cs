using E_commers.Application.Service.Interfaces.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddeleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext _context)
        {
            try
            {
                await _next(_context);
            }
            catch (DbUpdateException ex)
            {
                var logger = _context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddeleware>>();
                SqlException? innerException = ex.InnerException as SqlException;
                if (innerException != null)
                {
                    logger.LogError(innerException, "sql exception"); 
                    switch (innerException.Number)
                    {
                        case 2627:
                            _context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await _context.Response.WriteAsync("unique constraint violation");
                            break;
                        case 515:
                            _context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await _context.Response.WriteAsync("can not insert null");
                            break;

                        case 547:
                            _context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await _context.Response.WriteAsync("foreign key constraint violation");
                            break;

                        default:
                            _context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await _context.Response.WriteAsync("an error occurred while processing your request.");
                         break;
                    }
                }
                else
                {
                    logger.LogError(ex, "EFCORE Exception");

                    _context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await _context.Response.WriteAsync("An error occurred while saving the entity changes.");
                }
            }
            catch (Exception ex)
            {
                var logger = _context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddeleware>>();
                logger.LogError(ex, "UNKNOW Exception");

                _context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await _context.Response.WriteAsync("an error occurred:"+ex.Message);
            }
        }
    }
}
