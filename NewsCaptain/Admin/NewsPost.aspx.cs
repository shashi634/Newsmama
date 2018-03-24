using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsCaptain.Admin
{
    public partial class NewsPost : common
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                PopulateDropDown("Select Name,PublicId from category",NewsCategory,"Name","PublicId");
            }
        }
    }
}