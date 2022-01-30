using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Contact_ContactAddEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillStateForDropDown();
            FillCountryDropDown();
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
    private void FillCountryDropDown()
    {
        SqlConnection conn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");
        conn.Open();
        SqlCommand sc = new SqlCommand("PR_Country_SelectForDropDownList", conn);
        sc.CommandType = CommandType.StoredProcedure;
        SqlDataReader sdr = sc.ExecuteReader();

        if (sdr.HasRows)
        {
            ddCountry.DataSource = sdr;
            ddCountry.DataValueField = "CountryID";
            ddCountry.DataTextField = "CountryName";
            ddCountry.DataBind();
        }

        conn.Close();
        ddCountry.Items.Insert(0, new ListItem("Select Country", "-1"));
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
}