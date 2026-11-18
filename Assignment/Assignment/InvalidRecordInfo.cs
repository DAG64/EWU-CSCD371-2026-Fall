using System;
namespace Assignment;

public class InvalidRecordInfo
{
    public int LineNumber { get; }

    public string RawLine { get; }

    public Exception Exception { get; }

    public InvalidRecordInfo(
        int lineNumber,
        string rawLine,
        Exception exception)
    {
        ArgumentNullException.ThrowIfNull(rawLine);
        ArgumentNullException.ThrowIfNull(exception);

        LineNumber = lineNumber;
        RawLine = rawLine;
        Exception = exception;
    }
}