using ASPNET_CRUDBASIC.Models;
using System.Data.SqlClient;
using System.Data;
namespace ASPNET_CRUDBASIC.Data
{
    public class ContactData
    {
        public List<ContactModel> ListUsers()
        {
            var UsersList = new List<ContactModel>();
            var cn = new Connection();
            using (var connection = new SqlConnection(cn.GetSqlconnection()))
            {
                connection.Open();
                SqlCommand cmd = new("sp_List", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                using var dr = cmd.ExecuteReader();
                
                    while (dr.Read())
                    {
                        UsersList.Add(CreateContactModelFromDataReader(dr));
                    }
                
            }
            return UsersList;

         }
        private static ContactModel CreateContactModelFromDataReader(SqlDataReader dr)
        {
            return new ContactModel()
            {
                UserId = Convert.ToInt32(dr["UserId"]),
                UserName = dr["UserName"].ToString(),
                UserPhone = dr["UserPhone"].ToString(),
                UserEmail = dr["UserEmail"].ToString(),
            };
        }
        public ContactModel GetUserById(int UserId)
        {
            var SelectedUserById = new ContactModel();
            var cn = new Connection();
            using (var connection = new SqlConnection(cn.GetSqlconnection()))
            {
                connection.Open();
                SqlCommand cmd = new ("sp_getUser", connection);
                //Use the stored Procedure responsible of Select an user by id
                cmd.Parameters.AddWithValue("UserId",UserId);
                cmd.CommandType = CommandType.StoredProcedure;
               
                using var dr = cmd.ExecuteReader();
                
                    while (dr.Read())
                    {
                      SelectedUserById = CreateModelForSlectedUserByIdFromDataReader(dr);
                    }
                
            }
            return SelectedUserById;
        }

        private static ContactModel CreateModelForSlectedUserByIdFromDataReader(SqlDataReader dr)
        {
            var user = new ContactModel
            {
                UserId = Convert.ToInt32(dr["UserId"]),
                UserName = dr["UserName"].ToString(),
                UserPhone = dr["UserPhone"].ToString(),
                UserEmail = dr["UserEmail"].ToString()
            };
            return user;
        }
        public (bool StatusResult, string ErrorMessage) SaveUser(ContactModel selectedUser)
        {
            bool StatusResult;
            string ErrorMessage = string.Empty;
            try
            {
                var cn = new Connection();
                using var connection = new SqlConnection(cn.GetSqlconnection());
                connection.Open();

                // Check if the user already exists
                SqlCommand checkCmd = new("sp_CheckUserExistence", connection);
                checkCmd.Parameters.AddWithValue("@UserEmail", selectedUser.UserEmail);
                checkCmd.CommandType = CommandType.StoredProcedure;
                int existingUserCount = (int)checkCmd.ExecuteScalar();

                if (existingUserCount > 0)
                {
                    // User already exists, handle accordingly
                    StatusResult = false;
                    ErrorMessage = "User already exists in the database.";
                }
                else
                {
                    // User does not exist, proceed with saving
                    SqlCommand cmd = new ("sp_insertUser", connection);
                    // Use the stored procedure responsible for inserting a new user
                    cmd.Parameters.AddWithValue("UserName", selectedUser.UserName);
                    cmd.Parameters.AddWithValue("UserEmail", selectedUser.UserEmail);
                    cmd.Parameters.AddWithValue("UserPhone", selectedUser.UserPhone);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    StatusResult = true;
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Failed to save data. An internal error has occurred. Please contact the support ";
                StatusResult = false;
            }
            return (StatusResult, ErrorMessage);
        }
        public bool UpdateUser(ContactModel selectedUser)
        {
            bool StatusResult;
            try
            {
                var cn = new Connection();
                using (var connection = new SqlConnection(cn.GetSqlconnection()))
                {
                    connection.Open();
                    SqlCommand cmd = new("sp_updateUser", connection);
                    //Use the stored Procedure responsible of update a user
                    cmd.Parameters.AddWithValue("UserId", selectedUser.UserId);
                    cmd.Parameters.AddWithValue("UserName", selectedUser.UserName);
                    cmd.Parameters.AddWithValue("UserEmail", selectedUser.UserEmail);
                    cmd.Parameters.AddWithValue("UserPhone", selectedUser.UserPhone);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                }
                StatusResult = true;
            }
            catch (Exception e)
            {
                string ErrorMessage = e.Message;
                Console.WriteLine(ErrorMessage);
                StatusResult = false;
            }
            return StatusResult;
        }
        public bool DeleteUser(int UserId)
        {
            bool StatusResult;
            try
            {
                var cn = new Connection();
                using (var connection = new SqlConnection(cn.GetSqlconnection()))
                {
                    connection.Open();
                    SqlCommand cmd = new("sp_deleteUser", connection);
                    //Use the stored Procedure responsible of delete a user
                    cmd.Parameters.AddWithValue("UserId", UserId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                }
                StatusResult = true;
            }
            catch (Exception e)
            {
                string ErrorMessage = e.Message;
                Console.WriteLine(ErrorMessage);
                StatusResult = false;
            }
            return StatusResult;
        }
    }
}
