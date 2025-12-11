using Payroc.TestFunctional.Extensions;
using Payroc.TestFunctional.Json;
using Payroc.TestFunctional.Validation;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Payroc.TestFunctional;

public static class Data
{
    /// <summary>
    /// Gets an instance of data based on a JSON file named after the type T's name.
    /// </summary>
    public static T Get<T>(
        (Expression<Func<T, object?>> Member, object? Value)[]? propertyValueOverrides = null)
        => Get<T>(typeof(T).Name, propertyValueOverrides);

    /// <summary>
    /// Gets an instance of data based on a named JSON file.
    /// </summary>
    public static T Get<T>(
        string filename,
        (Expression<Func<T, object?>> Member, object? Value)[]? propertyValueOverrides = null)
    {
        ThrowIf.IsNullOrEmpty(filename);
        string path = filename.AsTestDataFilePath();
        ThrowIf.FileNotFound(path);

        var json = File.ReadAllText(path);
        var obj = JsonSerializer.Deserialize<T>(json, JsonOptions.RelaxedDeserialization)
            ?? throw new Exception("Failed to deserialize from JSON file.");

        foreach (var (memberExpr, value) in propertyValueOverrides ?? [])
        {
            SetMemberValue(obj, memberExpr, value);
        }

        PopulateDefaultValuesForRequiredProperties(obj);
        return obj;
    }

    private static void SetMemberValue<T>(
        T target,
        Expression<Func<T, object?>> memberExpr,
        object? value)
    {
        MemberExpression member;

        if (memberExpr.Body is MemberExpression m)
        {
            member = m;
        }
        else if (memberExpr.Body is UnaryExpression unary && unary.Operand is MemberExpression inner)
        {
            member = inner;
        }
        else
        {
            throw new ArgumentException("Expression must target a property or field.");
        }

        if (member.Member is not PropertyInfo prop)
        {
            throw new ArgumentException("Expression must target a property.");
        }

        if (!prop.CanWrite)
        {
            throw new InvalidOperationException(
                "Property '" + prop.Name + "' does not have a setter and cannot be written.");
        }

        var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

        // If value is a string and property is not a string, convert
        if (value is string s && targetType != typeof(string))
        {
            value = Convert.ChangeType(s, targetType);
        }
        else if (value != null && !targetType.IsAssignableFrom(value.GetType()))
        {
            // Attempt conversion if types are not directly compatible
            value = Convert.ChangeType(value, targetType);
        }

        prop.SetValue(target, value);
    }

    private static void PopulateDefaultValuesForRequiredProperties<T>(T obj)
    {
        var type = typeof(T);

        foreach (var prop in type.GetProperties(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            var hasRequiredMarker =
                prop.GetCustomAttributes(
                    typeof(CompilerFeatureRequiredAttribute), true).FirstOrDefault()
                        is CompilerFeatureRequiredAttribute reqAttr
                        && reqAttr.FeatureName == "RequiredMembers";

            if (hasRequiredMarker && prop.CanWrite)
            {
                var current = prop.GetValue(obj);
                var propType = prop.PropertyType;
                bool isDefault = current == null || (propType.IsValueType && Activator.CreateInstance(propType)?.Equals(current) == true);

                if (isDefault)
                {
                    object? defaultValue = propType.IsValueType
                        ? Activator.CreateInstance(propType)
                        : null;

                    prop.SetValue(obj, defaultValue);
                }
            }
        }
    }
}
