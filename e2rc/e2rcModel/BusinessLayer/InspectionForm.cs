using System;
using System.Collections.Generic;
using System.Data;
using e2rcModel.BusinessLayer.Interface;
using e2rcModel.Common;
using e2rcModel.DataAccessLayer;

namespace e2rcModel.BusinessLayer
{
    public class InspectionForm : ICRUD<InspectionForm, long>
    {
        public long Form_ID { get; set; }
        public long CreatedBy_ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool Create()
        {
            throw new NotImplementedException();
        }

        public bool Edit()
        {
            return new DAL().Update("sp_InspectionForm_CRUD",
                  new object[] {"@Action", "@Form_ID", "@Description",
                   "@IsActive","@CreatedBy"
                },
                  new object[] {Actions.UPDATE.ToString(),Form_ID,Description,
                     IsActive,CreatedBy_ID
                });
        }

        public bool Delete()
        {
            return true;
            //return new DAL().Delete("sp_Franchise_CRUD",
            //      new object[] {"@Action", "@FranchiseID","@IsActive","@CreatedBy"
            //    },
            //      new object[] {Actions.DELETE.ToString(), Franchise_ID,IsActive, CreatedBy_ID                
            //    });
        }

        public IEnumerable<InspectionForm> List(string Name)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_InspectionForm_List", new object[] { "@Search_By" }, new object[] { Name });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<InspectionForm> InspectionFromList = new List<InspectionForm>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    InspectionFromList.Add(new InspectionForm
                       {
                           CreatedBy_ID = Convert.ToInt64(row["CreatedBy"]),
                           Name = Convert.ToString(row["Name"]),
                           IsActive = Convert.ToBoolean(row["IsActive"]),
                           Form_ID = Convert.ToInt64(row["Form_ID"]),
                           CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                           Path = Convert.ToString(row["Path"]),
                           Description = Convert.ToString(row["Description"])

                       });
                }
                return InspectionFromList;
            }
            else
            {
                return null;
            }
            
        }

        public InspectionForm Single(long value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InspectionForm> List()
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_InspectionForm_CRUD", new object[] { "@Action" }, new object[] { Actions.SELECT.ToString() });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<InspectionForm> InspectionFromList = new List<InspectionForm>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    InspectionFromList.Add(new InspectionForm
                    {
                        CreatedBy_ID = Convert.ToInt64(row["CreatedBy"]),
                        Name = Convert.ToString(row["Name"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        Form_ID = Convert.ToInt64(row["Form_ID"]),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                        Description = Convert.ToString(row["Description"]),
                        Path = Convert.ToString(row["Path"])
                    });
                }
                return InspectionFromList;
            }
            return null;
        }

        public IEnumerable<string> AutoList(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_Form_NameSearch",
                                                                new object[] { "@Search_By", "@CreatedBy" },
                                                                new object[] { search, CreatedBy_ID });

            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> FormName = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    FormName.Add(Convert.ToString(row["Name"]));
                }
                return FormName;
            }
            return null;
        }
    }
}
