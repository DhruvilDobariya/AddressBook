using System;
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
        SqlConnection conn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");
        conn.Open();
        SqlCommand sc = new SqlCommand("PR_State_SelectForDropDownList", conn);
        sc.CommandType = CommandType.StoredProcedure;
        SqlDataReader sdr = sc.ExecuteReader();
        if (sdr.HasRows)
        {
            ddState.DataSource = sdr;
            ddState.DataValueField = "StateID";
            ddState.DataTextField = "StateName";
            ddState.DataBind();
        }
        conn.Close();
        ddState.Items.Insert(0, new ListItem("Select State", "-1"));
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(txtCity.Text.Trim() == "" || ddState.SelectedIndex == -1)
        {
            lblMsg.Text = "Enter Full and Valid Information";
            return;
        }
        SqlConnection conn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");
        conn.Open();
        SqlCommand sc = new SqlCommand("PR_City_Insert", conn);
        sc.CommandType = CommandType.StoredProcedure;
        sc.Parameters.AddWithValue("@CityName", Convert.ToString(txtCity.Text.Trim()));
        sc.Parameters.AddWithValue("@StateID", Convert.ToInt32(ddState.SelectedValue));
        sc.Parameters.AddWithValue("@PinCode", Convert.ToString(txtPin.Text.Trim()));
        sc.Parameters.AddWithValue("@STDCode", Convert.ToString(txtSTD.Text.Trim()));
        sc.ExecuteNonQuery();
        conn.Close();

        lblMsg.Text = "City Added Successfully";
        txtCity.Text = txtPin.Text = txtSTD.Text = "";
        ddState.SelectedIndex = -1;

    }
}