namespace RESTApiVerticalSlice.Common.Logging;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Delegate | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public sealed class LogAttribute : Attribute
{
    public string Operation { get; }
    public LogLevel Level { get; }

    public LogAttribute(string operation) : this(operation, LogLevel.Information)
    {
    }

    public LogAttribute(string operation, LogLevel level)
    {
        Operation = operation;
        Level = level;
    }
}
