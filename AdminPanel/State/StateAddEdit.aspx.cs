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
    private void FillCountryDropDown()
    {
        SqlConnection objConn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");

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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtState.Text.Trim() == "" || txtCode.Text.Trim() == "" || ddCountry.SelectedIndex == -1)
        {
            lblMsg.Text = "Please enter State Name and State Code and Select Country Name";
            return;
        }
        SqlConnection objConn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@StateName", Convert.ToString(txtState.Text.Trim()));
            objCmd.Parameters.AddWithValue("@StateCode", Convert.ToString(txtCode.Text.Trim()));
            objCmd.Parameters.AddWithValue("@CountryID", Convert.ToInt32(ddCountry.SelectedValue));

            if (Request.QueryString["StateID"] != null) 
            {
                objCmd.CommandText = "PR_State_UpdateByPK";
                objCmd.Parameters.AddWithValue("@StateID", Convert.ToString(Request.QueryString["StateID"]));
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/State/StateList.aspx");
            }
            else
            {
                objCmd.CommandText = "PR_State_Insert";
                objCmd.ExecuteNonQuery();
                lblMsg.Text = "State Added Successfully";
                txtState.Text = txtCode.Text = "";
                ddCountry.SelectedIndex = -1;
                txtState.Focus();
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
}