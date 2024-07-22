using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using chatapp.Models;

public class ValueController : ApiController
{
    private string connectionString = "Server=HP\\SQLEXPRESS;Database=chatapp;User Id=sa;Password=aditya;"
;

    // GET api/value/messages?senderId=1&receiverId=2
    [HttpGet]
    public IEnumerable<Message> GetMessages(int senderId, int receiverId)
    {
        List<Message> messages = new List<Message>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("GetMessages", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@senderId", senderId);
                command.Parameters.AddWithValue("@receiverId", receiverId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Message message = new Message();
                    message.Id = Convert.ToInt32(reader["Id"]);
                    message.senderId = Convert.ToInt32(reader["senderId"]);
                    message.receiverId = Convert.ToInt32(reader["receiverId"]);
                    message.MessageText = reader["MessageText"].ToString();
                    message.SentAt = Convert.ToDateTime(reader["SentAt"]);
                    message.IsRead = Convert.ToBoolean(reader["IsRead"]);

                    messages.Add(message);
                }
            }
        }

        return messages;
    }

    // POST api/value/messages
    [HttpPost]
    public IHttpActionResult PostMessage(Message message)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("InsertMessage", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@senderId", message.SenderId);
                command.Parameters.AddWithValue("@receiverId", message.ReceiverId);
                command.Parameters.AddWithValue("@messageText", message.MessageText);
                command.Parameters.AddWithValue("@isRead", message.IsRead);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok("Message sent successfully.");
                }
                else
                {
                    return BadRequest("Failed to send message.");
                }
            }
        }
    }

    // POST api/value/login
    [HttpPost]
    public IHttpActionResult Login(UserLoginInfo loginInfo)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("UserLogin", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", loginInfo.Username);
                command.Parameters.AddWithValue("@password", loginInfo.Password);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Ok("Login successful.");
                }
                else
                {
                    return Unauthorized();
                }
            }
        }
    }

    // POST api/value/register
    [HttpPost]
    public IHttpActionResult Register(UserRegistrationInfo registrationInfo)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand("UserRegistration", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", registrationInfo.Username);
                command.Parameters.AddWithValue("@email", registrationInfo.Email);
                command.Parameters.AddWithValue("@password", registrationInfo.Password);
                command.Parameters.AddWithValue("@name", registrationInfo.Name);
                command.Parameters.AddWithValue("@photo", registrationInfo.Photo);
                command.Parameters.AddWithValue("@latitude", registrationInfo.Latitude);
                command.Parameters.AddWithValue("@longitude", registrationInfo.Longitude);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok("Registration successful.");
                }
                else
                {
                    return BadRequest("Registration failed.");
                }
            }
        }
    }
}
