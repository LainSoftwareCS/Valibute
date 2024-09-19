using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Valibute.Attributes;
using Valibute.Models;
using Valibute.Models.Responses;
using EmailValidation;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Valibute.Utils
{

    public static class ValibuteUtils
    {
        private static List<Type> validNumberTypes = new List<Type>
            {
                typeof(int),
                typeof(double),
                typeof(float),
                typeof(decimal)
            };
        private static void RequiredValidation<T>(T entity, VValidProperty vValidProperty, PropertyInfo prop, ref List<ValidationError> errors) {
            if (prop.GetValue(entity) == null)
            {
                errors.Add(new ValidationError(vValidProperty));
            }
        }

        private static void NotEmptyValidation<T>(T entity, VNotEmpty vValidProperty, PropertyInfo prop, ref List<ValidationError> errors)
        {
            RequiredValidation(entity, vValidProperty, prop, ref errors);
            if (string.IsNullOrEmpty(prop.GetValue(entity)!.ToString().Trim()))
            {
                errors.Add(new ValidationError(vValidProperty));
            }
        }
        private static void NumberValidation<T>(T entity, VNumber numberAttribute, PropertyInfo prop, ref List<ValidationError> errors)
        {
            if (!validNumberTypes.Any(x => x == prop.PropertyType))
            {
                errors.Add(new ValidationError(numberAttribute));
            }

            RequiredValidation(entity, numberAttribute, prop, ref errors);
            ValidateNumberValue(prop.GetValue(entity)!, numberAttribute, ref errors);
        }
        private static void EmailValidation<T>(T entity, VEmail emailAttribute, PropertyInfo prop, ref List<ValidationError> errors)
        {
            var value = prop.GetValue(entity);
            if (value == null)
            {
                return;
            }
            if (!EmailValidator.Validate(value.ToString()))
            {
                errors.Add(new ValidationError(emailAttribute));
            }
        }
        private static void ItemsValidation<T>(T entity, VItems itemsAttribute, PropertyInfo prop, ref List<ValidationError> errors)
        {
            var value = prop.GetValue(entity);
            if (value == null)
            {
                return;
            }

            if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
            {
                var enumerableLength = ((IEnumerable<object>)value).Count();
                if (itemsAttribute.MinLength > enumerableLength)
                {
                    errors.Add(new ValidationError(itemsAttribute.Name, "The length is below than the minimum value"));
                }
                if (itemsAttribute.MaxLength < enumerableLength)
                {
                    errors.Add(new ValidationError(itemsAttribute.Name, "The length is higher than the max value"));
                }
                if (itemsAttribute.ValidateEachItem)
                {
                    var itemType = ((IEnumerable)value).GetType().GetGenericArguments().Single();
                    foreach (object item in (IEnumerable)value)
                    {
                        var response = Validate(item, itemType);
                        if (response.Errors != null)
                        {
                            errors.AddRange(response.Errors.Select(x => new ValidationError()
                            {
                                PropName = itemsAttribute.Name + "." + x.PropName,
                                Error = x.Error
                            }));
                        }
                    }
                }
            }
            else
            {
                errors.Add(new ValidationError(itemsAttribute.Name, $"The property {itemsAttribute.Name} is not a IEnumerable or an Array"));
            }            
        }
        private static void ValidateNumberValue(object value, VNumber numberAttribute, ref List<ValidationError> errors)
        {
            if (value is int)
            {
                if ((int)value < numberAttribute.Min)
                {
                    errors.Add(new ValidationError(numberAttribute.Name, string.IsNullOrEmpty(numberAttribute.ErrorMessage)
                        ? numberAttribute.ErrorMessage : $"The {numberAttribute.Name} is less than {numberAttribute.Min}"));
                }
                if ((int)value > numberAttribute.Max)
                {
                    errors.Add(new ValidationError(numberAttribute.Name, string.IsNullOrEmpty(numberAttribute.ErrorMessage) 
                        ? numberAttribute.ErrorMessage : $"The {numberAttribute.Name} is bigger than {numberAttribute.Max}"));
                }
                return;
            }
            else if (value is float)
            {
                if ((float)value < numberAttribute.Min)
                {
                    errors.Add(new ValidationError(numberAttribute.Name, string.IsNullOrEmpty(numberAttribute.ErrorMessage)
                        ? numberAttribute.ErrorMessage : $"The {numberAttribute.Name} is less than {numberAttribute.Min}"));
                }
                if ((float)value > numberAttribute.Max)
                {
                    errors.Add(new ValidationError(numberAttribute.Name, string.IsNullOrEmpty(numberAttribute.ErrorMessage)
                        ? numberAttribute.ErrorMessage : $"The {numberAttribute.Name} is bigger than {numberAttribute.Max}"));
                }
                return;
            }
            else if (value is decimal)
            {
                if ((decimal)value < numberAttribute.Min)
                {
                    errors.Add(new ValidationError(numberAttribute.Name, string.IsNullOrEmpty(numberAttribute.ErrorMessage)
                        ? numberAttribute.ErrorMessage : $"The {numberAttribute.Name} is less than {numberAttribute.Min}"));
                }
                if ((decimal)value > numberAttribute.Max)
                {
                    errors.Add(new ValidationError(numberAttribute.Name, string.IsNullOrEmpty(numberAttribute.ErrorMessage)
                        ? numberAttribute.ErrorMessage : $"The {numberAttribute.Name} is bigger than {numberAttribute.Max}"));
                }
                return;
            } else if (value is double)
            {
                if ((double)value < numberAttribute.Min)
                {
                    errors.Add(new ValidationError(numberAttribute.Name, string.IsNullOrEmpty(numberAttribute.ErrorMessage) 
                        ? numberAttribute.ErrorMessage : $"The {numberAttribute.Name} is less than {numberAttribute.Min}"));
                }
                if ((double)value > numberAttribute.Max)
                {
                    errors.Add(new ValidationError(numberAttribute.Name, string.IsNullOrEmpty(numberAttribute.ErrorMessage)
                        ? numberAttribute.ErrorMessage : $"The {numberAttribute.Name} is bigger than {numberAttribute.Max}"));
                }
                return;
            } 
        }
        private static void ValidateProperty<T>(this VValidProperty vValidProperty, T entity, PropertyInfo prop,
            ref List<ValidationError> errors)
        {   
            if (vValidProperty is VRequired)
            {
                RequiredValidation(entity, vValidProperty, prop, ref errors);
            }
            else if (vValidProperty is VNotEmpty)
            {
                NotEmptyValidation(entity, (VNotEmpty)vValidProperty, prop, ref errors);
            }
            else if (vValidProperty is VNumber)
            {
                NumberValidation(entity, (VNumber)vValidProperty, prop, ref errors);
            }
            else if (vValidProperty is VEmail)
            {
                EmailValidation(entity, (VEmail)vValidProperty, prop, ref errors);
            }
            else if (vValidProperty is VItems)
            {
                ItemsValidation(entity, (VItems)vValidProperty, prop, ref errors);
            }
        }
        /// <summary>
        /// Validate an entity 
        /// </summary>
        /// <param name="entity">Entity to validate</param>
        /// <typeparam name="T">Entity class to validate</typeparam>
        public static ValidationResponse Validate<T>(T entity) where T : class 
        {
            if (entity == null)
            {
                return new ErrorValidationResponse("The entity is null");
            }
            Type entityType = typeof(T);
            List<ValidationError> errors = new List<ValidationError>(); 
            foreach (var prop in entityType.GetProperties().Where(x => x.GetCustomAttributes<VValidProperty>().Any()))
            {
                var validationAttributes = prop.GetCustomAttributes<VValidProperty>().ToList();
                foreach (VValidProperty att in validationAttributes)
                {
                    att.ValidateProperty(entity,prop, ref errors);
                }
            }
            if (errors.Any())
            {
                return new ErrorValidationResponse(errors);
            }
            return new CorrectValidationResponse();
        }
        /// <summary>
        /// Validate an entity 
        /// </summary>
        /// <param name="entity">Entity to validate</param>
        /// <param name="entityType">Entity class to validate</param>
        public static ValidationResponse Validate(object entity, Type entityType)
        {
            if (entity == null)
            {
                return new ErrorValidationResponse("The entity is null");
            }
            List<ValidationError> errors = new List<ValidationError>();
            foreach (var prop in entityType.GetProperties().Where(x => x.GetCustomAttributes<VValidProperty>().Any()))
            {
                var validationAttributes = prop.GetCustomAttributes<VValidProperty>().ToList();
                foreach (VValidProperty att in validationAttributes)
                {
                    att.ValidateProperty(entity, prop, ref errors);
                }
            }
            if (errors.Any())
            {
                return new ErrorValidationResponse(errors);
            }
            return new CorrectValidationResponse();
        }
    }
}
