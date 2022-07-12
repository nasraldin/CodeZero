using FluentValidation.Results;

namespace CodeZero.Domain.Messaging;

public abstract class CommandHandler
{
    protected ValidationResult ValidationResult;

    protected CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }

    protected void AddError(string mensagem)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
    }

    protected async Task<ValidationResult> Commit(IBaseDbContext context, string message)
    {
        var result = await context.SaveChangesAsync(CancellationToken.None);

        if (result == 0) AddError(message);

        return ValidationResult;
    }

    protected async Task<ValidationResult> Commit(IBaseDbContext context)
    {
        return await Commit(context, "There was an error saving data").ConfigureAwait(false);
    }
}