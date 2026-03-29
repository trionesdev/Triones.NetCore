using System.Reflection;
using Autofac.Core;

namespace Triones.Extensions.beans;

public class AutowiredSelector : IPropertySelector
{
    public bool InjectProperty(PropertyInfo propertyInfo, object instance)
    {
        var  res = propertyInfo.GetCustomAttributes(true).Any(attr =>
            attr is AutowiredAttribute || attr.GetType().IsDefined(typeof(AutowiredAttribute), true));
        if (res)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}