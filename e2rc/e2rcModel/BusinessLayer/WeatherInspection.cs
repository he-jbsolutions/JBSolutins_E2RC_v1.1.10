using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e2rcModel.BusinessLayer.Interface;
using e2rcModel.DataAccessLayer;
using e2rcModel.Common;

namespace e2rcModel.BusinessLayer
{
    public class WeatherInspection : ICRUD<WeatherInspection, byte>
    {
        public long Weather_ID { get; set; }

        public long Inspection_ID { get; set; }
        public string StromEvent { get; set; }       
        public bool StromEventYes { get; set; }
        public bool StromEventNo { get; set; }
        public string WeatherTime { get; set; }
        public float Temperature { get; set; }
        public bool LastInspection { get; set; }
        public bool LastInspectionYes { get; set; }
        public bool LastInspectionNo { get; set; }
        public bool InspectionOccuring { get; set; }
        public bool InspectionOccuringYes { get; set; }
        public bool InspectionOccuringNo { get; set; }
        public string InspectionOccuringYesValue { get; set; }
        public bool UnsafeInspection { get; set; }
        public bool UnsafeInspectionNo { get; set; }
        public string UnsafeInspectionValue { get; set; }
        public bool UnsafeInspectionYes { get; set; }
        public long UploadData_ID { get; set; }
        public long Storm_ID { get; set; }

        public WeatherInspection()
        {
            UploadDataList = new List<UploadData>
            {
            };
            StormDetailslList = new List<StormDetails>
            {
            };
        }

        public StormDetails StormDetailsListOne { get; set; }
        public StormDetails StormDetailsListTwo { get; set; }
        public StormDetails StormDetailsListThree { get; set; }
        public StormDetails StormDetailsListFour { get; set; }


        public List<UploadData> UploadDataList { get; set; }
        public List<StormDetails> StormDetailslList { get; set; }

        public List<UploadData> UploadDataEditList { get; set; }
        public List<StormDetails> StormDetailsEditList { get; set; }

        public bool Create()
        {
            throw new NotImplementedException();
        }
        public bool Edit()
        {
            throw new NotImplementedException();
        }

        public bool Edit(long Inspection_ID)
        {
            string str = string.Empty;
            str = new DAL().Update("sp_WeatherInspection_CRUD",
                                    new object[] {"@Action", "@Inspection_ID", "@StromEvent","@WeatherTime","@Temperature","@LastInspection",
                                                  "@InspectionOccuring","@InspectionOccuringYes","@UnsafeInspection","@UnsafeInspectionYes" 
                                                 },
                                    new object[] {Actions.UPDATE.ToString(),Inspection_ID,StromEvent,WeatherTime,Temperature,LastInspection,
                                                  InspectionOccuring,InspectionOccuringYesValue,UnsafeInspection,UnsafeInspectionValue 
                                                 }, "@Weather_ID");
            if (!String.IsNullOrEmpty(str))
            {
                Weather_ID = Convert.ToInt64(str);
            }
            //StormDetailsListOne.Edit();
            //StormDetailsListTwo.Edit();
            //StormDetailsListThree.Edit();
            //StormDetailsListFour.Edit();           

            
            //--------------------------------------- Storm Event -------------------------------------

            var ParentStormDetailList = StormDetailslList.FindAll(item => item.stID != 0);
            foreach (var ParentItem in ParentStormDetailList)
            {
                Storm_ID = ParentItem.Create(Weather_ID);
                //find child itesm of parent and add them

                var childStormDetailList = StormDetailslList.FindAll(childItem => childItem.ParentstID == ParentItem.stID);
                CreateChildstormevent(childStormDetailList, Weather_ID, Storm_ID);

            }

            var childItemStormDetailsList = StormDetailslList.FindAll(item => item.ParentStorm_ID != 0);
            if(childItemStormDetailsList!=null)
            {
            foreach (StormDetails item in childItemStormDetailsList)
            {
                item.Create(Weather_ID, item.ParentStorm_ID);

            }
            }
            if (StormDetailsEditList != null)
            {
                foreach (StormDetails item in StormDetailsEditList)
                {                  
                    
                        item.Edit();
                   
                }
            }

            // ------------------------------------Upload Data --------------------------------------

            var ParentUploadDataList = UploadDataList.FindAll(item => item.ID != 0);
            foreach (var ParentItem in ParentUploadDataList)
            {
                UploadData_ID = ParentItem.Create(Inspection_ID);
                //find child itesm of parent and add them

                var childUploadDataList = UploadDataList.FindAll(childItem => childItem.ParentID == ParentItem.ID);               
                CreateChild(childUploadDataList, Inspection_ID, UploadData_ID);

            }

            var childItemUploadDataList = UploadDataList.FindAll(item => item.ParentUploadData_ID != 0);

            foreach (UploadData item in childItemUploadDataList)
            {
                item.Create(Inspection_ID,item.ParentUploadData_ID);

            }
            foreach (UploadData item in UploadDataEditList)
            {
                item.Edit();
            }
            return true;
        }

        private void CreateChild(List<UploadData> childUploadDataList, long Inspection_ID, long UploadData_ID)
        {
            if (childUploadDataList != null)
            {
                foreach (UploadData item in childUploadDataList.ToList())
                {
                    item.Create(Inspection_ID, UploadData_ID);
                }
            }
        }

        internal void Create(long Inspection_ID)
        {
            Weather_ID = (long)new DAL().ExecuteStoredProcedure("sp_WeatherInspection_CRUD",
                        new object[] {"@Action", "@Inspection_ID", "@StromEvent","@WeatherTime","@Temperature","@LastInspection",
                                "@InspectionOccuring","@InspectionOccuringYes","@UnsafeInspection","@UnsafeInspectionYes"
                },
                  new object[] {Actions.INSERT.ToString(),Inspection_ID,StromEvent,WeatherTime,Temperature,LastInspection,
                                InspectionOccuring,InspectionOccuringYesValue,UnsafeInspection,UnsafeInspectionValue 
                }, "@Weather_ID", "0", System.Data.SqlDbType.BigInt);
            if (StromEventYes)
            {              
                var ParentStormEventList = StormDetailslList.FindAll(item => item.stID != 0);
                foreach (var ParentItem in ParentStormEventList)
                {
                    Storm_ID=  ParentItem.Create(Weather_ID);
                    //find child itesm of parent and add them
                    var childStormDetailslList = StormDetailslList.FindAll(childItem => childItem.ParentstID == ParentItem.stID);                  
                    CreateChildstormevent(childStormDetailslList, Weather_ID, Storm_ID);
                }
            }

            var ParentUploadDataList = UploadDataList.FindAll(item => item.ID != 0);
            foreach (var ParentItem in ParentUploadDataList)
            {
                UploadData_ID = ParentItem.Create(Inspection_ID);
                //find child itesm of parent and add them
                var childUploadDataList = UploadDataList.FindAll(childItem => childItem.ParentID == ParentItem.ID);
                //foreach (var childUploadData in childUploadDataList)
                //{
                //    var csparentid = childUploadData.ParentID;
                //}
                CreateChild(childUploadDataList, Inspection_ID, UploadData_ID);

            }
        }
        private void CreateChildstormevent(List<StormDetails> childStormDetailsList, long Weather_ID, long Storm_ID)
        {
            if (childStormDetailsList != null)
            {
                foreach (StormDetails item in childStormDetailsList.ToList())
                {
                    item.Create(Weather_ID, Storm_ID);
                }
            }
        }
       
        public bool Delete()
        {
            throw new NotImplementedException();
        }
        public WeatherInspection Single(byte value)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<WeatherInspection> List()
        {
            throw new NotImplementedException();
        }
    }
}
