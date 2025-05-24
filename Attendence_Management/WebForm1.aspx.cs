using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Attendence_Management.Data_Layer;

namespace Attendence_Management
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DatabaseDB db = new DatabaseDB();
        CrateTableDB ct = new CrateTableDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            db.CreateDatabase();
            ct.CreateTables();

        }
    }
}