using Excelia.exam.contracts.common;
using FluentValidation.Results;


namespace Exelia.exam.Business.Helpers
{
    public static class ValidationErrorHelper
    {
        public static List<Error> GetErrorMessage(List<ValidationFailure> failures)
        {
            return (from error in failures select new Error(error.ErrorMessage,error.ErrorMessage)).ToList();
                    
        }
    }
}
