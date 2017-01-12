using System;
using System.ComponentModel.DataAnnotations;

namespace e2rc.Models.Common
{
    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute(string minimum)
            : base(typeof(DateTime), minimum, DateTime.Now.ToShortDateString())
        {
        }
    }
}