using Model.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.BasicData
{
    public class CompanyDAL : DBHelper<Company>
    {
        public List<string> GetAllCode3()
        {
            var linq = from t in context.Company
                       select t.CompanyCode3;
            return linq.ToList();
        }
    }
}
