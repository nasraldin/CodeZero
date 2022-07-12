using System.ComponentModel;
using AutoMapper;

namespace CodeZero.AutoMapper;

public static class IgnoreNoMapExtensions
{
    public static IMappingExpression<TSource, TDestination> IgnoreNoMap<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> expression)
    {
        var sourceType = typeof(TSource);

        foreach (var property in sourceType.GetProperties().Select(p => p.Name))
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property]!;
            NoMapAttribute attribute = (NoMapAttribute)descriptor.Attributes[typeof(NoMapAttribute)]!;

            if (attribute != null)
                expression.ForMember(property, opt => opt.Ignore());
        }

        return expression;
    }
}

public class NoMapAttribute : Attribute { }
