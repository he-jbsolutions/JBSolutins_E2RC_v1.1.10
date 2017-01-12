using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using e2rcModel.DataAccessLayer;

namespace e2rcModel.BusinessLayer
{
   public class UOM
    {
        public int UOM_ID { get; set; }
        public string uom { get; set; }

        public IEnumerable<UOM> UOMs
        {
            get
            {
                DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_UOM_List");

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    List<UOM> list = new List<UOM>();

                    foreach (DataRow Row in dataSet.Tables[0].Rows)
                    {
                        list.Add(new UOM
                        {
                            UOM_ID = Convert.ToInt32(Row["UOM_ID"]),                           
                            uom = Convert.ToString(Row["UOM"])
                        });
                    }
                    return list;
                }
                return null;
            }
        }
    }
}
