using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e2rc.Models
{
    public class SiteClassificationModel
    {
        public int CodeId { get; set; }
        public int CodeTypeId { get; set; }
        public string Description { get; set; }
        public SiteClassificationModel()
        {
            CodeId = 0;
            CodeTypeId = 0;
            Description = string.Empty;
        }
    }
}