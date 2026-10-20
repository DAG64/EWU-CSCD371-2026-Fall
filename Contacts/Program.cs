namespace Contacts;

public class Program
{
    public static void Main()
    {
        var book = new ContactBook();
        var nextId = 1;


        while (true)
        {
            Console.WriteLine("\n=== Contacts CLI ===");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Add Employee");
            Console.WriteLine("3. List Contacts");
            Console.WriteLine("4. Remove Contact");
            Console.WriteLine("5. Exit");
            Console.Write("Choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent(book, nextId++);
                    break;

                case "2":
                    AddEmployee(book, nextId++);
                    break;

                case "3":
                    List(book);
                    break;

                case "4":
                    Remove(book);
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    private static void AddStudent(ContactBook book, int id)
    {
        var name = ReadName();

        Console.Write("Major: ");
        var major = Console.ReadLine() ?? "";

        var student = new Student(id, name, major);

        if (!book.Add(student))
            Console.WriteLine("Duplicate student — not added.");
        else
            Console.WriteLine("Student added.");
    }

    private static void AddEmployee(ContactBook book, int id)
    {
        var name = ReadName();

        Console.Write("Department: ");
        var dept = Console.ReadLine() ?? "";

        var employee = new Employee(id, name, dept);

        if (!book.Add(employee))
            Console.WriteLine("Duplicate employee — not added.");
        else
            Console.WriteLine("Employee added.");
    }

    private static void List(ContactBook book)
    {
        var all = book.GetAll();

        if (!all.Any())
        {
            Console.WriteLine("No contacts stored.");
            return;
        }

        Console.WriteLine("\n--- Contacts ---");

        foreach (var contact in all)
        {
            var details = contact switch
            {
                Student student => $"Student — Major: {student.Major}",
                Employee employee => $"Employee — Department: {employee.Department}",
                _ => contact.ContactInfo
            };

            Console.WriteLine($"{contact.Id}: {details} — {contact.Name}");
        }
    }

    private static void Remove(ContactBook book)
    {
        Console.Write("ID to remove: ");

        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        if (book.RemoveById(id))
            Console.WriteLine("Contact removed.");
        else
            Console.WriteLine("Contact not found.");
    }

    private static FullName ReadName()
    {
        Console.Write("First: ");
        var first = Console.ReadLine() ?? "";

        Console.Write("Middle (optional): ");
        var middle = Console.ReadLine();

        Console.Write("Last: ");
        var last = Console.ReadLine() ?? "";

        return new FullName(
            first,
            last,
            string.IsNullOrWhiteSpace(middle) ? null : middle
        );
    }
}