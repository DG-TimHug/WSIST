using Microsoft.AspNetCore.Components;
using WSIST.Engine;

namespace WSIST.Web.Components.Pages;

public partial class Home
{
    [Inject]
    private TestManagement management { get; set; } = default!;

    private List<Test> tests = [];
    private Test? temporaryTest;
    private Modes Mode { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await management.LoadAsync();
        tests = management.Tests.ToList();
    }

    public enum Modes
    {
        AddTest,
        EditTest,
    }

    // Modal

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
            Grade = test.Grade,
        };

        showModal = true;
    }

    public void OpenAddTestModal()
    {
        temporaryTest = new Test { Title = "Some Test" };
        Mode = Modes.AddTest;
        temporaryTest.DueDate = DateOnly.FromDateTime(DateTime.Today);
        showModal = true;
    }

    private async Task ModalSubmit()
    {
        if (temporaryTest is null)
            return;

        switch (Mode)
        {
            case Modes.AddTest:
                await management.NewTestMaker(
                    temporaryTest.Title,
                    temporaryTest.Subject,
                    temporaryTest.DueDate,
                    temporaryTest.Volume,
                    temporaryTest.Understanding,
                    temporaryTest.Grade
                );
                break;

            case Modes.EditTest:
                await management.TestEditor(
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

        CloseModal();
        await Refresh();
    }

    private void CloseModal()
    {
        showModal = false;
    }

    private async Task Refresh()
    {
        await management.Refresh();
        tests = management.Tests.ToList();
        StateHasChanged();
    }

    private async Task DeleteTest(Guid id)
    {
        await management.TestRemover(id);
        await Refresh();
    }
}
