# Writing Xamarin UI Tests

## Set up

# Basics
* Create (in Visual Studio or Xamarin Studio) a new "UI Test App" solution. Create this as a subdirectory under the application you intend to test
* Ensure alice.markermetro.com is set up as a Nuget server on your machine
* Add the Nuget package ```MarkerMetro.UITest.Framework```
* Create a class called ```BaseTest``` (or similar) that inherits from ```MarkerMetro.UITest.Framework.BaseTest```
* Override ```ApkName```. This file will be expected in the same directory as the solution

# Writing the first test
* Create a unit test class, and inherit from your ```BaseTest```
* Running an empty test should run the application (either device or emulator) and pass the test
* In a new folder called ```Screens``` create a class called ```Application``` that inherits from ```MarkerMetro.UITest.Framework.BaseScreen```, ie ```Application : BaseControl<Application>```
* Add the necessary constructor
* Add a property to your base test that returns an instance of Application
* Add methods to ```Application``` that navigate to a new screens etc. For these screen, create another class in the same way as ```Application```
* The methods on the screen class should return either a new screen (using ```Create<T>```) or themselves (using ```CreateThis```). This chain will produce a fluent interface, for example
~~~
Application.NavigateToHomeScreen()
            .TapSearch()
            .EnterSearchTerm("bananas")
            .SelectResult(0)
~~~
* The contents of these methods will be calls to the ```app``` object. Refer to Xamarin UI Test documentation for the different options. Also read "Using Xamarin Test Recorder" below

## Techniques when creating tests

### Using Repl
* Insert the line ```app.Repl()``` at around the point you are interested to explore the screen
* When the debugger hits this line, a terminal window will open
* Type ```tree``` at the command line. This allows the structure of the screen to be looked at

### Using Xamarin Test Recorder
* Download and install
* Set up test recorder to point to the app in question
* Record while interacting - this gives the basic behaviour of what is being clicked

## Test Cloud
