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
    private Modes Mode { get; set; }

    protected override void OnInitialized()
    {
        tests = management.Tests.ToList();
    }

    public Home(double grade, Test.PersonalUnderstanding understanding, Test.Subjects selectedSubject,
        Test.TestVolume volume, bool showModal)
    {
        this.grade = grade;
        this.understanding = understanding;
        this.selectedSubject = selectedSubject;
        this.volume = volume;
        this.showModal = showModal;
    }

    public enum Modes
    {
        AddTest,
        EditTest,
        Off
    }


    //Modal

    private bool showModal;

    private void OpenModal(Modes modes)
    {
        showModal = true;
    }

    private void CloseModal()
    {
        showModal = false;
        Refresh();
    }


    private void OpenAddTestModal()
    {
        testTitle = "";
        dueDate = DateOnly.FromDateTime(DateTime.Today);
    }
    

    private void ModalSubmit()
    {
        switch (Mode)
        {
            case Modes.AddTest:
            {
                management.NewTestMaker(testTitle, selectedSubject, dueDate, volume, understanding, grade);
                break;
            }
            case Modes.EditTest:
            {
                management.TestEditor(
                    localTest.Id,
                    localTest.Title,
                    localTest.Subject,
                    localTest.DueDate,
                    localTest.Volume,
                    localTest.Understanding,
                    localTest.Grade
                );
                break;
            }
        }
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
