using System;
using System.Web.UI.WebControls;

namespace NewsCaptain.Admin
{
    public partial class Category :common
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button2.Visible = false;
            string categoryId = Request.QueryString["C"];

            if (IsPostBack) return;
            ShowData();
            if (categoryId != null)
            {
                Button1.Visible = false;
                Button2.Visible = true;
                GetCategory(categoryId);
            }

        }

        protected void ShowData()
        {
            categoryGrid.DataSource = GetData("select * from Category");
            categoryGrid.DataBind();
        }


        protected void Category_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label id = categoryGrid.Rows[e.RowIndex].FindControl("h") as Label;
            DeleteData("DeleteCategory", new Guid(id.Text));
            ShowData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == null || TextBox1.Text == "")
            {
                lblmsg.Text = "Please enter Category";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                int dataValue = EnterSignleValue("AddCategory", TextBox1.Text);
                if (dataValue == 200)
                {
                    TextBox1.Text = null;
                    lblmsg.Text = "Category Listed Successfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    ShowData();
                }
                if (dataValue == 201)
                {
                    lblmsg.Text = "Category Already Listed";
                    lblmsg.ForeColor = System.Drawing.Color.AliceBlue;
                }
            }
        }
        public void GetCategory(string publicId)
        {
            var Category = GetData("select * from Category where publicId='" + publicId + "'");
            if (Category.Rows.Count <= 0) return;
            TextBox1.Text = Category.Rows[0][1].ToString();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == null || TextBox1.Text == "")
            {
                lblmsg.Text = "Please enter Category";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                try
                {
                    UpdateSignleValue("UpdateCategory", TextBox1.Text, Convert.ToString(Request.QueryString["C"]));
                    Button2.Visible = false;
                    Button1.Visible = true;
                    TextBox1.Text = null;
                    ShowData();
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.Message;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}