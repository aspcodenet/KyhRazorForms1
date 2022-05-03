using System.ComponentModel.DataAnnotations;

namespace SkysFormsDemo.Infrastructure.Val;

public class GoodHockeyYear2Attribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        int year = int.Parse(value.ToString());
        var goodyears = new[] { 1953, 1957, 1962, 1987, 1991, 1992, 1998, 2006, 2013 };
        return goodyears.Contains(year);
    }
}