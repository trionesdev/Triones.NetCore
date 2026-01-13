namespace Triones.NetCore.Boot.Properties;

[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
public class ConfigurationPropertiesAttribute : Attribute
{
    public string? Value { get; set; }
}
