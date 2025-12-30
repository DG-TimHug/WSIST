namespace WSIST.Engine;

public class Test
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public Subjects Subject { get; set; }
    public DateOnly DueDate { get; set; }

    public enum Subjects
    {
        Math,
        English,
        French,
        German,
        Chemistry,
        Other
    }

    //public static void Manual()
    //{
    //    TestManagement management = new();
    //    int.TryParse(Console.ReadLine(), out var selectedOption);
    //    switch (selectedOption)
    //    {
    //        case 1:
    //            Console.WriteLine("Please enter the name of the test");
    //            var title= Console.ReadLine();
    //            Console.WriteLine("Please enter the subject of the test");
    //            var subject = Console.ReadLine();
    //            Console.WriteLine("Please enter the due date of test as DD/MM/YYYY");
    //            DateTime.TryParse(Console.ReadLine(), out var dateTime);
    //            management.NewTestMaker(title, subject, dateTime);
    //            break;
    //        case 2:
    //            Console.WriteLine("Please Enter the Id of the Test you wish to be deleted");
    //            Guid.TryParse(Console.ReadLine(), out var guid);
    //            management.TestRemover(guid);
    //            break;
    //        default:
    //            Console.WriteLine("Please choose another option.");
    //            break;
    //    }
    //    
    //}
}
