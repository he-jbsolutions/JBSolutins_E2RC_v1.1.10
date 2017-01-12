using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.BusinessLayer.Interface;
using System.Data;
using e2rcModel.DataAccessLayer;

namespace e2rcModel.BusinessLayer
{
    public class State : ICRUD<State, byte>
    {
        public byte State_ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public IEnumerable<State> States
        {
            get
            {
                DataSet dataSet = new DAL().ExecuteStoredProcedure("sp_State_List");

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    List<State> states = new List<State>();

                    foreach (DataRow Row in dataSet.Tables[0].Rows)
                    {
                        states.Add(new State
                        {
                            State_ID = Convert.ToByte(Row["State_ID"]),
                            Name = Convert.ToString(Row["Name"]),
                            Code = Convert.ToString(Row["Code"])
                        });
                    }
                    return states;

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

        public State Single(byte value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<State> List()
        {
            throw new NotImplementedException();
        }
    }
}
