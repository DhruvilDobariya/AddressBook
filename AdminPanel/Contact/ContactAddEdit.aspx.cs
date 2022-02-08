using System;
using System.Collections;
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
        #region Set Connection
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Set Connection

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            #region Create Command and Bind Data
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
            #endregion Create Command and Bind Data
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
        #region Set Connection
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Set Connection

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            #region Create Command and Bind Data
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
            #endregion Create Command and Bind Data
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
        #region Set Connection
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Set Connection

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            #region Create Command and Bind Data
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
            #endregion Create Command and Bind Data
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
        #region Set Connection
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Set Connection

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            #region Create Command and Bind Data
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
            #endregion Create Command and Bind Data
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
        #region Local Variable
        bool flag = false;
        int i = 1;
        string temp = "";
        #endregion Local Variable
        #region Server side validaton
        
        IDictionary<TextBox, Label> textBoxValidation = new Dictionary<TextBox, Label>()
        {
            {txtContact, lblContact },
            {txtContactNo, lblContactNo },
            {txtEmail, lblEmail},
            {txtAddress, lblAddress }
        };
        IDictionary<DropDownList, Label> dropDownListValidation = new Dictionary<DropDownList, Label>() 
        {
            {ddContactCategory, lblContactCategory },
            {ddCity, lblCity },
            {ddState, lblState },
            {ddCountry, lblCountry }
        };
        
        foreach(KeyValuePair<TextBox, Label> pair in textBoxValidation)
        {   
            if(pair.Key.Text == "")
            {
                flag = true;
                temp += i + ") " + pair.Value.Text + "</br>"; 
            }
            i++;
        }
        foreach (KeyValuePair<DropDownList, Label> pair in dropDownListValidation)
        {
            if (pair.Key.SelectedValue == "-1")
            {
                flag = true;
                temp += i + ") " + pair.Value.Text + "</br>";
            }
            i++;
        }
        if (flag)
        {
            lblMsg.Text = "</br> Please : </br>" + temp;
            return;
        }

        #endregion Server side validaton
        #region Set Connection
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Set Connection
        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            #region Create Command and Set Parameters
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@ContactName", Convert.ToString(txtContact.Text.Trim()));
            objCmd.Parameters.AddWithValue("@ContactCategoryID", Convert.ToInt32(ddContactCategory.SelectedValue));
            objCmd.Parameters.AddWithValue("@CityID", Convert.ToInt32(ddCity.SelectedValue));
            objCmd.Parameters.AddWithValue("@StateID", Convert.ToInt32(ddState.SelectedValue));
            objCmd.Parameters.AddWithValue("@CountryID", Convert.ToInt32(ddCountry.SelectedValue));
            objCmd.Parameters.AddWithValue("@ContactNo", Convert.ToString(txtContactNo.Text.Trim()));
            objCmd.Parameters.AddWithValue("@WhatsappNo", Convert.ToString(txtWhatsappNo.Text.Trim()));
            objCmd.Parameters.AddWithValue("@BirthDate", Convert.ToString(txtBirthDate.Text.Trim()));
            objCmd.Parameters.AddWithValue("@Email", Convert.ToString(txtEmail.Text.Trim()));
            objCmd.Parameters.AddWithValue("@Age", Convert.ToString(txtAge.Text.Trim()));
            objCmd.Parameters.AddWithValue("@BloodGroup", Convert.ToString(txtBloodGroup.Text.Trim()));
            objCmd.Parameters.AddWithValue("@FacebookID", Convert.ToString(txtFecebook.Text.Trim()));
            objCmd.Parameters.AddWithValue("@LinkedInID", Convert.ToString(txtLinkedin.Text.Trim()));
            objCmd.Parameters.AddWithValue("@Address", Convert.ToString(txtAddress.Text.Trim()));
            #endregion Create Command and Set Parameters


            if (Request.QueryString["ContactID"] != null)
            {
                #region Update record
                objCmd.CommandText = "PR_Contact_UpdateByPK";
                objCmd.Parameters.AddWithValue("@ContactID", Convert.ToString(Request.QueryString["ContactID"]));
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/Contact/ContactList.aspx");
                #endregion Update record
            }
            else
            {
                #region Add record
                objCmd.CommandText = "PR_Contact_Insert";
                objCmd.ExecuteNonQuery();
                txtContact.Text = txtContactNo.Text = txtWhatsappNo.Text = txtBirthDate.Text = txtEmail.Text = txtAge.Text = txtBloodGroup.Text = txtFecebook.Text = txtLinkedin.Text = txtAddress.Text = "";
                ddContactCategory.SelectedValue = ddCity.SelectedValue = ddState.SelectedValue = ddCountry.SelectedValue = "-1";
                lblMsg.Text = "Contact Added Successfully";
                #endregion Add record
            }
            objConn.Close();
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message + ex ;
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
        #region Set Connection
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Set Connection

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            #region Create Command and Set Parameters
            SqlCommand objCmd = new SqlCommand("PR_Contact_SelectByPK", objConn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@ContactID", Id);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            #endregion Create Command and Set Parameters

            #region Get data and set data
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
                        txtAddress.Text = objSDR["Address"].ToString();
                    }
                    break;
                }
            }
            else
            {
                lblMsg.Text = "Contact Not Found!";
            }
            #endregion Get data and set data
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