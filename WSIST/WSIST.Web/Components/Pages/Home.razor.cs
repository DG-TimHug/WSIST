using WSIST.Engine;

namespace WSIST.Web.Components.Pages;

public partial class Home
{
    private readonly TestManagement management = new TestManagement();
    private List<Test> tests = new();
    private string testTitle;
    private string subject;
    private DateOnly dueDate;

    protected override void OnInitialized()
    {
        tests = management.Tests.ToList();
    }
        // Add Test Modal
        private bool showAddTestModal = false;

    private void OpenAddTestModal()
    {
        testTitle = "";
        subject = "";
        dueDate = DateOnly.FromDateTime(DateTime.Today);
        showAddTestModal = true;
    }

    private void CloseAddTestModal()
    {
        showAddTestModal = false;
    }

    private void AddTestSubmit()
    {
        management.NewTestMaker(testTitle, subject, dueDate);
        CloseAddTestModal();
        Refresh();
    }
    
    //Edit Test Modal

    private bool showEditTestModal = false;
    private Test? localTest;

    private void OpenEditTestModal()
    {
        showEditTestModal = true;
    }

    private void OpenEdit(Test test)
    {
        localTest = new Test()
        {
            Id = test.Id,
            Title = test.Title,
            Subject = test.Subject,
            DueDate = test.DueDate
        };
        showEditTestModal = true;
        //OpenEditTestModal();
    }

    private void CloseEditTestModal()
    {
        showEditTestModal = false;
    }

    private void EditTestSubmit()
    {
        management.TestEditor(localTest.Id, localTest.Title, localTest.Subject, localTest.DueDate);
        CloseEditTestModal();
        Refresh();
    }

    private void Refresh()
    {
        management.Refresh();
        tests = management.Tests.ToList();
        StateHasChanged();
    }

    private void DeleteTest(Guid ID)
    {
        management.TestRemover(ID);
        Refresh();
    }
}