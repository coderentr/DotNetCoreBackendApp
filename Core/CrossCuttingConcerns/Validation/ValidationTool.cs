﻿using Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void  Validate(IValidator validator, object entity)
        {
            var result = validator.Validate((IValidationContext)entity);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
