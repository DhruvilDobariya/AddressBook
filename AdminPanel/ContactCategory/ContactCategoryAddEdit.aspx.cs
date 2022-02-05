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

        SqlConnection objConn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_ContactCategory_Insert";
            objCmd.Parameters.AddWithValue("@ContactCategoryName", Convert.ToString(txtContactCategory.Text.Trim()));
            objCmd.ExecuteNonQuery();
            objConn.Close();

            lblMsg.Text = "Contact Category Added Successfully";
            txtContactCategory.Text = "";
            txtContactCategory.Focus();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        
    }
}