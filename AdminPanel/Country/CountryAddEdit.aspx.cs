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

public partial class AdminPanel_Country_CountryAddEdit : System.Web.UI.Page
{
    #region PageLode
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if(Request.QueryString["CountryID"] != null)
            {
                btnSubmit.Text = "Edit";
                lblTitle.Text = "Edit Country";
                FillControlls(Convert.ToInt32(Request.QueryString["CountryID"]));
            }
        }
    }
    #endregion PageLode
    #region SubmimForm
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region Local variable
        SqlString CountryName = txtCountry.Text.Trim();
        SqlString CountryCode = txtCode.Text.Trim();
        #endregion Local variable
        #region Server side validation
        if (CountryName == "" && CountryCode == "")
        {
            lblMsg.Text = "Please enter Country Name and Country Code";
            return;
        }
        else if (CountryName == "")
        {
            lblMsg.Text = "Please enter Country Name";
            return;
        }
        else if (CountryCode == "")
        {
            lblMsg.Text = "Please enter Country Code";
            return;
        }
        #endregion Server side validation
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

            objCmd.Parameters.AddWithValue("@CountryName", CountryName);
            objCmd.Parameters.AddWithValue("@CountryCode", CountryCode);
            #endregion Create Command and Set Parameters

            if (Request.QueryString["CountryID"] != null)
            {
                #region Update record
                objCmd.CommandText = "PR_Country_UpdateByPK";
                objCmd.Parameters.AddWithValue("@CountryID", Convert.ToInt32(Request.QueryString["CountryID"]));
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/Country/CountryList.aspx");
                #endregion Update record
            }
            else
            {
                #region Add record
                objCmd.CommandText = "PR_Country_Insert";
                objCmd.ExecuteNonQuery();
                lblMsg.Text = "Country Added Successfully";
                txtCountry.Text = "";
                txtCode.Text = "";
                txtCountry.Focus();
                #endregion Add record
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
    #endregion SubmitForm
    #region FillControlls
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
            SqlCommand objCmd = new SqlCommand("PR_Country_SelectByPK", objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@CountryID", Id);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            #endregion Create Command and Set Parameters
            #region Get data and set data
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["CountryName"].Equals(DBNull.Value))
                    {
                        txtCountry.Text = objSDR["CountryName"].ToString();
                    }
                    if (!objSDR["CountryCode"].Equals(DBNull.Value))
                    {
                        txtCode.Text = objSDR["CountryCode"].ToString();
                    }
                    break;
                }
            }
            else
            {
                lblMsg.Text = "Country Not Found!";
            }
            #endregion Get data and set data
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
    #endregion FillControlls
}