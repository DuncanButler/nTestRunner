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
    [NUnit.Framework.DescriptionAttribute("nTestRunner solution change")]
    public partial class NTestRunnerSolutionChangeFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "nTestRunnerSaveFile.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "nTestRunner solution change", @"As a developer
In order to get rapid feedback
When I save a file, the program should be compiled and all tests run and the results
stored in a file in the same format as nunit, so I can use beacons to view the results of the
test.

The file watcher watches the solution file, and the immediate project directories, when a change in these watched areas is detected
then the watcher is stopped, the build and test cycle started, and the results written to the xml results file in nunit format, if the
runner display is set then it is activated with the results from the build test cycle.", GenerationTargetLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("with default configuration")]
        public virtual void WithDefaultConfiguration()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("with default configuration", ((string[])(null)));
#line 12
this.ScenarioSetup(scenarioInfo);
#line 13
 testRunner.Given("the program is running with no argument");
#line 14
 testRunner.When("a change event is received from the watcher");
#line 15
 testRunner.Then("the watcher is switched off");
#line 16
 testRunner.And("the build is triggered");
#line 17
 testRunner.And("the tests are run");
#line 18
 testRunner.And("the results are stored");
#line 19
 testRunner.And("the watcher is restarted");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("with test runner configuration set")]
        public virtual void WithTestRunnerConfigurationSet()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("with test runner configuration set", ((string[])(null)));
#line 21
this.ScenarioSetup(scenarioInfo);
#line 22
 testRunner.Given("the program is running with MSpec argument");
#line 23
 testRunner.When("a change event is received from the watcher");
#line 24
 testRunner.Then("only projects with the specified runner are tested");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("with the display runner configuration set")]
        public virtual void WithTheDisplayRunnerConfigurationSet()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("with the display runner configuration set", ((string[])(null)));
#line 26
this.ScenarioSetup(scenarioInfo);
#line 27
 testRunner.Given("the program is running with Growl argument");
#line 28
 testRunner.When("a change event is received from the watcher");
#line 29
 testRunner.Then("the runner display is called");
#line 30
 testRunner.And("the runner display is given the test results");
#line hidden
            testRunner.CollectScenarioErrors();
        }
    }
}
#endregion
