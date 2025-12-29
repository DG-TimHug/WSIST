using WSIST.Engine;

namespace WSIST.Web.Components.Pages;

public partial class Home
{
    private TestManagement Management = new TestManagement();
    private List<Test> tests = new();
    private string TestTitle;
    private string Subject;
    private DateTime dueDate;

    protected override void OnInitialized()
    {
        tests = Management.Tests.ToList();
    }
        // Add Test Modal
        private bool showAddTestModal = false;

    private void OpenAddTestModal()
    {
        showAddTestModal = true;
    }

    private void CloseAddTestModal()
    {
        showAddTestModal = false;
    }

    private void AddTestSubmit()
    {
        Management.NewTestMaker(TestTitle, Subject, dueDate);
        CloseAddTestModal();
        Refresh();
    }
    
    //Delete Test Modal

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
        Management.TestEditor(localTest.Id, localTest.Title, localTest.Subject, localTest.DueDate);
        CloseAddTestModal();
        Refresh();
    }

    private void Refresh()
    {
        Management.Refresh();
        tests = Management.Tests.ToList();
        StateHasChanged();
    }

    private void DeleteTest(Guid ID)
    {
        Management.TestRemover(ID);
        Refresh();
    }
}