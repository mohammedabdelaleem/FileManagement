namespace FileManagement.Api.Contract;

public class TestRecordRequestValidator : AbstractValidator<TestRecordRequest>
{
	public TestRecordRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MinimumLength(3);

		RuleFor(x => x.Age)
			.NotEmpty()
			.LessThanOrEqualTo(3);

	}
	

}
