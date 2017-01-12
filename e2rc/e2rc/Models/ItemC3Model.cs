using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e2rc.Models
{
    public class ItemC3Model
    {
        public int Item_ID { get; set; }
        public string Name { get; set; }
        public ItemC3Model()
        {
            Item_ID = 0;
            Name = string.Empty;
        }
    }
}