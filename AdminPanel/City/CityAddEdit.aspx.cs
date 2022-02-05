﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_City_CityAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillStateForDropDown();
        }
    }
    private void FillStateForDropDown()
    {
        SqlConnection objConn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");
        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_State_SelectForDropDownList", objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                ddState.DataSource = objSDR;
                ddState.DataValueField = "StateID";
                ddState.DataTextField = "StateName";
                ddState.DataBind();
            }
            objConn.Close();
            ddState.Items.Insert(0, new ListItem("Select State", "-1"));
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
        if(txtCity.Text.Trim() == "" || ddState.SelectedIndex == -1)
        {
            lblMsg.Text = "Enter Full and Valid Information";
            return;
        }
        SqlConnection objConn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");
        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_City_Insert", objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@CityName", Convert.ToString(txtCity.Text.Trim()));
            objCmd.Parameters.AddWithValue("@StateID", Convert.ToInt32(ddState.SelectedValue));
            objCmd.Parameters.AddWithValue("@PinCode", Convert.ToString(txtPin.Text.Trim()));
            objCmd.Parameters.AddWithValue("@STDCode", Convert.ToString(txtSTD.Text.Trim()));
            objCmd.ExecuteNonQuery();
            objConn.Close();

            lblMsg.Text = "City Added Successfully";
            txtCity.Text = txtPin.Text = txtSTD.Text = "";
            ddState.SelectedIndex = -1;
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