using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.BusinessLayer.Interface;
using System.Data;
using e2rcModel.DataAccessLayer;
using e2rcModel.Common;

namespace e2rcModel.BusinessLayer
{
    public class SiteInspection : ICRUD<SiteInspection, byte>
    {
        public long Inspection_ID { get; set; }
        public long InspectionQuestion_ID { get; set; }
        public bool? AreaInspected { get; set; }
        public bool AreaInspectedYes { get; set; }
        public bool AreaInspectedNA { get; set; }
        public bool AreaInspectedNo { get; set; }
        public bool ActionRequired { get; set; }
        public bool ActionRequiredYes { get; set; }
        public bool ActionRequiredNo { get; set; }
        public string Notes { get; set; }
        public string Question { get; set; }
        public long SiteInspection_ID { get; set; }

        public SiteInspection()
        {
            Inspection_ID = 0;
            InspectionQuestion_ID = 0;
            AreaInspected = true;
            AreaInspectedYes = false;
            AreaInspectedNA = false;
            AreaInspectedNo = false;
            ActionRequired = true;
            ActionRequiredYes = false;
            ActionRequiredNo = false;
            Notes = string.Empty;
            Question = string.Empty;
            SiteInspection_ID = 0;
        }

        public bool Create()
        {
            throw new NotImplementedException();
        }

        public bool Edit()
        {
            if (this.AreaInspected == null)
                return new DAL().Update("sp_SiteInspection_CRUD",
                                       new object[] { "@Action", "@SiteInspection_ID", "@ActionRequired", "@Notes" },
                                       new object[] { Actions.UPDATE.ToString(), SiteInspection_ID, ActionRequired, Notes }
                                       );
            else
                return new DAL().Update("sp_SiteInspection_CRUD",
                                   new object[] { "@Action", "@SiteInspection_ID", "@AreaInspected", "@ActionRequired", "@Notes" },
                                   new object[] { Actions.UPDATE.ToString(), SiteInspection_ID, AreaInspected, ActionRequired, Notes }
                                   );
        }
        internal bool Create(long Inspection_ID)
        {
            if (this.AreaInspected == null)
                return new DAL().Insert("sp_SiteInspection_CRUD",
                new object[] { "@Action", "@Inspection_ID", "@InspectionQuestion_ID", "@ActionRequired", "@Notes" },
                new object[] { Actions.INSERT.ToString(), Inspection_ID, InspectionQuestion_ID, ActionRequired, Notes });
            else
                return new DAL().Insert("sp_SiteInspection_CRUD",
                     new object[] { "@Action", "@Inspection_ID", "@InspectionQuestion_ID", "@AreaInspected", "@ActionRequired", "@Notes" },
                     new object[] { Actions.INSERT.ToString(), Inspection_ID, InspectionQuestion_ID, AreaInspected, ActionRequired, Notes });
        }
        public bool Delete()
        {
            throw new NotImplementedException();
        }
        public SiteInspection Single(byte value)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<SiteInspection> List()
        {
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_SiteInspectionQuestions");
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                List<SiteInspection> SiteInspectionList = new List<SiteInspection>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    SiteInspectionList.Add(new SiteInspection
                    {
                        InspectionQuestion_ID = Convert.ToInt64(row["InspectionQuestion_ID"]),
                        Question = Convert.ToString(row["Question"])
                    });
                }
                return SiteInspectionList;
            }
            return null;
        }
    }
}
