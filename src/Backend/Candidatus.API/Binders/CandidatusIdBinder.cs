using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sqids;

namespace Candidatus.API.Binders;

public class CandidatusIdBinder : IModelBinder
{
    private readonly SqidsEncoder<int> _idEncoder;

    public CandidatusIdBinder(SqidsEncoder<int> encoder)
    {
        _idEncoder = encoder;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var modelName = bindingContext.ModelName;

        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
            return Task.CompletedTask;

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var value = valueProviderResult.FirstValue;

        if (string.IsNullOrWhiteSpace(value))
            return Task.CompletedTask;

        var id = _idEncoder.Decode(value).Single();

        bindingContext.Result = ModelBindingResult.Success(id);

        return Task.CompletedTask;
    }
}