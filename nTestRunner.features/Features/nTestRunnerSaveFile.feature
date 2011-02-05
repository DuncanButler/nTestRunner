Feature: nTestRunner Program Startup
	As a developer
	In order to get rapid feedback
	When I save a file, the program should be compiled and all tests run and the results
	stored in a file in the same format as nunit, so I can use beacons to view the results of the 
	test.

	The file watcher watches the solution file, and the immediate project directories, when a change in these watched areas is detected
	then the watcher is stopped, the build and test cycle started, and the results written to the xml results file in nunit format, if the
	runner display is set then it is activated with the results from the build test cycle.
	
Scenario: with default configuration
	Given the program is running
	When a change event is received from the watcher
	Then the watcher is switched off
	And the build is triggered
	And the tests are run
	And the results are stored
	And the watcher is restarted

Scenario: with test runner configuration set
	Given the program is running
	When a change event is received from the watcher
	Then only projects with the specified runner are tested

Scenario: with the display runner configuration set
	Given the program is running
	When a change event is received from the watcher
	Then the runner display is called
	And the runner display is given the test results
