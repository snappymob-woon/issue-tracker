using System;
using System.Reflection;

namespace IssueTracker.Atrributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class ColorAttribute : Attribute
{
    public string Color { get; set; }

    public ColorAttribute(string colour)
    {
        Color = colour;
    }
}

public enum BootstrapColor
{
    [Color("primary")]
    Primary,

    [Color("secondary")]
    Secondary,

    [Color("success")]
    Success,

    [Color("danger")]
    Danger,

    [Color("warning")]
    Warning,

    [Color("info")]
    Info,

    [Color("light")]
    Light,

    [Color("dark")]
    Dark
}



public static class ColorHelper
{
    public static string GetColorAttributeValue<T>(T enumValue) where T : Enum
    {
        FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        if (fieldInfo == null)
        {
            throw new ArgumentException("Enum member not found.");
        }

        ColorAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(ColorAttribute), false) as ColorAttribute[];

        if (attributes == null || attributes.Length == 0)
        {
            throw new ArgumentException("ColorAttribute not found on enum member.");
        }

        return attributes[0].Color;
    }
}