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
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SqlString CountryName = txtCountry.Text.Trim();
        SqlString CountryCode = txtCode.Text.Trim();

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

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        conn.Open();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "PR_Country_Insert";
        sc.Parameters.AddWithValue("@CountryName", CountryName);
        sc.Parameters.AddWithValue("@CountryCode", CountryCode);
        sc.ExecuteNonQuery();
        conn.Close();
        lblMsg.Text = "Country Added Successfully";
        txtCountry.Text = "";
        txtCode.Text = "";
        txtCountry.Focus();
    }
}