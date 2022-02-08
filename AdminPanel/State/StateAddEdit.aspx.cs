﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_State_StateAddEdit : System.Web.UI.Page
{
    #region PageLode
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillCountryDropDown();
            if(Request.QueryString["StateID"] != null)
            {
                lblTitle.Text = "Edit State";
                btnSubmit.Text = "Edit";
                FillControlls(Convert.ToInt32(Request.QueryString["StateID"]));
            }
        }
    }
    #endregion PageLode
    #region FillCountryDropDown
    private void FillCountryDropDown()
    {
        #region Set Connection
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Set Connection

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_Country_SelectForDropDownList", objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                ddCountry.DataSource = objSDR;
                ddCountry.DataValueField = "CountryID";
                ddCountry.DataTextField = "CountryName";
                ddCountry.DataBind();
            }

            objConn.Close();
            ddCountry.Items.Insert(0, new ListItem("Select Country", "-1"));
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
    #endregion FillCountryDropDown
    #region SubmitForm
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region Server side validation
        if (txtState.Text.Trim() == "" || txtCode.Text.Trim() == "" || ddCountry.SelectedIndex == -1)
        {
            lblMsg.Text = "Please enter State Name and State Code and Select Country Name";
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
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@StateName", Convert.ToString(txtState.Text.Trim()));
            objCmd.Parameters.AddWithValue("@StateCode", Convert.ToString(txtCode.Text.Trim()));
            objCmd.Parameters.AddWithValue("@CountryID", Convert.ToInt32(ddCountry.SelectedValue));
            #endregion Create Command and Set Parameters

            if (Request.QueryString["StateID"] != null) 
            {
                #region Update record
                objCmd.CommandText = "PR_State_UpdateByPK";
                objCmd.Parameters.AddWithValue("@StateID", Convert.ToString(Request.QueryString["StateID"]));
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/State/StateList.aspx");
                #endregion Update record
            }
            else
            {
                #region Add record
                objCmd.CommandText = "PR_State_Insert";
                objCmd.ExecuteNonQuery();
                lblMsg.Text = "State Added Successfully";
                txtState.Text = txtCode.Text = "";
                ddCountry.SelectedIndex = -1;
                txtState.Focus();
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
    #region Fill Controlls
    private void FillControlls(SqlInt32 Id)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_State_SelectByPK", objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@StateID", Id);
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["StateName"].Equals(DBNull.Value))
                    {
                        txtState.Text = objSDR["StateName"].ToString();
                    }
                    if (!objSDR["StateCode"].Equals(DBNull.Value))
                    {
                        txtCode.Text = objSDR["StateCode"].ToString();
                    }
                    if (!objSDR["CountryID"].Equals(DBNull.Value))
                    {
                        ddCountry.SelectedValue = objSDR["CountryID"].ToString();
                    }
                    break;
                }
            }
            else
            {
                lblMsg.Text = "State Not Found!";
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
    #endregion Fill Controlls
}