using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public class ItemC1Repository
    {
        public static IEnumerable<ItemC1Model> items
        {
            get
            {
                List<ItemC1Model> items = new List<ItemC1Model>();
                foreach (var item in new ItemC1().items)
                {
                    items.Add(new ItemC1Model { Item_ID = item.Item_ID, Name = item.Name });
                }
                return items;
            }
        }      
    }
}