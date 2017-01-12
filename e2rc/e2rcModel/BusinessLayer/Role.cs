using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.BusinessLayer.Interface;
using e2rcModel.DataAccessLayer;
using System.Data;
namespace e2rcModel.BusinessLayer
{
    public class Role : ICRUD<Role, byte>
    {
        public byte Role_ID { get; set; }

        public string Description { get; set; }

        public static string Role_Type { get; set; }

        public IEnumerable<Role> Roles
        {
            get
            {
                DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_Role_List", new object[] { "@Role_Type" }, new object[] { Role_Type });

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    List<Role> Roles = new List<Role>();

                    foreach (DataRow Row in dataSet.Tables[0].Rows)
                    {
                        Roles.Add(new Role
                        {
                            Role_ID = Convert.ToByte(Row["Role_ID"]),
                            Description = Convert.ToString(Row["Role"])
                        });
                    }
                    return Roles;

                }
                return null;
            }
        }

        public bool Create()
        {
            throw new NotImplementedException();
        }

        public bool Edit()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public Role Single(byte value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> List()
        {
            throw new NotImplementedException();
        }
    }
}
