using System;
using System.IO;
using System.Linq;
using System.Text;
using MSpecCaller.Serialization;
using Machine.Specifications;
using SerializationHelpers;

namespace nTestRunner.Spec
{
    [Subject("MSpec file serialization")]
    public class calling_serialize_with_a_valid_stream : SerializeMSpecResultsFileContext
    {
        Because of =
            () =>
                {
                    _result = _serializer.ToTypeof<MSpec>(_stream);
                };

        It serializes_to_a_valid_mspec_object =
            () => _result.ShouldNotBeNull();

        It contains_six_specification_nodes =
            () => _result.Assembly[0].Concern[0].Context[0].Specification.Count.ShouldEqual(7);

        It contains_five_passing_specification_nodes =
            () =>
            (from spec in _result.Assembly[0].Concern[0].Context[0].Specification where spec.Status == "passed" select spec).
                Count().ShouldEqual(5);

        It contains_one_failing_specification_node =
            () =>
            (from spec in _result.Assembly[0].Concern[0].Context[0].Specification where spec.Status == "failed" select spec).
                Count().ShouldEqual(1);

        It contains_one_specification_that_is_not_implemeted =
            () => (from spec in _result.Assembly[0].Concern[0].Context[0].Specification
                   where spec.Status == "not-implemented"
                   select spec).Count().ShouldEqual(1);
    }

    public class SerializeMSpecResultsFileContext
    {
        Establish context =
            () =>
                {
                    string xml =
                        "<?xml version=\"1.0\" encoding=\"utf-8\"?><MSpec><run time=\"479\" /><assembly name=\"CalculatorKata.Specs\" location=\"C:\\Users\\duncan.butler\\My Dropbox\\Dropbox\\nTestRunnerTests\\CalculatorKata\\CalculatorKata.Specs\\bin\\debug\\CalculatorKata.Specs.dll\" time=\"392\"><concern name=\"Calculator\"><context name=\"Given a string containing\" type-name=\"CalculatorKata.Specs.Given_a_string_containing\"><specification name=\"nothing returns zero\" field-name=\"nothing_returns_zero\" status=\"passed\" /><specification name=\"one returns 1\" field-name=\"one_returns_1\" status=\"passed\" /><specification name=\"two returns 2\" field-name=\"two_returns_2\" status=\"passed\" /><specification name=\"one comma two returns 3\" field-name=\"one_comma_two_returns_3\" status=\"passed\" /><specification name=\"two comma three returns 5\" field-name=\"two_comma_three_returns_5\" status=\"failed\"><error><message><![CDATA[  Expected: [6]But was:  [5]]]></message><stack-trace><![CDATA[   at CalculatorKata.Specs.Given_a_string_containing.<.ctor>b__6() in c:\\Users\\duncan.butler\\My Dropbox\\Dropbox\\nTestRunnerTests\\CalculatorKata\\CalculatorKata.Specs\\CalculatorSpecifications.cs:line 22]]></stack-trace></error></specification><specification name=\"one comma two comma three returns 6\" field-name=\"one_comma_two_comma_three_returns_6\" status=\"passed\" /><specification name=\"one newline two comma three returns 6\" field-name=\"one_newline_two_comma_three_returns_6\" status=\"not-implemented\" /></context></concern></assembly></MSpec>";

                    _stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));

                    _serializer = new TheSerializer();
                };

        protected static MemoryStream _stream;
        protected static TheSerializer _serializer;
        protected static MSpec _result;
    }
}