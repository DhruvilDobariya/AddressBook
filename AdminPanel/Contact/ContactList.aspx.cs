using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Contact_ContactList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = "data source=ALEX; initial catalog=AddressBook; Integrated Security=True";
        conn.Open();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "PR_Contact_SelectAll";
        SqlDataReader sdr = sc.ExecuteReader();
        gvContact.DataSource = sdr;
        gvContact.DataBind();
        conn.Close();
    }
}