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
	Given I am a developer
	When I attempt to start the test runner: with arguments ' '
	Then I see text containing display text 'nTestRunner.sln'
	And I see text containing display text 'nTestRunner.features.csproj'
	And I see text containing display text 'nTestRunner.Spec.csproj'
	And I see text containing display text 'Will build solution using MSBuild4'
	And I see text containing display text 'Will run tests using MSpec, MSTest, NUnit'
	And I see text containing display text 'Will display results using Form'

Scenario: Startup with path arguments
	Given I am a developer
	When I attempt to start the test runner: with arguments '-Path,{currentDirectory}\TestData\TestSolution.sln'
	Then I see text containing display text 'TestSolution.sln'
	And I see text containing display text 'TestProject1.csproj'
	And I see text containing display text 'TestProject2.csproj'

Scenario: Startup with text runner arguments
	Given I am a developer
	When I attempt to start the test runner: with arguments '-Test,MSpec'
	Then I see text containing display text 'Will run tests using MSpec'
	
Scenario: Startup with display arguments
	Given I am a developer
	When I attempt to start the test runner: with arguments '-Display,Growl'
	Then I see text containing display text 'Will display results using Growl'

Scenario: Startup with builder arguments
	Given I am a developer
	When I attempt to start the test runner: with arguments '-Builder,MSBuild35'
	Then I see text containing display text 'Will build solution using MSBuild35'

Scenario: when starting creates a blank test results file
	Given I am a developer
	When I attempt to start the test runner: with arguments ' '
	Then I should see the results file 'TestResult.xml'
		