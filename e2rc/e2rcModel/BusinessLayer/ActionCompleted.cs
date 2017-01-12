using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.DataAccessLayer;
using e2rcModel.Common;
using System.Data;
using System.Dynamic;

namespace e2rcModel.BusinessLayer
{
   public class ActionCompleted
    {
      public  string name;

      public IEnumerable<ActionCompleted> getActionMaintenanceCompleteList()
        {
            List<ActionCompleted> ActionMaintenanceCompletedList = new List<ActionCompleted>();
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getActionMaintenanceCompletedDetails");
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ActionMaintenanceCompletedList.Add(new ActionCompleted
                    {
                        name = Convert.ToString(row["Location"]),

                    });
                }
                return ActionMaintenanceCompletedList;
            }
            else
            {
                ActionMaintenanceCompletedList.Add(new ActionCompleted
                {
                    name="",

                });
            }
            return ActionMaintenanceCompletedList;
        }

   }
}
