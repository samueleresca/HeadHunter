using System.Threading.Tasks;
using FluentValidation;
using HeadHunter.API.Services;

namespace HeadHunter.API.Infrastructure.Requests.Service.Validation
{
	public class CreateSubjectRequestValidator : AbstractValidator<CreateSubjectRequest>
	{
		private readonly ISubjectService _service;

		public CreateSubjectRequestValidator(ISubjectService service)
		{
			_service = service;

			RuleFor(entity => entity.Name).NotEmpty().WithMessage("{PropertyName} must not be null");
			RuleFor(entity => entity.Email)
				.Must((entity, name) => IsUnique(entity.Email).Result)
				.WithMessage("Name and Version must be unique");
		}

		private async Task<bool> IsUnique(string email)
		{
			var entity = await _service.GetByEmail(email);
			return entity == null;
		}
	}
}