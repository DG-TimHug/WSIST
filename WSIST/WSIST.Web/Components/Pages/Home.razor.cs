using System.Globalization;
using WSIST.Engine;

namespace WSIST.Web.Components.Pages;

public partial class Home
{
    private readonly TestManagement management = new TestManagement();
    private List<Test> tests = [];
    private string? testTitle;
    private DateOnly dueDate;
    private Test.Subjects selectedSubject;
    private Test.TestVolume volume;
    private Test.PersonalUnderstanding understanding;
    private double grade;

    protected override void OnInitialized()
    {
        tests = management.Tests.ToList();
    }
        // Add Test Modal
        private bool showAddTestModal;

    private void OpenAddTestModal()
    {
        testTitle = "";
        dueDate = DateOnly.FromDateTime(DateTime.Today);
        showAddTestModal = true;
    }

    private void CloseAddTestModal()
    {
        showAddTestModal = false;
    }

    private void AddTestSubmit()
    {
        management.NewTestMaker(testTitle, selectedSubject, dueDate, volume, understanding, grade);
        CloseAddTestModal();
        Refresh();
    }
    
    //Edit Test Modal

    private bool showEditTestModal;
    private Test? localTest;

    private void OpenEditTestModal()
    {
        showEditTestModal = true;
    }

    private void OpenEdit(Test test)
    {
        localTest = new Test
        {
            Id = test.Id,
            Title = test.Title,
            Subject = test.Subject,
            DueDate = test.DueDate,
            Volume = test.Volume,
            Understanding = test.Understanding,
            Grade = test.Grade
        };
        OpenEditTestModal();
    }

    private void CloseEditTestModal()
    {
        showEditTestModal = false;
    }

    private void EditTestSubmit()
    {
        management.TestEditor(localTest.Id, localTest.Title, localTest.Subject, localTest.DueDate, localTest.Volume, localTest.Understanding, localTest.Grade);
        CloseEditTestModal();
        Refresh();
    }

    private void Refresh()
    {
        management.Refresh();
        tests = management.Tests.ToList();
        StateHasChanged();
    }

    private void DeleteTest(Guid id)
    {
        management.TestRemover(id);
        Refresh();
    }
}