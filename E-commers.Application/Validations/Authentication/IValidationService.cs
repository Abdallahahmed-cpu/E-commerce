using E_commers.Application.DTOS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commers.Application.Validations.Authentication
{
    public interface IValidationService
    {
        Task<ServiceResponse> ValidationAsync<T>(T model,IValidator<T> validator); 
    }
}
