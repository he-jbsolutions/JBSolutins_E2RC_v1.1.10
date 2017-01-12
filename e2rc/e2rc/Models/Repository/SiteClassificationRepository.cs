using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public class SiteClassificationRepository
    {
        public static IEnumerable<SiteClassificationModel> items
        {
            get
            {
    
                List<SiteClassificationModel> items = new List<SiteClassificationModel>();
                foreach (var item in new SiteClassification().items)
                {
                    items.Add(new SiteClassificationModel { CodeId = item.CodeId, CodeTypeId = item.CodeTypeId, Description = item.Description });
                  
                }
                return items;
            }
        }
       
    }
}