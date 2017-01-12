using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e2rc.Models
{
    public class ItemC2Model
    {
        public int Item_ID { get; set; }
        public string Name { get; set; }
        public ItemC2Model()
        {
            Item_ID = 0;
            Name = string.Empty;
        }
    }
}