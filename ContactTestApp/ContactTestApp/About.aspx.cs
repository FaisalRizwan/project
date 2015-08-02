using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace ContactTestApp
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridViewWithXml();
            }

        }
        protected void BindGridViewWithXml()
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());

            DataTable dt = new DataTable();


            try
            {
                connection.Open();
                string sqlStatement = "SELECT * FROM tblContact";
                SqlCommand sqlCmd = new SqlCommand(sqlStatement, connection);
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                sqlDa.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dgvContactDetails.DataSource = dt;
                    dgvContactDetails.DataBind();
                    dgvContactDetails.ShowFooter = true;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Fetch Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                connection.Close();
            }
        }

        protected void dgvContactDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvContactDetails.EditIndex = e.NewEditIndex;
            BindGridViewWithXml();
        }

        protected void dgvContactDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgvContactDetails.EditIndex = -1;
            BindGridViewWithXml();
        }

        protected void dgvContactDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //getting username from particular row
                string username = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "name"));
                //identifying the control in gridview
                ImageButton lnkbtnresult = (ImageButton)e.Row.FindControl("imgbtnDelete");
                //raising javascript confirmationbox whenver user clicks on link button
                if (lnkbtnresult != null)
                {
                    lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + username + "')");
                }
            }
        }
        protected void dgvContactDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox txtContactName = (TextBox)dgvContactDetails.Rows[e.RowIndex].FindControl("txtName");
                TextBox txtPhone = (TextBox)dgvContactDetails.Rows[e.RowIndex].FindControl("txtPhone");
                string id = ((Label)dgvContactDetails.Rows[e.RowIndex].FindControl("lbleditContactId")).Text;

                int index = dgvContactDetails.Rows[e.RowIndex].DataItemIndex;
                BindGridViewWithXml();

                UpdateRecord(id, txtContactName.Text, txtPhone.Text);


                dgvContactDetails.EditIndex = -1;
                BindGridViewWithXml();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "StartupScript", "alert('" + ex.Message + "');", true);
            }
        }

        protected void dgvContactDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BindGridViewWithXml();

            string id = e.Keys["contactid"].ToString();
            deleteRecord(id);

            BindGridViewWithXml();
        }

        protected void dgvContactDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                bool ysnContactExist = false;

                int contactid = Convert.ToInt32(dgvContactDetails.DataKeys[0].Values["contactid"].ToString());
                TextBox txtContactName = (TextBox)dgvContactDetails.FooterRow.FindControl("txtftrName");
                TextBox txtPhone = (TextBox)dgvContactDetails.FooterRow.FindControl("txtftrPhone");
                TextBox txtContactId = (TextBox)dgvContactDetails.FooterRow.FindControl("txtftrContactId");
                FileUpload fp = (FileUpload)dgvContactDetails.FooterRow.FindControl("fbFileUpload");
                if (!fp.HasFile)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "StartUpScript", "alert('Sorry!You have not uploaded image.');", true);
                    return;

                }
                else
                {
                    string filename = Path.GetFileName(fp.PostedFile.FileName);
                    fp.SaveAs(Server.MapPath("Images/" + filename));
                }
                for (int index = 0; index < dgvContactDetails.Rows.Count; index++)
                {
                    Label lblContactId = (Label)dgvContactDetails.Rows[index].FindControl("lblitemContactId");

                    if (txtContactId.Text == lblContactId.Text)
                    {
                        ysnContactExist = true;
                    }
                }

                if (ysnContactExist)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "StartUpScript", "alert('Sorry!Contact Id has already been exist.Please check your existing loan schedule.');", true);
                    return;
                }
                string flag = "CREATE";
                UpdateOrAddNewRecord(txtContactId.Text, txtContactName.Text, txtPhone.Text, "Images/" + fp.PostedFile.FileName, flag);

                BindGridViewWithXml();
            }
        }
        private void UpdateOrAddNewRecord(string contactid, string name, string phone, string fileName, string flag)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            string sqlStatement = string.Empty;

            if (flag == "CREATE")
            {
                sqlStatement = "INSERT INTO tblContact" +
                                "VALUES (@contactid,@name,@phone,@image)";
            }

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@contactid", contactid);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@image", fileName);

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert/Update Error:";
                msg += ex.Message;
                throw new Exception(msg);

            }
            finally
            {
                connection.Close();
            }
        }
        #region Internal Method
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        }

        private void UpdateRecord(string contactid, string name, string phone)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            string sqlStatement = string.Empty;

            sqlStatement = "UPDATE tblContact " + "SET name = @name,phone = @phone" + " WHERE contactid = @contactid";

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@contactid", contactid);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@phone", phone);


                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert/Update Error:";
                msg += ex.Message;
                throw new Exception(msg);

            }
            finally
            {
                connection.Close();
            }
        }
        private void deleteRecord(string contactid)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            string sqlStatement = string.Empty;


            sqlStatement = "delete from tblContact where contactid=@contactid";

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@contactid", contactid);

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert/Update Error:";
                msg += ex.Message;
                throw new Exception(msg);

            }
            finally
            {
                connection.Close();
            }
        }

        #endregion
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //SELECT * FROM tblContact where Name like '%34%' or phone like '%423%'

            string name = txtName.Text;
            string phone = txtPhone.Text; ;
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
            }
            if (!string.IsNullOrEmpty(name))
            {
                phone = phone.Trim();
            }
            searchMethod(name, phone);

            //TO Do : Button Search Code
        }
        private void searchMethod(string name, string phone)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());

            DataTable dt = new DataTable();


            try
            {
                connection.Open();
                string sqlStatement = "SELECT * FROM tblContact where Name like '%" + name + "%' and phone like '%" + phone + "%'";
                SqlCommand sqlCmd = new SqlCommand(sqlStatement, connection);
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                sqlDa.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dgvContactDetails.DataSource = dt;
                    dgvContactDetails.DataBind();
                    dgvContactDetails.ShowFooter = true;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Fetch Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
