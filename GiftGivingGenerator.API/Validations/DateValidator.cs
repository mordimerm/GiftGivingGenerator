using System.ComponentModel.DataAnnotations;

namespace GiftGivingGenerator.API.Validations;

public class DateValidator : ValidationAttribute
{
	protected override ValidationResult IsValid(object value,
		ValidationContext validationContext)
	{
		var date = ((DateTime) value);

		if (date < DateTime.Now)
		{
			return new ValidationResult("Date must be later than now.");
		}
		
		return ValidationResult.Success;
	}
}