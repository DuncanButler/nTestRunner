Feature: nTestRunner solution change
	As a developer
	In order to get rapid feedback
	When I save a file, the program should be compiled and all tests run and the results
	stored in a file in the same format as nunit, so I can use beacons to view the results of the 
	test.

	The file watcher watches the solution file, and the immediate project directories, when a change in these watched areas is detected
	then the watcher is stopped, the build and test cycle started, and the results written to the xml results file in nunit format, if the
	runner display is set then it is activated with the results from the build test cycle.
	
Scenario: with default configuration
	Given I am a developer
	When I attempt to run the test runner: with arguments ' '	
	And I attempt to change a file: source file
	Then I see text containing display text 'Running build using MSBuild4'
	And I see text containing display text 'Running tests using MSpec, MSTest, NUnit'			
	And I see text containing display text 'Waiting'