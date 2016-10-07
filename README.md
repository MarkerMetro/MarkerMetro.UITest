
## Getting Started

MarkerMetro.UITest is a library to support writing Xamarin UI tests (for running locally, or on Xamarin Test Cloud) using "Screen Objects" in a fluent style.

The resulting tests are easier to follow, cleaner and more maintainable:
~~~
Application.NavigateToHomeScreen()
            .TapSearch()
            .EnterSearchTerm("bananas")
            .SelectResult(0)
~~~

Refer to the following articles for some background on this style of test:

- https://danatxamarin.com/2015/05/12/building-a-scalable-test-suite-with-xamarin-uitest-and-page-objects/
- https://github.com/danwaters/vspom
- http://www.seleniumhq.org/docs/06_test_design_considerations.jsp
- http://martinfowler.com/bliki/PageObject.html
- http://automatetheplanet.com/page-object-pattern/

Refer to the Xamarin Evolve 2016 app for another example of fluent style Screen Object tests
https://github.com/xamarinhq/app-evolve

![build status](http://alice.markermetro.com/app/rest/builds/buildType:(id:MarkerMetroUITest_CI)/statusIcon) **CI**

## Usage

### Set up
* Create (in Visual Studio or Xamarin Studio) a new "UI Test App" solution. Create this as a subdirectory under the application you intend to test.
* Add the Nuget package ```MarkerMetro.UITest.Framework``` https://www.nuget.org/packages/MarkerMetro.UITest.Framework.
* Create a class called ```BaseTest``` (or similar) that inherits from ```MarkerMetro.UITest.Framework.BaseTest```.
* Override ```ApkPath``` and/or ```BundlePath```. This file is relative to the UITest bin folder, so will usually be in the form ```../../../iOS/bin/<and so on>```.


### Writing tests
* Create a unit test class, and inherit from your ```BaseTest```.
* Don't forget to attribute this class with the correct platforms and pass this through to the constructor
~~~
[TestFixture(Platform.Android)]
[TestFixture(Platform.iOS)]
public class ThisTest : BaseTest
{
            public Tests(Platform platform) :  base(platform)
            {
            }
}
~~~
* Running an empty test should run the application (either device or emulator) and pass the test.
* In a new folder called ```Screens``` create a class called ```Application``` that inherits from ```MarkerMetro.UITest.Framework.BaseScreen```, ie ```Application : BaseControl<Application>```.
* Add the necessary constructor.
* Add a property to your base test that returns an instance of ```Application```.
* Add methods to ```Application``` that navigate to new screens, etc. To model these screens, create another Screen Object class in the same way as you created ```Application```.
* In order to form the fluent interface, methods on these "Screen" classes should return either the new screen being navigated to (using ```Create<T>```) or themselves again (using ```CreateThis```). In this example the method ```NavigateToHomeScreen``` is returning ```Create<SearchScreen>(app)```.
~~~
Application.NavigateToHomeScreen()
            .TapSearch()
            .EnterSearchTerm("bananas")
            .SelectResult(0)
~~~
* The contents of these methods will be standard calls to the ```app``` object, as per any Xamarin UI test. Refer to Xamarin UI Test documentation for the different ways of interacting with the UI and fetching values.
* Use ```With``` to call a method on a Screen object that is not fluent and therefore doesn't return a Screen. This is particularly useful for assertions, for example 
~~~
.With((dashboard) => Assert.AreEqual(balance, dashboard.GetBalance()));
~~~
* For each Screen (or, alternatively, use a base Screen class), override ```ProgressIndicatorId``` to force a wait for that element to not be present before the test continues. This is useful for automated the wait required for loading spinners.
* For each Screen (or, alternatively, use a base Screen class), override ```WaitForId``` to force a wait for that element to be present before the test continues. This can be useful if screens load asnychronously and elements should be present before continuing.

