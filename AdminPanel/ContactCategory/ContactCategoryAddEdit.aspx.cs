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
    #region Page Load
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
    #endregion Page Lode
    #region Submit Form
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region Local variable
        SqlString strContactCategory = SqlString.Null;
        #endregion Local variable
        #region Server side validation
        if (txtContactCategory.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter a Contact Category";
            return;
        }
        #endregion Server side validation
        #region Set local 
        if(txtContactCategory.Text.Trim() != "")
            strContactCategory = txtContactCategory.Text.Trim();
        #endregion Set local variable
        #region Set Connection
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Set Connection

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            #region Create Command and Set Parameters
            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@ContactCategoryName", strContactCategory);
            #endregion Create Command and Set Parameters

            if (Request.QueryString["ContactCategoryID"] != null)
            {
                #region Update record
                objCmd.CommandText = "PR_ContactCategory_UpdateByPK";
                objCmd.Parameters.AddWithValue("@ContactCategoryID", Convert.ToString(Request.QueryString["ContactCategoryID"]));
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/ContactCategory/ContactCategoryList.aspx");
                #endregion Update record
            }
            else
            {
                #region Add record
                objCmd.CommandText = "PR_ContactCategory_Insert";
                objCmd.ExecuteNonQuery();
                lblMsg.Text = "Contact Category Added Successfully";
                ClearControls();
                #endregion Add record
            }

            if (objConn.State == ConnectionState.Open)
                objConn.Close();

        }
        catch (Exception ex)
        {
            if(ex.Message.Contains("Cannot insert duplicate key row in object 'dbo.ContactCategory' with unique index 'IX_ContactCategory'."))
            {
                lblMsg.Text = "Contact Category alrady exist!";
            }
            else
            {
                lblMsg.Text = ex.Message;
            }
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        
    }
    #endregion Submit Foem
    #region Fill Controlls
    private void FillControlls(SqlInt32 Id)
    {
        #region Set Connection
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Set Connection

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            #region Create Command and Set Parameters
            SqlCommand objCmd = new SqlCommand("PR_ContactCategory_SelectByPK", objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@ContactCategoryID", Id);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            #endregion Create Command and Set Parameters
            #region Get data and set data
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
            #endregion Get data and set data

            if (objConn.State == ConnectionState.Open)
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
    #endregion Fill Controlls
    private void ClearControls()
    {
        txtContactCategory.Text = "";
        txtContactCategory.Focus();
    }
}