﻿using WriteToCompassion.Views;
using WriteToCompassion.Views.Popups;

namespace WriteToCompassion;

public partial class AppShell : Shell
{
	public AppShell()
	{

		AppShell.InitializeRouting();
		InitializeComponent();
	}


	protected override async void OnHandlerChanged()
	{
		base.OnHandlerChanged();

		if (Handler is not null)
		{
            /*nav service was calling initialize to determine which starting page here based on tutorial preference
             * await _navigationService.InitializeAsync();
             *   from the nav service -->   public async Task InitializeAsync()
                {
                    if (_settingsService.DisplayTutorial)
                        await Shell.Current.GoToAsync(nameof(TutorialView));
                    else
                        await Shell.Current.GoToAsync(nameof(ThoughtsPage));
                }
            */
        }
    }

	// TODO: delete comment if relative routing refresh works
	private static void InitializeRouting()
	{
/*        Routing.RegisterRoute("thoughtspage", typeof(ThoughtsPage));
        Routing.RegisterRoute("newthoughteditorview", typeof(NewThoughtEditorView));
        Routing.RegisterRoute("thoughtcollectionview", typeof(ThoughtCollectionView));*/
        Routing.RegisterRoute(nameof(TutorialView), typeof(TutorialView));
        Routing.RegisterRoute(nameof(SettingsView), typeof(SettingsView));
        Routing.RegisterRoute(nameof(EditorView), typeof(EditorView));
        Routing.RegisterRoute(nameof(AddThoughtPopupView), typeof(AddThoughtPopupView));
        Routing.RegisterRoute(nameof(EditThoughtPopup), typeof(EditThoughtPopup));
    }
}
