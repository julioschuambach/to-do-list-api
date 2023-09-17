using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ToDoList.Api.Extensions;

public static class ModelStateExtension
{
    public static List<string> GetErrors(this ModelStateDictionary modelState)
    {
        List<string> errors = new();

        foreach (var value in modelState.Values)
            errors.AddRange(value.Errors.Select(x => x.ErrorMessage));

        return errors;
    }
}
