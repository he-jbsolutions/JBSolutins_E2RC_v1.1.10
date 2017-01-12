using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.BusinessLayer.Interface;
using e2rcModel.DataAccessLayer;
using e2rcModel.Common;

namespace e2rcModel.BusinessLayer
{
    public class UploadData : ICRUD<UploadData, byte>
    {

        public int ParentID { get; set; }
        public int ID { get; set; }
        public long Inspection_ID { get; set; }
        public long ItemC1_ID { get; set; }
        public string ItemC1_Name { get; set; }
        //public bool MaintenanceNeeded { get; set; }
        //public bool MaintenanceNeededYes { get; set; }
        //public bool MaintenanceNeededNo { get; set; }
        //public bool CorrectiveAction { get; set; }
        //public bool CorrectiveActionYes { get; set; }
        //public bool CorrectiveActionNo { get; set; }
        public long ItemC2_ID { get; set; }
        public long ItemC3_ID { get; set; }
        public string ItemC2_Name { get; set; }
        public string ItemC3_Name { get; set; }
        public string Station { get; set; }
        public string Location { get; set; }
        public int LengthPPP { get; set; }
        public int UOM_ID { get; set; }
        public string LtRt { get; set; }
        public string UOM { get; set; }
        public long UploadData_ID { get; set; }
        public long ParentUploadData_ID { get; set; }
        public int ParentIndex { get; set; }
        public string UploadImagePath { get; set; }
        public string FileName { get; set; }
        public string ImageName { get; set; }
        public string Status { get; set; }
        public UploadData()
        {
            Inspection_ID = 0;
            ItemC1_ID = 1;
            ItemC1_Name = string.Empty;            
            ItemC2_ID = 0;
            ItemC3_ID = 0;
            ItemC2_Name = string.Empty;
            ItemC3_Name = string.Empty;
            Station = string.Empty;
            Location = string.Empty;
            LengthPPP = 0;
            UOM_ID = 0;
            LtRt = string.Empty;
            UOM = string.Empty;
            UploadData_ID = 0;
            ParentUploadData_ID = 0;
            UploadImagePath = string.Empty;
            FileName = string.Empty;
            ImageName = string.Empty;
        }
        public bool Create()
        {
            throw new NotImplementedException();
        }

        public bool Create(long Inspection_ID, long ParentUploadData_ID)
        {
            return new DAL().Insert("[sp_UploadDataInspection_CRUD]",
                  new object[] {"@Action", "@Inspection_ID","@ItemC1_ID","@ItemC2_ID","@ItemC3_ID","@Station","@Location","@LengthPPP","@UOM_ID","@LtRt","@ParentUploadData_ID","@UploadImagePath"
                },
                  new object[] {Actions.INSERT.ToString(),Inspection_ID,ItemC1_ID,
                                ItemC2_ID,ItemC3_ID,Station,Location,LengthPPP,UOM_ID,LtRt,ParentUploadData_ID,UploadImagePath
                });
        }

        internal long Create(long Inspection_ID)
        {
            return (long)new DAL().ExecuteStoredProcedure("[sp_UploadDataInspection_CRUD]",
                  new object[] {"@Action", "@Inspection_ID","@ItemC1_ID",
                                "@ItemC2_ID","@ItemC3_ID", "@Station","@Location","@LengthPPP","@UOM_ID","@LtRt","@UploadImagePath"
                },
                  new object[] {Actions.INSERT.ToString(),Inspection_ID,ItemC1_ID,
                                ItemC2_ID,ItemC3_ID,Station,Location,LengthPPP,UOM_ID,LtRt,UploadImagePath
                }, "UploadDataID", "0", System.Data.SqlDbType.BigInt);
        }

        public bool Edit()
        {
            return new DAL().Update("sp_UploadDataInspection_CRUD",
                                    new object[] {"@Action", "@UploadData_ID","@ItemC1_ID",
                                                   "@ItemC2_ID","@ItemC3_ID", "@Station","@Location","@LengthPPP","@UOM_ID","@LtRt","@ParentUploadData_ID","@UploadImagePath" },
                                    new object[] {Actions.UPDATE.ToString(),UploadData_ID,ItemC1_ID,
                                                  ItemC2_ID,ItemC3_ID,Station,Location,LengthPPP,UOM_ID,LtRt,ParentUploadData_ID,UploadImagePath }
                                    );
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public UploadData Single(byte value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UploadData> List()
        {
            throw new NotImplementedException();
        }

       
    }
}
