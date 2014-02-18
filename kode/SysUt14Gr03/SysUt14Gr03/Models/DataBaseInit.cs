using System.Collections.Generic;
using System.Data.Entity;

namespace SysUt14Gr03.Models
{
    public class DataBaseInit : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
 	         base.Seed(context);
        }
    }
}