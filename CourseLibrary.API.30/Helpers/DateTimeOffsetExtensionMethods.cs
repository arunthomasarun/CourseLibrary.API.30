using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API._30.Helpers
{
    public static class DateTimeOffsetExtensionMethods
    {
        public static int GetAge(this DateTimeOffset dateTimeOffset)
        {
            var currDate = DateTime.UtcNow;
            int age = currDate.Year - dateTimeOffset.Year;

            if (currDate < dateTimeOffset.AddYears(age))
                age--;

            return age;
        }
    }
}
