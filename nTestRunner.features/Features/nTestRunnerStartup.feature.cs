// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.5.0.0
//      Runtime Version:4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
namespace nTestRunner.features.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.5.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("nTestRunner Program Startup")]
    public partial class NTestRunnerProgramStartupFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "nTestRunnerStartup.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "nTestRunner Program Startup", @"As a developer
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
starts the program up with the specified display runner", GenerationTargetLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Startup without arguments")]
        public virtual void StartupWithoutArguments()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Startup without arguments", ((string[])(null)));
#line 24
this.ScenarioSetup(scenarioInfo);
#line 25
 testRunner.Given("that the program is not running");
#line 26
 testRunner.When("the program is run with arguments \'\'");
#line 27
 testRunner.Then("the user sees text containing \'nTestRunner version 1.0\'");
#line 28
 testRunner.And("the user sees text containing \'Watching Files\'");
#line 29
 testRunner.And("the user sees text containing \'nTestRunner.sln\'");
#line 30
 testRunner.And("the user sees text containing \'nTestRunner.features.csproj\'");
#line 31
 testRunner.And("the user sees text containing \'nTestRunner.Spec.csproj\'");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Startup with path arguments")]
        public virtual void StartupWithPathArguments()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Startup with path arguments", ((string[])(null)));
#line 33
this.ScenarioSetup(scenarioInfo);
#line 34
 testRunner.Given("that the program is not running");
#line 35
 testRunner.When("the program is run with arguments \'-Path,C:\\Users\\Duncan\\Documents\\My Dropbox\\Dro" +
                    "pbox\\nTestRunner\\nTestRunner.features\\TestData\\TestSolution.sln\'");
#line 36
 testRunner.Then("the user sees text containing \'TestSolution.sln\'");
#line 37
 testRunner.And("the user sees text containing \'TestProject1.csproj\'");
#line 38
 testRunner.And("the user sees text containing \'TestProject2.csproj\'");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Startup with test runner arguments")]
        public virtual void StartupWithTestRunnerArguments()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Startup with test runner arguments", ((string[])(null)));
#line 40
this.ScenarioSetup(scenarioInfo);
#line 41
 testRunner.Given("that the program is not running");
#line 42
 testRunner.When("the program is run with arguments \'-Test,MSpec\'");
#line 43
 testRunner.Then("the user sees text containing \'Running tests with MSpec\'");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Startup with display arguments")]
        public virtual void StartupWithDisplayArguments()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Startup with display arguments", ((string[])(null)));
#line 45
this.ScenarioSetup(scenarioInfo);
#line 46
 testRunner.Given("that the program is not running");
#line 47
 testRunner.When("the program is run with arguments \'-Display,Growl\'");
#line 48
 testRunner.Then("the user sees text containing \'Displaying results in Growl\'");
#line hidden
            testRunner.CollectScenarioErrors();
        }
    }
}
#endregion
