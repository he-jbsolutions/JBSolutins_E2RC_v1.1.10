using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.Common;
using System.Data;
using e2rcModel.DataAccessLayer;

namespace e2rcModel.BusinessLayer
{
    public class FranchiseAssignLocationToClient : User
    {
        public long Assign_ID { get; set; }
        public long User_ID { get; set; }
        public string User { get; set; }
        public long? Client_ID { get; set; }
        public long? Reviewer_ID { get; set; }
        public string ReviewerName { get; set; }
        public string CompanyName { get; set; }
        public long? Location_ID { get; set; }
        public string LocationName { get; set; }
        public DateTime Date { get; set; }

        public string sReviewer_ID { get; set; }
        public string sLocation_ID { get; set; }

        public IEnumerable<string> AutoList(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_GetReviewer_SearchName",
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

        public IEnumerable<FranchiseAssignLocationToClient> List(long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_FranchiseAssignLocationToClient_List", new object[] { "@CreatedBy" }, new object[] { CreatedBy_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<FranchiseAssignLocationToClient> FranchiseAssignLocationToInspectorList = new List<FranchiseAssignLocationToClient>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    FranchiseAssignLocationToInspectorList.Add(new FranchiseAssignLocationToClient
                    {
                        Assign_ID = Convert.ToInt64(row["Assign_ID"]),
                        Reviewer_ID = Convert.ToInt64(row["Reviewer_ID"]),
                        ReviewerName = Convert.ToString(row["ReviewerName"]),
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
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

        public IEnumerable<FranchiseAssignLocationToClient> List(string search, long CreatedBy_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_FranchiseAssignLocationToClient_List",
                                                                new object[] { "@Search_By", "@CreatedBy" },
                                                                new object[] { search, CreatedBy_ID });
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                List<FranchiseAssignLocationToClient> FranchiseAssignLocationToClientList = new List<FranchiseAssignLocationToClient>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    FranchiseAssignLocationToClientList.Add(new FranchiseAssignLocationToClient
                    {
                        Reviewer_ID = Convert.ToInt64(row["Reviewer_ID"]),
                        ReviewerName = Convert.ToString(row["ReviewerName"]),
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                        Assign_ID = Convert.ToInt64(row["Assign_ID"]),
                        Location_ID = Convert.ToInt64(row["Location_ID"]),
                        LocationName = Convert.ToString(row["LocationName"]),
                        User_ID = Convert.ToInt64(row["User_ID"]),
                        Date = Convert.ToDateTime(row["Date"]),
                    });
                }
                return FranchiseAssignLocationToClientList;
            }
            return null;
        }
        
        public override bool Create()
        {
            return new DAL().Insert("sp_FranchiseAssignLocationToClient_CRUD",
                 new object[] {"@Action", "@Date", "@Reviewer_ID", "@Client_ID", "@User_ID","@Location_ID"
                },
                 new object[] { Actions.INSERT.ToString(), DateTime.Now, sReviewer_ID, Client_ID, CreatedBy_ID, sLocation_ID                
                });
        }
        public override bool RemoveProjectAccess()
        {
            return new DAL().Insert("sp_FranchiseAssignLocationToClient_CRUD",
                 new object[] {"@Action", "@Date", "@Reviewer_ID", "@Client_ID", "@User_ID","@Location_ID"
                },
                 new object[] { "REMOVE_ACCESS", DateTime.Now, sReviewer_ID, Client_ID, CreatedBy_ID, sLocation_ID                
                });
        }

        public override bool Edit()
        {
            return new DAL().Update("sp_FranchiseAssignLocationToClient_CRUD",
                 new object[] {"@Action", "@Date","@Location_ID","@Client_ID","@Assign_ID","@Reviewer_ID"
                },
                 new object[] {Actions.UPDATE.ToString(),DateTime.Now,Location_ID,Client_ID,Assign_ID,Reviewer_ID            
                });
        }

        public override bool Delete()
        {
            return new DAL().Delete("sp_FranchiseAssignLocationToClient_CRUD",
                                               new object[] { "@Action", "@Assign_ID" },
                                               new object[] { Actions.DELETE.ToString(), Assign_ID });
        }

        public FranchiseAssignLocationToClient Single(long Assign_ID, long CreatedBy)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_EditFranchiseAssignLocationToClient_List",
                                                               new object[] { "@Assign_ID" },
                                                               new object[] { Assign_ID });
            if (dataset != null && dataset.Tables[0].Rows.Count > 0 && dataset.Tables.Count > 0)
            {
                return (new FranchiseAssignLocationToClient
                {
                    Assign_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Assign_ID"]),
                    Client_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Client_ID"]),
                    Reviewer_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Reviewer_ID"]),
                    ReviewerName = Convert.ToString(dataset.Tables[0].Rows[0]["ReviewerName"]),
                    CompanyName = Convert.ToString(dataset.Tables[0].Rows[0]["CompanyName"]),
                    Location_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["Location_ID"]),
                    LocationName = Convert.ToString(dataset.Tables[0].Rows[0]["LocationName"]),
                    Date = Convert.ToDateTime(dataset.Tables[0].Rows[0]["Date"]),
                    User_ID = Convert.ToInt64(dataset.Tables[0].Rows[0]["User_ID"]),


                });
            }
            return null;
        }

        public IEnumerable<dynamic> GetReviewerDetails(long User_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_FranchiseReviewer_List", new object[] { "@User_ID" }, new object[] { User_ID });
            List<dynamic> ClientList = new List<dynamic>();
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ClientList.Add(new
                    {
                        Reviewer_ID = Convert.ToInt64(row["Reviewer_ID"]),
                        Name = Convert.ToString(row["Name"]),
                    });
                }
                return ClientList;
            }
            else
            {
                ClientList.Add(new { Client_ID = 0, CompanyName = "" });
                return ClientList;
            }
        }

        public IEnumerable<dynamic> GetReviewerClients(long User_ID, long Reviewer_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_getReviewerClients_List", new object[] { "@User_ID", "@Reviewer_ID" }, new object[] { User_ID, Reviewer_ID });
            List<dynamic> ClientList = new List<dynamic>();
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    ClientList.Add(new
                    {
                        Client_ID = Convert.ToInt64(row["Client_ID"]),
                        CompanyName = Convert.ToString(row["CompanyName"]),
                    });
                }
                return ClientList;
            }
            else
            {
                ClientList.Add(new { Client_ID = 0, CompanyName = "" });
                return ClientList;
            }
        }


        public IEnumerable<dynamic> GetReviewerClientsLocation(long User_ID,long Client_ID)
        {
            DataSet dataset = new DAL().ExecuteStoredProcedure("sp_ReviewerClientsLocation_List", new object[] { "@User_ID", "@Client_ID" }, new object[] { User_ID, Client_ID });
            List<dynamic> LocationList = new List<dynamic>();
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    LocationList.Add(new
                    {
                        Location_ID = Convert.ToInt64(row["Location_ID"]),
                        Name = Convert.ToString(row["Name"]),
                    });
                }
                return LocationList;
            }
            else
            {
                LocationList.Add(new { Location_ID = 0, Name = "" });
                return LocationList;
            }
        }

    }
}
