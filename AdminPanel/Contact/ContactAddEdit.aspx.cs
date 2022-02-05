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

public partial class AdminPanel_Contact_ContactAddEdit : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillContactCategoryForDropDown();
            FillCityForDropDown();
            FillStateForDropDown();
            FillCountryDropDown();
            if(Request.QueryString["ContactID"] != null)
            {
                lblTitle.Text = "Edit Contact";
                btnSubmit.Text = "Edit";
                FillControlls(Convert.ToInt32(Request.QueryString["ContactID"]));
            }
        }
    }
    #endregion Page Load
    #region Fill Contact Category DropDown
    private void FillContactCategoryForDropDown()
    {
        SqlConnection objConn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_ContactCategory_SelectForDropDownList", objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                ddContactCategory.DataSource = objSDR;
                ddContactCategory.DataValueField = "ContactCategoryID";
                ddContactCategory.DataTextField = "ContactCategoryName";
                ddContactCategory.DataBind();
            }
            objConn.Close();
            ddContactCategory.Items.Insert(0, new ListItem("Select Contact Category", "-1"));
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
    #endregion Fill Contact Category DropDown
    #region Fill City DropDown
    private void FillCityForDropDown()
    {
        SqlConnection objConn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");
        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_City_SelectForDropDownList", objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                ddCity.DataSource = objSDR;
                ddCity.DataValueField = "CityID";
                ddCity.DataTextField = "CityName";
                ddCity.DataBind();
            }

            objConn.Close();
            ddCity.Items.Insert(0, new ListItem("Select City", "-1"));
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
    #endregion Fill City DropDown
    #region Fill State DropDown
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
    #endregion Fill State DropDown
    #region Fill Country DropDown
    private void FillCountryDropDown()
    {
        SqlConnection objConn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");
        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand("PR_Country_SelectForDropDownList", objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                ddCountry.DataSource = objSDR;
                ddCountry.DataValueField = "CountryID";
                ddCountry.DataTextField = "CountryName";
                ddCountry.DataBind();
            }

            objConn.Close();
            ddCountry.Items.Insert(0, new ListItem("Select Country", "-1"));
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
    #endregion Fill Country DropDown
    #region Submit Form
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
        SqlConnection objConn = new SqlConnection("data source=ALEX; initial catalog=AddressBook; Integrated Security=True");
        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@ContactName", Convert.ToString(txtContact.Text.Trim()));
            objCmd.Parameters.AddWithValue("@ContactCategoryID", Convert.ToInt32(ddContactCategory.SelectedValue));
            objCmd.Parameters.AddWithValue("@CityID", Convert.ToInt32(ddCity.SelectedValue));
            objCmd.Parameters.AddWithValue("@StateID", Convert.ToInt32(ddState.SelectedValue));
            objCmd.Parameters.AddWithValue("@CountryID", Convert.ToInt32(ddCountry.SelectedValue));
            objCmd.Parameters.AddWithValue("@ContactNo", Convert.ToString(txtContactNo.Text.Trim()));
            objCmd.Parameters.AddWithValue("@WhatsappNo", WhatsappNo);
            objCmd.Parameters.AddWithValue("@BirthDate", BirthDate);
            objCmd.Parameters.AddWithValue("@Email", Convert.ToString(txtEmail.Text.Trim()));
            objCmd.Parameters.AddWithValue("@Age", Convert.ToInt32(txtAge.Text.Trim()));
            objCmd.Parameters.AddWithValue("@BloodGroup", BloodGroup);
            objCmd.Parameters.AddWithValue("@FacebookID", Facebook);
            objCmd.Parameters.AddWithValue("@LinkedInID", Linkedin);
            objCmd.Parameters.AddWithValue("@Address", Address);

            if (Request.QueryString["ContactID"] != null)
            {
                objCmd.CommandText = "PR_Contact_UpdateByPK";
                objCmd.Parameters.AddWithValue("@ContactID", Convert.ToString(Request.QueryString["ContactID"]));
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/Contact/ContactList.aspx");
            }
            else
            {
                objCmd.CommandText = "PR_Contact_Insert";
                objCmd.ExecuteNonQuery();
                txtContact.Text = txtContactNo.Text = txtWhatsappNo.Text = txtBirthDate.Text = txtEmail.Text = txtAge.Text = txtBloodGroup.Text = txtFecebook.Text = txtLinkedin.Text = tbAddress.Text = "";
                ddContactCategory.SelectedValue = ddCity.SelectedValue = ddState.SelectedValue = ddCountry.SelectedValue = "-1";
                lblMsg.Text = "Contact Added Successfully";
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

            SqlCommand objCmd = new SqlCommand("PR_Contact_SelectByPK", objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@ContactID", Id);
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["ContactName"].Equals(DBNull.Value))
                    {
                        txtContact.Text = objSDR["ContactName"].ToString();
                    }
                    if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                    {
                        ddContactCategory.SelectedValue = objSDR["ContactCategoryID"].ToString();
                    }
                    if (!objSDR["CityID"].Equals(DBNull.Value))
                    {
                        ddCity.SelectedValue = objSDR["CityID"].ToString();
                    }
                    if (!objSDR["StateID"].Equals(DBNull.Value))
                    {
                        ddState.SelectedValue = objSDR["StateID"].ToString();
                    }
                    if (!objSDR["CountryID"].Equals(DBNull.Value))
                    {
                        ddCountry.SelectedValue = objSDR["CountryID"].ToString();
                    }
                    if (!objSDR["ContactNo"].Equals(DBNull.Value))
                    {
                        txtContactNo.Text = objSDR["ContactNo"].ToString();
                    }
                    if (!objSDR["WhatsappNo"].Equals(DBNull.Value))
                    {
                        txtWhatsappNo.Text = objSDR["WhatsappNo"].ToString();
                    }
                    if (!objSDR["BirthDate"].Equals(DBNull.Value))
                    {
                        DateTime bd = Convert.ToDateTime(objSDR["BirthDate"].ToString());
                        txtBirthDate.Text = bd.ToShortDateString();
                    }
                    if (!objSDR["Email"].Equals(DBNull.Value))
                    {
                        txtEmail.Text = objSDR["Email"].ToString();
                    }
                    if (!objSDR["Age"].Equals(DBNull.Value))
                    {
                        txtAge.Text = objSDR["Age"].ToString();
                    }
                    if (!objSDR["BloodGroup"].Equals(DBNull.Value))
                    {
                        txtBloodGroup.Text = objSDR["BloodGroup"].ToString();
                    }
                    if (!objSDR["FacebookID"].Equals(DBNull.Value))
                    {
                        txtFecebook.Text = objSDR["FacebookID"].ToString();
                    }
                    if (!objSDR["LinkedinID"].Equals(DBNull.Value))
                    {
                        txtLinkedin.Text = objSDR["LinkedinID"].ToString();
                    }
                    if (!objSDR["Address"].Equals(DBNull.Value))
                    {
                        tbAddress.Text = objSDR["Address"].ToString();
                    }
                    break;
                }
            }
            else
            {
                lblMsg.Text = "Contact Not Found!";
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