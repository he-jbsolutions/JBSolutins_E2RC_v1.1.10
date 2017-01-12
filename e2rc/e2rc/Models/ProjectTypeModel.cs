using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e2rc.Models
{
    public class ProjectTypeModel
    {
        public long ProjectType_ID { get; set; }
        public string ProjectType { get; set; }

        public ProjectTypeModel()
        {
            ProjectType_ID = 0;
            ProjectType = string.Empty;
        }
    
    }
}