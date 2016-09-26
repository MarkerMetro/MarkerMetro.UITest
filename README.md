
## Getting Started

MarkerMetro.UITest is a library to support writing Xamarin UI tests (for running locally or on Xamarin Test Cloud) using "Screen Objects" in a fluent style.

The resulting tests are easier to follow, cleaner and more maintainable:
~~~
Application.NavigateToHomeScreen()
            .TapSearch()
            .EnterSearchTerm("bananas")
            .SelectResult(0)
~~~

Refer to the follow articles for background of this style of test:
https://danatxamarin.com/2015/05/12/building-a-scalable-test-suite-with-xamarin-uitest-and-page-objects/
http://www.seleniumhq.org/docs/06_test_design_considerations.jsp
http://martinfowler.com/bliki/PageObject.html
http://automatetheplanet.com/page-object-pattern/

![build status](http://alice.markermetro.com/app/rest/builds/buildType:(id:MarkerMetroUITest_CI)/statusIcon) **CI**

## Usage

### Set up
* Create (in Visual Studio or Xamarin Studio) a new "UI Test App" solution. Create this as a subdirectory under the application you intend to test
* Add the Nuget package ```MarkerMetro.UITest.Framework```
* Create a class called ```BaseTest``` (or similar) that inherits from ```MarkerMetro.UITest.Framework.BaseTest```
* Override ```ApkPath``` and/or ```BundlePath```. This file is relative to the UITest bin folder

### Writing tests
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
* Use ```With``` to call a method on a screen object that is not fluent. This is particularly useful for assertions, for example 
~~~
.With((dashboard) => Assert.AreEqual(balance, dashboard.GetBalance()));
~~~
* For each screen (or use a base Screen class), override ```ProgressIndicatorId``` to force a wait for that element to not be present before the test continues. This is useful for automated waiting for loading spinners, ect
* For each screen (or use a base Screen class), override ```WaitForId``` to force a wait for that element to be present before the test continues. This can be useful is screens load asnychronously and elements should be present before continuing

