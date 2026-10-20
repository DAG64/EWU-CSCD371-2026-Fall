namespace Contacts;

public record Employee(int Id, FullName Name, string Department)
    : BaseContact(Id, Name)
{
    public override string ContactInfo => "Employee";
}