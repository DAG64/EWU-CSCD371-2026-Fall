namespace Contacts;

public class ContactBook
{
    private HashSet<IContact> Contacts { get; } = [];

    public bool Add(IContact item)
    {
        return Contacts.Add(item);
    }

    public bool Remove(IContact item)
    {
        return Contacts.Remove(item);
    }

    public bool RemoveById(int id)
    {
        var contact = Contacts.FirstOrDefault(c => c.Id == id);
        return contact is not null && Contacts.Remove(contact);
    }

    public bool Contains(IContact item)
    {
        return Contacts.Contains(item);
    }

     public IContact[] GetAll() => Contacts.ToArray();
}
