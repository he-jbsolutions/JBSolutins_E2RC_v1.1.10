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
   public class Action
    {

      public String Name ;
      public Int64  Inspection_ID ;
      public String Inspector ;
      public String ActionRequired;             
      public String Date;


      public IEnumerable<Action> getActionList(string role, long User_ID)
      {
          List<Action> ActionList = new List<Action>();
          DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getActionDetails", new object[] { "@User_ID", "@Role" }, new object[] { User_ID, role });
          if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
          {
             
              foreach (DataRow row in dataset.Tables[0].Rows)
              {
                  ActionList.Add(new Action
                  {
                      Name = Convert.ToString(row["Name"]),                      
                      Inspector = Convert.ToString(row["Inspector"]),                                       
                      Date = Convert.ToString(row["createdDate"])
                  });
              }
              return ActionList;

          }
          else
          {
              ActionList.Add(new Action
              {
                  Name = "",                  
                  Inspector = "",                 
                  Date = ""
              });
              return ActionList;
          }

      }

    }
}
