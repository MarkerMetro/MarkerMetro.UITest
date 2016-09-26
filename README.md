
![build status](http://alice.markermetro.com/app/rest/builds/buildType:(id:MarkerMetroUITest_Release)/statusIcon) **Release**

## Writing Tests

### Basics
* Create (in Visual Studio or Xamarin Studio) a new "UI Test App" solution. Create this as a subdirectory under the application you intend to test
* Add the Nuget package ```MarkerMetro.UITest.Framework```
* Create a class called ```BaseTest``` (or similar) that inherits from ```MarkerMetro.UITest.Framework.BaseTest```
* Override ```ApkPath``` and/or ```BundlePath```. This file is relative to the UITest bin folder

### Writing the first test
* Create a unit test class, and inherit from your ```BaseTest```
* Running an empty test should run the application (either device or emulator) and pass the test
* In a new folder called ```Screens``` create a class called ```Application``` that inherits from ```MarkerMetro.UITest.Framework.BaseScreen```, ie ```Application : BaseControl<Application>```
* Add the necessary constructor
* Add a property to your base test that returns an instance of Application
* Add methods to ```Application``` that navigate to a new screens etc. For these screen, create another class in the same way as ```Application```
* The methods on these screen classes should return either a new screen (using ```Create<T>```) or themselves (using ```CreateThis```). This chain will produce a fluent interface, for example
~~~
Application.NavigateToHomeScreen()
            .TapSearch()
            .EnterSearchTerm("bananas")
            .SelectResult(0)
~~~
* The contents of these methods will be calls to the ```app``` object. Refer to Xamarin UI Test documentation for the different options. Also read "Using Xamarin Test Recorder" below

