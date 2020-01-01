using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ClosestPrime.Models
{
    public class NumberInput
    {
        [Required]
        [ValidInputNumber]
        public int Input { get; set; }
    }

    public class ValidInputNumber : Attribute, IModelValidator 
    {
        public string ErrorMessage { get; set; } = "Input has to be a number less than 179424692";

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {            
            var inputNumer = context.Model as int?;            
            if (!inputNumer.HasValue || inputNumer >= 179424692)
            {
                return new List<ModelValidationResult>
                {
                    new ModelValidationResult("", ErrorMessage)
                };
            }
            else
            {
                return Enumerable.Empty<ModelValidationResult>();
            }
        }
    }
}
