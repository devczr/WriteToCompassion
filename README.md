# WriteToCompassion
`WriteToCompassion` is an experimental note-taking app that aims to improve the helpfulness of an individual's thoughts. 
* Feeling ***upbeat*** & ***positive***? Stockpile some of that positivity by recording a few kind, self-compassionate thoughts.
* Feeling overly ***harsh*** or ***self-critical***? Read through your previously recorded thoughts to find inspiration and hopefully replace unhelpful thoughts.


# Architecture
The MVVM architectural pattern is implemented wherever possible, though some code-behind logic is necessary for the animations and popups.

### .NET MAUI Features

* .NET MAUI Standard Features
  * MVVM Data Bindings
  * Animations 
  * Custom Controls <sup>[[1]](https://github.com/devczr/WriteToCompassion/blob/ce4cfaddc0cc4fcffa9c2d39c2d0a92961b915a1/Controls/CustomCloudControl.xaml)</sup>
  * Bindable Layout <sup>[[1]](https://github.com/devczr/WriteToCompassion/blob/ce4cfaddc0cc4fcffa9c2d39c2d0a92961b915a1/Views/HomeView.xaml#L207-L226)</sup>
  * Preferences <sup>[[1]](https://github.com/devczr/WriteToCompassion/blob/ce4cfaddc0cc4fcffa9c2d39c2d0a92961b915a1/Services/Settings/SettingsService.cs) </sup> 
  
* MAUI Community Toolkit
  * IconTintColorBehavior   <sup>[[1]](https://github.com/devczr/WriteToCompassion/blob/0b6ca714d6a588acd0efddf28f8994490d41400d/Views/HomeView.xaml#L312) </sup>
 
  * EventToCommandBehavior <sup>[[1]](https://github.com/devczr/WriteToCompassion/blob/1523ad40d0bc0eac9338bbe64f51856052724110/Views/HomeView.xaml#L22) </sup>
  * Popups <sup>[[1]](https://github.com/devczr/WriteToCompassion/blob/1523ad40d0bc0eac9338bbe64f51856052724110/Views/Popups/ThemeOptionsPopup.xaml) </sup>
  * Toast <sup>[[1]](https://github.com/devczr/WriteToCompassion/blob/ce4cfaddc0cc4fcffa9c2d39c2d0a92961b915a1/ViewModels/BaseViewModel.cs#L21-L32) </sup>

* MVVM Toolkit
  * ObservableProperty <sup>[[1]](https://github.com/devczr/WriteToCompassion/blob/ce4cfaddc0cc4fcffa9c2d39c2d0a92961b915a1/ViewModels/HomeViewModel.cs#L27)</sup>
  * RelayCommand <sup>[[1]](https://github.com/devczr/WriteToCompassion/blob/a0128e695dce33a0ea9f64569f2c83aa85f6b8bf/ViewModels/HomeViewModel.cs#L287-L291)</sup>

* SQLite
  * CRUD Operations  <sup>[[1]](https://github.com/devczr/WriteToCompassion/blob/ce4cfaddc0cc4fcffa9c2d39c2d0a92961b915a1/Services/ThoughtsService.cs)</sup>

# Supported Platforms
- Android

# Licenses
## 3rd Party Licenses 
* Lottie Animation from LottieFiles under the [Lottie Simple License (FL 9.13.21)](https://lottiefiles.com/page/license)
* [.NET MAUI Community Toolkit](https://github.com/CommunityToolkit/Maui)

## Copyright and license
Code released under the MIT license.

# Acknowledgements / Helpful Resources
Huge thanks to the open source community and content creators that make learning free and easy! Here are some resources were instrumental in making this app.
>## Youtube Tutorials
> - James Montemagno's [**.NET MAUI - Full Course for Beginners**](https://youtu.be/DuNLR_NJv8U)
> - David Ortinau's [**CollectionView SelectionMode with LongPress**](https://youtu.be/As5vv40ZmsE)
> - .NET's [**.NET MAUI for Beginners**](https://youtu.be/Hh279ES_FNQ)
> - Gerald Versluis' [**Lottie Animations in MAUI**](https://youtu.be/o5X5yXdWpuc)
> - Javier Suárez's [**Ways to create controls in .NET MAUI**](https://youtu.be/8d7xLErPm9o)

>## Books / Reading Material
> - Michael Stonis' [**Enterprise Application Patterns using .NET MAUI**](https://learn.microsoft.com/en-us/dotnet/architecture/maui/)
> - Maui Community Toolkit's [**Github**](https://github.com/CommunityToolkit/Maui) &  [**Microsoft Learn**](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/)
> - MVVM Community Toolkit's [**Microsoft Learn**](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)