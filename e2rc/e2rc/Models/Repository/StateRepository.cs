using System.Collections.Generic;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public static class StateRepository
    {
        public static IEnumerable<StateModel> States
        {
            get
            {
                List<StateModel> states = new List<StateModel>();
                foreach (var state in new State().States)
                {
                    states.Add(new StateModel { State_ID = state.State_ID, Code = state.Code, Name = state.Name });
                }
                return states;
            }
        }
    }
}