Feature: nTestRunner Program Startup
	As a developer
	In order to get rapid feedback
	When I save a file, the program should be compiled and all tests run and the results
	stored in a file in the same format as nunit, so I can use beacons to view the results of the 
	test.

	This feature covers the startup of the program, 
	calling nTestRunner
		starts the program up in default mode, it will scan up the directory chain looking for a solution file, starting at its current directory
		it is this file that the applicaion uses to decide what is a test project, and it is the file that is passed to msbuild, it is assumed
		that all test files in the solution want to be run, regredless of the test runner used, all results are combined into an nunit result
		format.  No display is made when the tests are run, only the file is written to the same directory as the found solution file.
		
	calling nTestRunner -Path | -P [path to solution file]
		starts the program up with the solution file path set

	calling nTestRunner -Test | -T [Test runner name]
		starts the program up with only the specified test runner

	calling nTestRunner -Display | -D [Runner Display Name]
		starts the program up with the specified display runner

Scenario: Startup without arguments
	Given that the program is not running
	When the program is run with arguments ''
	Then the user sees text containing 'nTestRunner version 1.0'
	And the user sees text containing 'Watching Files' 
	And the user sees text containing 'nTestRunner.sln'
	And the user sees text containing 'nTestRunner.features.csproj'
	And the user sees text containing 'nTestRunner.Spec.csproj'

Scenario: Startup with path arguments
	Given that the program is not running
	When the program is run with arguments '-Path,C:\Users\Duncan\Documents\My Dropbox\Dropbox\nTestRunner\nTestRunner.features\TestData\TestSolution.sln'
	Then the user sees text containing 'TestSolution.sln'
	And the user sees text containing 'TestProject1.csproj'
	And the user sees text containing 'TestProject2.csproj'

Scenario: Startup with test runner arguments
	Given that the program is not running
	When the program is run with arguments '-Test,MSpec'
	Then the user sees text containing 'Running tests with MSpec'
	
Scenario: Startup with display arguments
	Given that the program is not running
	When the program is run with arguments '-Display,Growl'
	Then the user sees text containing 'Displaying results in Growl'  


Scenario: when starting creates a blank test results file
	Given that the program is not running
	When the program is run with arguments ''
	Then a file with the name 'TestResults.xml' is created in the solution file directory
