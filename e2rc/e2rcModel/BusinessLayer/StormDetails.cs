using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.BusinessLayer.Interface;
using e2rcModel.Common;
using e2rcModel.DataAccessLayer;

namespace e2rcModel.BusinessLayer
{
    public class StormDetails:ICRUD<StormDetails,byte>
    {
        public long Weather_ID { get; set; }
        public DateTime StormDateTime { get; set; }
        public string StormDuration { get; set; }
        public decimal Amount { get; set; }
        public long Storm_ID { get; set; }
        public int ParentstID { get; set; }
        public long ParentStorm_ID { get; set; }
        public int stID { get; set; }
        public bool Create()
        {
            throw new NotImplementedException();
        }

        public bool Edit()
        {
            return new DAL().Update("sp_StormInspection_CRUD",
                                   new object[] { "@Action", "@Storm_ID", "@StormDateTime", "@StormDuration", "@Amount", "@ParentStorm_ID" },
                                   new object[] { Actions.UPDATE.ToString(), Storm_ID, StormDateTime, StormDuration, Amount, ParentStorm_ID }
                                   );
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public StormDetails Single(byte value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StormDetails> List()
        {
            throw new NotImplementedException();
        }

        internal long Create(long Weather_ID)
        {
            return (long)new DAL().ExecuteStoredProcedure("sp_StormInspection_CRUD",
                 new object[] {"@Action","@Weather_ID","@StormDateTime","@StormDuration","@Amount"                                
                },
                 new object[] {Actions.INSERT.ToString(),Weather_ID,StormDateTime,StormDuration,Amount
                },"Storm_ID", "0", System.Data.SqlDbType.BigInt);
        }


        public bool Create(long Weather_ID, long ParentStorm_ID)
        {
            return new DAL().Insert("[sp_StormInspection_CRUD]",
                   new object[] {"@Action","@Weather_ID","@StormDateTime","@StormDuration","@Amount","@ParentStorm_ID" 
                },
                  new object[] {Actions.INSERT.ToString(),Weather_ID,StormDateTime,StormDuration,Amount,ParentStorm_ID
                });
        }

    }
}
