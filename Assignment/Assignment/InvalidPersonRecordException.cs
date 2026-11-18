using System;
namespace Assignment;
public class InvalidPersonRecordException : Exception
{
    public int LineNumber { get; }
    public string RawLine { get; } = string.Empty;
    public InvalidPersonRecordException()
    {
    }
    public InvalidPersonRecordException(string message) : base(message)
    {
    }
    public InvalidPersonRecordException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public InvalidPersonRecordException(string message, int lineNumber, string lineContent) : base(message)
    {
        ArgumentNullException.ThrowIfNull(lineContent);
        LineNumber = lineNumber;
        RawLine = lineContent;
    }

    public InvalidPersonRecordException(string message, int lineNumber, string lineContent, Exception innerException) : base(message, innerException)
    {
        ArgumentNullException.ThrowIfNull(lineContent);
        LineNumber = lineNumber;
        RawLine = lineContent;
    }
}