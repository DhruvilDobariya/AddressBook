using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_ContactCategory_ContactCategoryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = "data source=ALEX; initial catalog=AddressBook; Integrated Security=True";
        conn.Open();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "PR_ContactCategory_SelectAll";
        SqlDataReader sdr = sc.ExecuteReader();
        gvContactCategory.DataSource = sdr;
        gvContactCategory.DataBind();
        conn.Close();
    }
}