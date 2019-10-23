using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Common.Extentions
{
    public static class DataModelExtention
    {
        //#region ======数据库=======

        //public static void Insert<T>(this T entity)
        //{
        //    var attr = entity.GetType().GetCustomAttributesData().Where(p => p.AttributeType == typeof(TableAttribute)).FirstOrDefault();
        //    if (attr != null)
        //    {
        //        SK.Common.DBC.Context.GetTable(entity.GetType()).InsertOnSubmit(entity);
        //        SK.Common.DBC.Context.SubmitChanges();
        //    }
        //}

        //public static void Update<T>(this T TEntity)
        //{
        //    var attr = TEntity.GetType().GetCustomAttributesData().Where(p => p.AttributeType == typeof(TableAttribute)).FirstOrDefault();
        //    if (attr != null)
        //    {
        //        SK.Common.DBC.Context.SubmitChanges();
        //    }
        //}

        //#endregion
    }
}
