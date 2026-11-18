using System;
using System.Collections.Generic;
namespace Assignment;
// Includes the validation rules for a wrong field count, an invalid id or duplicate id
public static class PersonValidator
{
    public static void Validate(
        string[] fields,
        HashSet<int> seenIds)
    {
        ArgumentNullException.ThrowIfNull(fields);
        ArgumentNullException.ThrowIfNull(seenIds);

        if (fields.Length != 8)
        {
            throw new FormatException(
                "Record must contain exactly 8 fields.");
        }

        if (!int.TryParse(fields[0], out int id))
        {
            throw new FormatException(
                "Id must be numeric.");
        }

        if (id <= 0)
        {
            throw new FormatException(
                "Id must be greater than zero.");
        }

        if (!seenIds.Add(id))
        {
            throw new InvalidOperationException(
                $"Duplicate Id {id}.");
        }
    }
}