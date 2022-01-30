using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        }
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
        if (txtState.Text.Trim() == "" || txtCode.Text.Trim() == "" || ddCountry.SelectedIndex == -1)
        {
            lblMsg.Text = "Please enter State Name and State Code and Select Country Name";
            return;
        }
        SqlConnection conn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");
        conn.Open();
        SqlCommand sc = new SqlCommand("PR_State_Insert",conn);
        sc.CommandType = CommandType.StoredProcedure;
        sc.Parameters.AddWithValue("@StateName",Convert.ToString(txtState.Text.Trim()));
        sc.Parameters.AddWithValue("@StateCode", Convert.ToString(txtState.Text.Trim()));
        sc.Parameters.AddWithValue("@CountryID",Convert.ToInt32(ddCountry.SelectedValue));
        sc.ExecuteNonQuery();
        conn.Close();

        lblMsg.Text = "State Added Successfully";
        txtState.Text = txtCode.Text = "";
        ddCountry.SelectedIndex = -1;
        txtState.Focus();
    }
}