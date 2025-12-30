using WSIST.Engine;

namespace WSIST.Web.Components.Pages;

public partial class Home
{
    private readonly TestManagement management = new();
    private List<Test> tests = [];
    private string? testTitle;
    private DateOnly dueDate;
    private readonly Test.Subjects selectedSubject;
    private readonly Test.TestVolume volume;
    private readonly Test.PersonalUnderstanding understanding;
    private readonly double grade;
    private Test? localTest;
    private Test? temporaryTest;
    private Modes Mode { get; set; }

    protected override void OnInitialized()
    {
        tests = management.Tests.ToList();
    }

    public enum Modes
    {
        AddTest,
        EditTest,
    }

    //Modal

    private bool showModal;

    private void OpenEditTestModal(Test test)
    {
        Mode = Modes.EditTest;
        
        temporaryTest = new Test
        {
            Id = test.Id,
            Title = test.Title,
            Subject = test.Subject,
            DueDate = test.DueDate,
            Volume = test.Volume,
            Understanding = test.Understanding,
            Grade = test.Grade
        };
        showModal = true;
    }

    public void OpenAddTestModal()
    {
        temporaryTest = new Test();
        Mode = Modes.AddTest;
        temporaryTest.DueDate = DateOnly.FromDateTime(DateTime.Today);
        showModal = true;
    }

    private void CloseModal()
    {
        showModal = false;
        Refresh();
    }

    private void ModalSubmit()
    {
        switch (Mode)
        {
            case Modes.AddTest:
            {
                management.NewTestMaker(temporaryTest.Title,
                    temporaryTest.Subject,
                    temporaryTest.DueDate,
                    temporaryTest.Volume,
                    temporaryTest.Understanding,
                    temporaryTest.Grade);
                break;
            }
            case Modes.EditTest:
            {
                management.TestEditor(
                    temporaryTest.Id,
                    temporaryTest.Title,
                    temporaryTest.Subject,
                    temporaryTest.DueDate,
                    temporaryTest.Volume,
                    temporaryTest.Understanding,
                    temporaryTest.Grade
                );
                break;
            }
        }
        CloseModal();
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
