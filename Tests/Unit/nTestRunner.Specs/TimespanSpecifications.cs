using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace nTestRunner.Spec
{
    [Subject("Timespan assertions")]
    public class giving_a_timespan_a_seconds_value
    {
        Establish context =
            () =>
                {
                    _timeSpan = new TimeSpan(0, 0, 61);
                };

        It will_convert_to_the_appripriate_number_of_minutes =
            () => _timeSpan.Minutes.ShouldEqual(1);

        static TimeSpan _timeSpan;
    }
}
