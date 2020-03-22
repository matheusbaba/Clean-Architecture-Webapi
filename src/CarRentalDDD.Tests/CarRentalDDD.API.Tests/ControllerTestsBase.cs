using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CarRentalDDD.API.Tests
{
    public class ControllerTestsBase
    {
        /// <summary>
        /// Validate model to check if ModelState is valid
        /// </summary>
        /// <param name="objectToValidate">Object to be validated</param>
        /// <param name="controller">Controller where ModelState errors will be added</param>
        public void ValidateModelState(object objectToValidate, ControllerBase controller)
        {
            var validationContext = new ValidationContext(objectToValidate, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(objectToValidate, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }
        }
    }
}
