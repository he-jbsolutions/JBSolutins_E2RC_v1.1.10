using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.Common;
using System.Data;
using e2rcModel.DataAccessLayer;

namespace e2rcModel.BusinessLayer
{
    public class LocationAssign : User
    {
        public long Assign_ID { get; set; }
        public long User_ID { get; set; }
        public string User { get; set; }
        public long? Inspector_ID { get; set; }
        public string InspectorName { get; set; }
        public long? Location_ID { get; set; }      
        public string LocationName { get; set; }
        public DateTime Date { get; set; }
        public Inspector inspector { get; set; }

        public LocationAssign()
        {
           // Date = DateTime.Now;
        }

        public IEnumerable<string> AutoList(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_GetInspectorName",
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

        public IEnumerable<LocationAssign> Inspectors
        {
            get
            {
                DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_InspectorName_List");

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    List<LocationAssign> inspectors = new List<LocationAssign>();

                    foreach (DataRow Row in dataSet.Tables[0].Rows)
                    {
                        inspectors.Add(new LocationAssign
                        {
                            Inspector_ID = Convert.ToInt64(Row["Inspector_ID"]),
                            Name = Convert.ToString(Row["Name"])
                        });
                    }
                    return inspectors;

                }
                return null;
            }
        }


        public IEnumerable<LocationAssign> Locations
        {
            get
            {
                DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_PMLocation_List", new object[] { "@User_ID" }, new object[] { this.User_ID });

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    List<LocationAssign> locations = new List<LocationAssign>();

                    foreach (DataRow Row in dataSet.Tables[0].Rows)
                    {
                        locations.Add(new LocationAssign
                        {
                            Location_ID = Convert.ToInt64(Row["Location_ID"]),
                            Name = Convert.ToString(Row["Name"])
                        });
                    }
                    return locations;

                }
                return null;
            }
        }

        public IEnumerable<dynamic> PMLocations(long User_ID)
        {
            List<LocationAssign> locations = new List<LocationAssign>();
            DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_PMLocationName_List", new object[] { "@User_ID" }, new object[] { User_ID });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Row in dataSet.Tables[0].Rows)
                {
                    locations.Add(new LocationAssign
                    {
                        Location_ID = Convert.ToInt64(Row["Location_ID"]),
                        Name = Convert.ToString(Row["Name"])
                    });
                }
                return locations;
            }
            else
            {
                locations.Add(new LocationAssign
                {
                    Location_ID = 0,
                    Name = ""
                });
                return locations;
            }
        }

        public IEnumerable<LocationAssign> List(long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_AssignLocation_List", new object[] { "@CreatedBy" }, new object[] { CreatedBy_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<LocationAssign> LocationAssignList = new List<LocationAssign>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    LocationAssignList.Add(new LocationAssign
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
                return LocationAssignList;
            }
            return null;
        }

        public IEnumerable<LocationAssign> List(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_AssignLocation_List",
                                                                new object[] { "@Search_By", "@CreatedBy" },
                                                                new object[] { search, CreatedBy_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<LocationAssign> LocationAssignList = new List<LocationAssign>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    LocationAssignList.Add(new LocationAssign
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
                return LocationAssignList;
            }
            return null;
        }

        public override bool Create()
        {
            return new DAL().Insert("sp_LocationAssign_CRUD",
                 new object[] {"@Action", "@Date","@Inspector_ID","@User_ID","@Location_ID"
                },
                 new object[] {Actions.INSERT.ToString(), DateTime.Now,Inspector_ID,CreatedBy_ID,Location_ID               
                });
        }

        public override bool Edit()
        {
            return new DAL().Update("sp_LocationAssign_CRUD",
                 new object[] {"@Action", "@Date","@Location_ID","@Inspector_ID","@Assign_ID"
                },
                 new object[] {Actions.UPDATE.ToString(),DateTime.Now,Location_ID,Inspector_ID,Assign_ID            
                });
        }

        public override bool Delete()
        {
            return new DAL().Delete("sp_LocationAssign_CRUD",
                                               new object[] { "@Action", "@Assign_ID" },
                                               new object[] { Actions.DELETE.ToString(), Assign_ID});
        }

        public LocationAssign Single(long Assign_ID, long CreatedBy)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_EditAssignLocation_List",
                                                               new object[] { "@Assign_ID"},
                                                               new object[] { Assign_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                return (new LocationAssign
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
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_AssignLocationwiseInspector_List", new object[] { "@User_ID" }, new object[] { User_ID });
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

    }
}
