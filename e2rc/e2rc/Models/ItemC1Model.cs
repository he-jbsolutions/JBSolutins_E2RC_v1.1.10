using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e2rc.Models
{
    public class ItemC1Model
    {
        public int Item_ID { get; set; }
        public string Name { get; set; }
        
        public ItemC1Model()
        {
            Item_ID = 0;
            Name = string.Empty;
        }
    }
}