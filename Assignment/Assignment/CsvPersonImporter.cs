using System;
using System.Collections.Generic;
using System.IO;

namespace Assignment;

public sealed class CsvPersonImporter : IDisposable
{
    private readonly TextReader _reader;
    private bool _disposed;
    public CsvPersonImporter(TextReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);

        _reader = reader;
    }

    public IReadOnlyList<Person> Import(
        ImportMode mode,
        Action<InvalidRecordInfo>? invalidRecordCallback = null)
    {
        ThrowIfDisposed();

        if (mode == ImportMode.SkipInvalid &&
            invalidRecordCallback == null)
        {
            throw new ArgumentException(
                "A callback is required when using SkipInvalid mode.",
                nameof(invalidRecordCallback));
        }

        var people = new List<Person>();
        var seenIds = new HashSet<int>();

        string? header = _reader.ReadLine();

        if (header == null)
        {
            throw new InvalidDataException(
                "CSV data must contain a header row.");
        }

        string? line;
        int lineNumber = 1;

        while ((line = _reader.ReadLine()) != null)
        {
            lineNumber++;

            try
            {
                Person person = ParsePerson(
                    line,
                    lineNumber,
                    seenIds);

                people.Add(person);
            }
            catch (InvalidPersonRecordException ex)
            {
                if (mode == ImportMode.FailFast)
                {
                    throw;
                }

                invalidRecordCallback!(
                    new InvalidRecordInfo(
                        lineNumber,
                        line,
                        ex));
            }
        }

        return people;
    }

    private static Person ParsePerson(
        string line,
        int lineNumber,
        HashSet<int> seenIds)
    {
        ArgumentNullException.ThrowIfNull(line);
        ArgumentNullException.ThrowIfNull(seenIds);

        try
        {
            string[] fields = line.Split(',');

            PersonValidator.Validate(
                fields,
                seenIds);

            Address address = new Address(
                fields[4],
                fields[5],
                fields[6],
                fields[7]);

            return new Person(
                int.Parse(fields[0]),
                fields[1],
                fields[2],
                fields[3],
                address);
        }
        catch (Exception ex)
            when (ex is FormatException
               || ex is InvalidOperationException
               || ex is ArgumentException)
        {
            throw new InvalidPersonRecordException(
                "Invalid person record.",
                lineNumber,
                line,
                ex);
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(
                nameof(CsvPersonImporter));
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _reader.Dispose();
        _disposed = true;
    }
}