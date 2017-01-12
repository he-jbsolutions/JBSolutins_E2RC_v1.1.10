using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using e2rcModel.DataAccessLayer;

namespace e2rcModel.BusinessLayer
{
    public class SiteClassification
    {
        public int CodeId { get; set; }
        public string Description { get; set; }
        public int CodeTypeId { get; set; }

        public IEnumerable<SiteClassification> items
        {
            get
            {
                DataSet dataSet = new DAL().ExecuteStoredProcedure("usp_query_get code, 1");

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    List<SiteClassification> list = new List<SiteClassification>();

                    foreach (DataRow Row in dataSet.Tables[0].Rows)
                    {
                        list.Add(new SiteClassification
                        {
                            CodeId = Convert.ToInt32(Row["CodeId"]),
                            CodeTypeId = Convert.ToInt32(Row["CodeTypeId"]),
                            Description = Convert.ToString(Row["Description"])
                        });
                    }
                    return list;
                }
                return null;
            }
        }
    }
}
