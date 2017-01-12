using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public class UOMRepository
    {
        public static IEnumerable<UOMModel> UOMs
        {
            get
            {
                List<UOMModel> items = new List<UOMModel>();
                foreach (var item in new UOM().UOMs )
                {
                    items.Add(new UOMModel { UOM_ID = item.UOM_ID, UOM = item.uom });
                }
                return items;
            }
        }
    }
}