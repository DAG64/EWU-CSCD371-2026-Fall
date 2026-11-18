using System;
namespace Assignment;
public class Person
{
    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public Address Address { get; }

    public Person(
        int id,
        string firstName,
        string lastName,
        string email,
        Address address)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id));

        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentNullException.ThrowIfNull(address);

        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
    }
}