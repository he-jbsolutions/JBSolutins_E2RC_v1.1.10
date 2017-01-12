using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace e2rc.Models
{
    public class StormDetailsModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is require."), DisplayFormat(DataFormatString = "{0:d}")]     
        [DataType(DataType.Date)]
        public DateTime StormDate { get; set; }
        public int ParentstID { get; set; }
        public long ParentStorm_ID { get; set; }
        public int stID { get; set; }
        public string StormDuration { get; set; }
        public decimal Amount { get; set; }
        public long Weather_ID { get; set; }
        public long Storm_ID { get; set; }

        public StormDetailsModel()
        {
            //StormDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        }        
    }
}