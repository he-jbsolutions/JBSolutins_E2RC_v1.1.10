using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public class ItemC3Repository
    {

        public static IEnumerable<ItemC3Model> items
        {
            get
            {
                List<ItemC3Model> items = new List<ItemC3Model>();
                foreach (var item in new ItemC3().items)
                {
                    items.Add(new ItemC3Model { Item_ID = item.Item_ID, Name = item.Name });
                }
                return items;
            }
        }
    }
}