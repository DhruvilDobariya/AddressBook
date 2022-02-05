using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_ContactCategory_ContactCategoryAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if(Request.QueryString["ContactCategoryID"] != null)
            {
                lblTitle.Text = "Edit Contact Category";
                btnSubmit.Text = "Edit";
                FillControlls(Convert.ToInt32(Request.QueryString["ContactCategoryID"]));
            }
        }
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

            objCmd.Parameters.AddWithValue("@ContactCategoryName", Convert.ToString(txtContactCategory.Text.Trim()));

            if (Request.QueryString["ContactCategoryID"] != null)
            {
                objCmd.CommandText = "PR_ContactCategory_UpdateByPK";
                objCmd.Parameters.AddWithValue("@ContactCategoryID", Convert.ToString(Request.QueryString["ContactCategoryID"]));
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/ContactCategory/ContactCategoryList.aspx");
            }
            else
            {
                objCmd.CommandText = "PR_ContactCategory_Insert";
                objCmd.ExecuteNonQuery();
                lblMsg.Text = "Contact Category Added Successfully";
                txtContactCategory.Text = "";
                txtContactCategory.Focus();
            }

            objConn.Close();
            
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
    private void FillControlls(SqlInt32 Id)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_ContactCategory_SelectByPK", objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@ContactCategoryID", Id);
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["ContactCategoryName"].Equals(DBNull.Value))
                    {
                        txtContactCategory.Text = objSDR["ContactCategoryName"].ToString();
                    }
                    break;
                }
            }
            else
            {
                lblMsg.Text = "Contact Category Not Found!";
            }
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