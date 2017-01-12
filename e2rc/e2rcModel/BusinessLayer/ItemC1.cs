using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using e2rcModel.DataAccessLayer;

namespace e2rcModel.BusinessLayer
{
   public class ItemC1
    {
        public int Item_ID { get; set; }
        public string Name { get; set; }

        public IEnumerable<ItemC1> items
        {
            get
            {
                DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_ItemC1_List");

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    List<ItemC1> list = new List<ItemC1>();

                    foreach (DataRow Row in dataSet.Tables[0].Rows)
                    {
                        list.Add(new ItemC1
                        {
                            Item_ID = Convert.ToInt32(Row["Item_ID"]),
                            Name = Convert.ToString(Row["Name"])
                        });
                    }
                    return list;
                }
                return null;
            }
        }

     
    }
}
