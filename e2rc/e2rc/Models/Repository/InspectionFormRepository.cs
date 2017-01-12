using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public static class InspectionFormRepository
    {

        public static IEnumerable<InspectionFormModel> List(string search)
        {
            IEnumerable<InspectionForm> InspectionFromList = new InspectionForm().List(search);
            if (InspectionFromList != null)
            {
                List<InspectionFormModel> InspectionFromModelList = new List<InspectionFormModel>();

                foreach (InspectionForm inspectionForm in InspectionFromList)
                {
                    InspectionFromModelList.Add(GetInspectorModel(inspectionForm));
                }
                return InspectionFromModelList;
            }
            return null;
        }

        public static IEnumerable<InspectionFormModel> List()
        {
            IEnumerable<InspectionForm> InspectionFromList = new InspectionForm().List();
            if (InspectionFromList != null)
            {
                List<InspectionFormModel> InspectionFromModelList = new List<InspectionFormModel>();

                foreach (InspectionForm inspectionForm in InspectionFromList)
                {
                    InspectionFromModelList.Add(GetInspectorModel(inspectionForm));
                }
                return InspectionFromModelList;
            }
            return null;
        }

        public static bool Edit(InspectionFormModel InspectionFormModel)
        {
            InspectionForm InspectionForm = GetInspectionFrom(InspectionFormModel);
            return InspectionForm.Edit();

        }

        private static InspectionFormModel GetInspectorModel(InspectionForm inspectionForm)
        {
            return new InspectionFormModel
            {
                CreatedBy_ID = inspectionForm.CreatedBy_ID,
                Date = inspectionForm.CreatedDate,
                Form_ID = inspectionForm.Form_ID,
                IsActive = inspectionForm.IsActive,
                Name = inspectionForm.Name,
                Description = inspectionForm.Description,
                Path=inspectionForm.Path

            };
        }

        private static InspectionForm GetInspectionFrom(InspectionFormModel InspectionFormModel)
        {
            return new InspectionForm
            {
                CreatedBy_ID = InspectionFormModel.CreatedBy_ID,
                CreatedDate = InspectionFormModel.Date,
                Form_ID = InspectionFormModel.Form_ID,
                IsActive = InspectionFormModel.IsActive,
                Name = InspectionFormModel.Name,
                Description = InspectionFormModel.Description,
                Path=InspectionFormModel.Path
            };
        }

        public static List<string> SearchByFormName(string search, long User_ID)
        {
            var FormNames = new InspectionForm().AutoList(search, User_ID);

            if (FormNames != null)
            {
                return FormNames.ToList();
            }
            return null;
        }
    }
}