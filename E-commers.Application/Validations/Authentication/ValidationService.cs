using E_commers.Application.DTOS;
using FluentValidation;

namespace E_commers.Application.Validations.Authentication
{
    public class ValidationService<T> : IValidationService
    {
        public async Task<ServiceResponse> ValidationAsync<T1>(T1 model, IValidator<T1> validator)
        {
           var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid) 
            { 
              var errors = validationResult.Errors.Select(e=>e.ErrorMessage).ToList();
                string errorsToString = string.Join("; ", errors);
                return new ServiceResponse { Message = errorsToString };
            }
            return new ServiceResponse { Success = true };
        }
    }
}
