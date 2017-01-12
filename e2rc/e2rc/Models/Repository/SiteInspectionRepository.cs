using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public static class SiteInspectionRepository
    {
        public static IEnumerable<SiteInspectionModel> List()
        {
            IEnumerable<SiteInspection> SiteInspectionList = new SiteInspection().List();

            if (SiteInspectionList != null)
            {
                List<SiteInspectionModel> SiteInspectionModelList = new List<SiteInspectionModel>();

                foreach (SiteInspection siteInspection in SiteInspectionList)
                {
                    SiteInspectionModelList.Add(GetSiteInspectionModel(siteInspection));
                }
                return SiteInspectionModelList;
            }
            return null;
        }

        private static SiteInspectionModel GetSiteInspectionModel(SiteInspection siteInspection)
        {
            return new SiteInspectionModel
            {
                 InspectionQuestion_ID=siteInspection.InspectionQuestion_ID,
                 Question=siteInspection.Question                
            };
        }
    }
}