using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
            FillContactCategoryForDropDown();
            FillCityForDropDown();
            FillStateForDropDown();
            FillCountryDropDown();
        }
    }
    private void FillContactCategoryForDropDown()
    {
        SqlConnection conn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");
        conn.Open();
        SqlCommand sc = new SqlCommand("PR_ContactCategory_SelectForDropDownList", conn);
        sc.CommandType = CommandType.StoredProcedure;
        SqlDataReader sdr = sc.ExecuteReader();
        if (sdr.HasRows)
        {
            ddContactCategory.DataSource = sdr;
            ddContactCategory.DataValueField = "ContactCategoryID";
            ddContactCategory.DataTextField = "ContactCategoryName";
            ddContactCategory.DataBind();
        }
        conn.Close();
        ddContactCategory.Items.Insert(0, new ListItem("Select Contact Category", "-1"));
    }
    private void FillCityForDropDown()
    {
        SqlConnection conn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");
        conn.Open();
        SqlCommand sc = new SqlCommand("PR_City_SelectForDropDownList", conn);
        sc.CommandType = CommandType.StoredProcedure;
        SqlDataReader sdr = sc.ExecuteReader();
        if (sdr.HasRows)
        {
            ddCity.DataSource = sdr;
            ddCity.DataValueField = "CityID";
            ddCity.DataTextField = "CityName";
            ddCity.DataBind();
        }

        conn.Close();
        ddCity.Items.Insert(0, new ListItem("Select City", "-1"));
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
        SqlString WhatsappNo = SqlString.Null;
        SqlString BirthDate = SqlString.Null;
        SqlString BloodGroup = SqlString.Null;
        SqlString Facebook = SqlString.Null;
        SqlString Linkedin = SqlString.Null;
        SqlString Address = SqlString.Null;

        WhatsappNo = txtWhatsappNo.Text.Trim();
        BirthDate = txtBirthDate.Text.Trim();
        BloodGroup = txtBloodGroup.Text.Trim();
        Facebook = txtFecebook.Text.Trim();
        Linkedin = txtLinkedin.Text.Trim();
        Address = tbAddress.Text.Trim();

        if (txtContact.Text.Trim() == "" || txtContactNo.Text.Trim() == "" || txtEmail.Text.Trim() == "" || tbAddress.Text.Trim() == "" || ddContactCategory.SelectedIndex == 0 || ddCity.SelectedIndex == 0 || ddCountry.SelectedIndex == 0 || ddState.SelectedIndex == 0)
        {
            lblMsg.Text = "Please enter full or valid detail";
            return;
        }
        SqlConnection conn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");
        conn.Open();
        SqlCommand sc = new SqlCommand("PR_Contact_Insert", conn);
        sc.CommandType = CommandType.StoredProcedure;
        sc.Parameters.AddWithValue("@ContactName", Convert.ToString(txtContact.Text.Trim()));
        sc.Parameters.AddWithValue("@ContactCategoryID", Convert.ToInt32(ddContactCategory.SelectedValue));
        sc.Parameters.AddWithValue("@CityID", Convert.ToInt32(ddCity.SelectedValue));
        sc.Parameters.AddWithValue("@StateID", Convert.ToInt32(ddState.SelectedValue));
        sc.Parameters.AddWithValue("@CountryID", Convert.ToInt32(ddCountry.SelectedValue));
        sc.Parameters.AddWithValue("@ContactNo", Convert.ToString(txtContactNo.Text.Trim()));
        sc.Parameters.AddWithValue("@WhatsappNo", WhatsappNo);
        sc.Parameters.AddWithValue("@BirthDate", BirthDate);
        sc.Parameters.AddWithValue("@Email", Convert.ToString(txtEmail.Text.Trim()));
        sc.Parameters.AddWithValue("@Age", Convert.ToInt32(txtAge.Text.Trim()));
        sc.Parameters.AddWithValue("@BloodGroup", BloodGroup);
        sc.Parameters.AddWithValue("@FacebookID", Facebook);
        sc.Parameters.AddWithValue("@LinkedInID", Linkedin);
        sc.Parameters.AddWithValue("@Address", Address);
        sc.ExecuteNonQuery();
        conn.Close();

        txtContact.Text = txtContactNo.Text = txtWhatsappNo.Text = txtBirthDate.Text = txtEmail.Text = txtAge.Text = txtBloodGroup.Text = txtFecebook.Text = txtLinkedin.Text = tbAddress.Text = "";
        ddContactCategory.SelectedValue = ddCity.SelectedValue = ddState.SelectedValue = ddCountry.SelectedValue = "-1";
        lblMsg.Text = "Contact Added Successfully";
    }
}