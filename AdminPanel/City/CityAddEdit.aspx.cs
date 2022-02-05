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

public partial class AdminPanel_City_CityAddEdit : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillStateForDropDown();
            if(Request.QueryString["CityID"] != null)
            {
                lblTitle.Text = "Edit City";
                btnSubmit.Text = "Edit";
                FillControlls(Convert.ToInt32(Request.QueryString["CityID"]));
            }
        }
    }
    #endregion Page Load
    #region Fill State
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
    #endregion Fill State
    #region Submit Form
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

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@CityName", Convert.ToString(txtCity.Text.Trim()));
            objCmd.Parameters.AddWithValue("@StateID", Convert.ToInt32(ddState.SelectedValue));
            objCmd.Parameters.AddWithValue("@PinCode", Convert.ToString(txtPin.Text.Trim()));
            objCmd.Parameters.AddWithValue("@STDCode", Convert.ToString(txtSTD.Text.Trim()));

            if (Request.QueryString["CityID"] != null)
            {
                objCmd.CommandText = "PR_City_UpdateByPK";
                objCmd.Parameters.AddWithValue("@CityID", Convert.ToString(Request.QueryString["CityID"]));
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/City/CityList.aspx");
            }
            else
            {
                objCmd.CommandText = "PR_City_Insert";
                objCmd.ExecuteNonQuery();
                lblMsg.Text = "City Added Successfully";
                txtCity.Text = txtPin.Text = txtSTD.Text = "";
                ddState.SelectedIndex = -1;
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
    #endregion Submit Form
    #region Fill Controlls
    private void FillControlls(SqlInt32 Id)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_City_SelectByPK", objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@CityID", Id);
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["CityName"].Equals(DBNull.Value))
                    {
                        txtCity.Text = objSDR["CityName"].ToString();
                    }
                    if (!objSDR["STDCode"].Equals(DBNull.Value))
                    {
                        txtSTD.Text = objSDR["STDCode"].ToString();
                    }
                    if (!objSDR["PinCode"].Equals(DBNull.Value))
                    {
                        txtPin.Text = objSDR["PinCode"].ToString();
                    }
                    if (!objSDR["StateID"].Equals(DBNull.Value))
                    {
                        ddState.SelectedValue = objSDR["StateID"].ToString();
                    }
                    break;
                }
            }
            else
            {
                lblMsg.Text = "City Not Found!";
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