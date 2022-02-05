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

public partial class AdminPanel_State_StateList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillState();
        }
    }

    private void FillState()
    {
        SqlConnection objConn = new SqlConnection();
        objConn.ConnectionString = "data source=ALEX; initial catalog=AddressBook; Integrated Security=True";

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_State_SelectAll";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            gvState.DataSource = objSDR;
            gvState.DataBind();
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



    protected void gvState_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName == "DeleteRecord")
        {
            if(e.CommandArgument != null)
            {
                DeleteState(Convert.ToInt32(e.CommandArgument.ToString()));
                FillState();
            }
        }
    }

    private void DeleteState(SqlInt32 Id)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_State_DeleteByPK",objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@StateID", Id);
            objCmd.ExecuteNonQuery();
            objConn.Close();
            lblMsg.Text = "State Deleted Successfully!";
        }
        catch(Exception ex)
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