using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_ContactCategory_ContactCategoryAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(txtContactCategory.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter a Contact Category";
            return;
        }
        SqlConnection conn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");
        conn.Open();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "PR_ContactCategory_Insert";
        sc.Parameters.AddWithValue("@ContactCategoryName", Convert.ToString(txtContactCategory.Text.Trim()));
        sc.ExecuteNonQuery();
        conn.Close();

        lblMsg.Text = "Contact Category Added Successfully";
        txtContactCategory.Text = "";
        txtContactCategory.Focus();
    }
}