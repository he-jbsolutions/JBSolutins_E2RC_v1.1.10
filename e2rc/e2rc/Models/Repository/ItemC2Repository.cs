using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public class ItemC2Repository
    {
        public static IEnumerable<ItemC2Model> items
        {
            get
            {
                List<ItemC2Model> items = new List<ItemC2Model>();
                foreach (var item in new ItemC2().items)
                {
                    items.Add(new ItemC2Model { Item_ID = item.Item_ID, Name = item.Name });
                }
                return items;
            }
        }
    }
}