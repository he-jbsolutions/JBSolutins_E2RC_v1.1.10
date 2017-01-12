using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.Common;
using System.Data;
using e2rcModel.DataAccessLayer;


namespace e2rcModel.BusinessLayer
{
    public class FranchiseAssignLocationToInspector : User
    {
        public long Assign_ID { get; set; }
        public long User_ID { get; set; }
        public string User { get; set; }
        public long? Inspector_ID { get; set; }
        public string sInspector_ID { get; set; }
        public string InspectorName { get; set; }
        public long? Location_ID { get; set; }
        public string sLocation_ID { get; set; }
        public string LocationName { get; set; }
        public DateTime Date { get; set; }

        public List<long> lstInspector_ID { get; set; }
        public List<long> lstLocation_ID { get; set; }

        public IEnumerable<string> AutoList(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_GetFranchiseInspectorName",
                                                                new object[] { "@Search_By", "@CreatedBy" },
                                                                new object[] { search, CreatedBy_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                List<string> Name = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    Name.Add(Convert.ToString(row["Name"]));
                }
                return Name;
            }
            return null;
        }    

        public IEnumerable<FranchiseAssignLocationToInspector> List(long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_FranchiseAssignLocationToInspector_List", new object[] { "@CreatedBy" }, new object[] { CreatedBy_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<FranchiseAssignLocationToInspector> FranchiseAssignLocationToInspectorList = new List<FranchiseAssignLocationToInspector>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    FranchiseAssignLocationToInspectorList.Add(new FranchiseAssignLocationToInspector
                    {
                        Assign_ID = Convert.ToInt64(row["Assign_ID"]),
                        Inspector_ID = Convert.ToInt64(row["Inspector_ID"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        Location_ID = Convert.ToInt64(row["Location_ID"]),
                        LocationName = Convert.ToString(row["LocationName"]),
                        User_ID = Convert.ToInt64(row["User_ID"]),
                        Date = Convert.ToDateTime(row["Date"]),
                    });
                }
                return FranchiseAssignLocationToInspectorList;
            }
            return null;
        }

        public IEnumerable<FranchiseAssignLocationToInspector> List(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_FranchiseAssignLocationToInspector_List",
                                                                new object[] { "@Search_By", "@CreatedBy" },
                                                                new object[] { search, CreatedBy_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<FranchiseAssignLocationToInspector> FranchiseAssignLocationToInspectorList = new List<FranchiseAssignLocationToInspector>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    FranchiseAssignLocationToInspectorList.Add(new FranchiseAssignLocationToInspector
                    {
                        Inspector_ID = Convert.ToInt64(row["Inspector_ID"]),
                        InspectorName = Convert.ToString(row["InspectorName"]),
                        Assign_ID = Convert.ToInt64(row["Assign_ID"]),
                        Location_ID = Convert.ToInt64(row["Location_ID"]),
                        LocationName = Convert.ToString(row["LocationName"]),
                        User_ID = Convert.ToInt64(row["User_ID"]),
                        Date = Convert.ToDateTime(row["Date"]),
                    });
                }
                return FranchiseAssignLocationToInspectorList;
            }
            return null;
        }

        public override bool Create()
        {
            return new DAL().Insert("sp_FranchiseAssignLocationToInspector_CRUD",
                 new object[] {"@Action", "@Date", "@Inspector_ID", "@User_ID", "@Location_ID" },

                 new object[] { Actions.INSERT.ToString(), DateTime.Now, sInspector_ID, CreatedBy_ID, sLocation_ID               
            });
        }

        public override bool RemoveProjectAccess()
        {
            return new DAL().Insert("sp_FranchiseAssignLocationToInspector_CRUD",
                 new object[] { "@Action", "@Date", "@Inspector_ID", "@User_ID", "@Location_ID" },

                 new object[] { "REMOVE_ACCESS", DateTime.Now, sInspector_ID, CreatedBy_ID, sLocation_ID               
            });
        }

        public override bool Edit()
        {
            return new DAL().Update("sp_FranchiseAssignLocationToInspector_CRUD",
                 new object[] {"@Action", "@Date","@Location_ID","@Inspector_ID","@Assign_ID"
                },
                 new object[] {Actions.UPDATE.ToString(),DateTime.Now,Location_ID,Inspector_ID,Assign_ID            
                });
        }

        public override bool Delete()
        {
            return new DAL().Delete("sp_FranchiseAssignLocationToInspector_CRUD",
                                               new object[] { "@Action", "@Assign_ID" },
                                               new object[] { Actions.DELETE.ToString(), Assign_ID });
        }

        public FranchiseAssignLocationToInspector Single(long Assign_ID, long CreatedBy)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_EditFranchiseAssignLocationToInspector_List",
                                                               new object[] { "@Assign_ID" },
                                                               new object[] { Assign_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                return (new FranchiseAssignLocationToInspector
                {
                    Assign_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Assign_ID"]),
                    Inspector_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Inspector_ID"]),
                    InspectorName = Convert.ToString(dataset.Tables[0].Rows[0]["InspectorName"]),
                    Location_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Location_ID"]),
                    LocationName = Convert.ToString(dataset.Tables[0].Rows[0]["LocationName"]),
                    Date = Convert.ToDateTime(dataset.Tables[0].Rows[0]["Date"]),
                    User_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["User_ID"]),


                });
            }
            return null;
        }

        public IEnumerable<dynamic> GetInspectorDetails(long User_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_FranchiseInspector_List", new object[] { "@User_ID" }, new object[] { User_ID });
            List<dynamic> InspectorList = new List<dynamic>();
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    InspectorList.Add(new
                    {
                        Inspector_ID = Convert.ToInt64(row["Inspector_ID"]),
                        Name = Convert.ToString(row["Name"]),
                    });
                }
                return InspectorList;
            }
            else
            {
                InspectorList.Add(new { Inspector_ID = 0, Name = "" });
                return InspectorList;
            }
        }
        public IEnumerable<dynamic> GetFranchiseLocations(long User_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_FranchiseLocation_List", new object[] { "@User_ID" }, new object[] { User_ID });
            List<dynamic> LocationList = new List<dynamic>();
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    LocationList.Add(new
                    {
                        Location_ID = Convert.ToInt64(row["Location_ID"]),
                        Name = Convert.ToString(row["Name"]),
                        Client_ID = Convert.ToString(row["Client_ID"]),
                        NameLocation_ID = (row["Location_ID"]) + ",," + (row["Name"])
                    });
                }
                return LocationList;
            }
            else
            {
                LocationList.Add(new { Location_ID = 0, Name = "", Client_ID = 0});
                return LocationList;
            }
        }
    }
}
