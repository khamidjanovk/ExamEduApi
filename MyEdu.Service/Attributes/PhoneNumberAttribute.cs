using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyEdu.Service.Attributes
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] code = { "33", "90", "91", "93", "94", "97", "98", "99", "95" };

            var phoneNumber = value as string;

            if (phoneNumber.Contains("+998") && code.Contains(phoneNumber.Substring(4, 2)))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid phone number");
        }
    }
}