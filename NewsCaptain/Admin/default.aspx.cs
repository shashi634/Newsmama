using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsCaptain.Admin
{
    public partial class _default : common
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                if (IsAdminLoggedIn)
                {
                    lblmsg.Text = "Admin is loggedin";
                }
                if (IsWriterLoggedIn)
                {
                    lblmsg.Text = "Writer is loggedin";
                }
                if (IsEditorLoggedIn)
                {
                    lblmsg.Text = "Ediotr is loggedin";
                }
            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(name.Text))
            {
                lblmsg.Text = "Enter your Email Id";
            }
            else {
                var pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                if (!Regex.IsMatch(name.Text, pattern)) {
                    lblmsg.Text = "Incorrect Email Id.";
                }

            }
            if (string.IsNullOrEmpty(pass.Text))
            {
                lblmsg.Text = "Enter Password";
            }
            else {
                var userData = GetData("select Id, Name, PublicId, UserType from [User] where emailid='"+name.Text
                    +"' and password= '"+pass.Text+"'");
                if (userData.Rows.Count > 0)
                {
                    // For admin
                    if (Convert.ToInt64(userData.Rows[0][3]) == 1) {
                        Session["AdminUser"] = userData.Rows[0][1] + "|" + userData.Rows[0][2];
                    }
                    // For writer
                    if (Convert.ToInt64(userData.Rows[0][3]) == 2)
                    {
                        Session["WriterUser"] = userData.Rows[0][1] + "|" + userData.Rows[0][2];
                    }
                    // For sub admin
                    if (Convert.ToInt64(userData.Rows[0][3]) == 3)
                    {
                        Session["EditorUser"] = userData.Rows[0][1] + "|" + userData.Rows[0][2];
                    }
                }
            }
        }
    }
}