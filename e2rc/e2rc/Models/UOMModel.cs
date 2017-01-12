using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e2rc.Models
{
    public class UOMModel
    {
        public int UOM_ID { get; set; }
        public string UOM { get; set; }
        public UOMModel()
        {
            UOM_ID = 0;
            UOM = string.Empty;
        }
    }
}