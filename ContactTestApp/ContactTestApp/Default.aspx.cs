using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ContactTestApp
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            string name = txtName.Text;
            string phone = txtPhone.Text;
            FileUpload fp = (FileUpload)fbFileUpload;
            string filename = fp.PostedFile.FileName;
            fp.SaveAs(Server.MapPath("Images/" + filename));
            int contactid = getTotalCount();
            contactid = contactid + 1;

            AddNewRecord(contactid.ToString(), name, phone, "Images/" + fp.PostedFile.FileName);

            txtPhone.Text = string.Empty;
            txtName.Text = string.Empty;

        }
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        }
        protected int getTotalCount()
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
            return dt.Rows.Count;
        }

        private void AddNewRecord(string contactid, string name, string phone, string fileName)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            string sqlStatement = string.Empty;


            sqlStatement = "INSERT INTO tblContact" +
                            " VALUES (@contactid,@name,@phone,@image)";


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
                string msg = "Insert Error:";
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
