using System.Collections.Generic;


namespace e2rcModel.BusinessLayer.Interface
{
    public interface ICRUD<TModel, TValue>
    {
        bool Create();

        bool Edit();

        bool Delete();

        TModel Single(TValue value);

        IEnumerable<TModel> List();

       
    }
}
