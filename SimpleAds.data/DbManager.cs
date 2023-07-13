using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAds.data
{
    public class DbManager
    {
        private string _connectionString;

        public DbManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void NewAd(Ad ad)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO AD (Name, Date, PhoneNumber, Listing)
                            VALUES (@name, @date, @phoneNumber, @listing) SELECT SCOPE_IDENTITY()";
            object name = ad.ListerName;
            if(name == null)
            {
                command.Parameters.AddWithValue("@name", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@name", ad.ListerName);
            }
            command.Parameters.AddWithValue("@date", DateTime.Now);
            command.Parameters.AddWithValue("@phoneNumber", ad.PhoneNumber);
            command.Parameters.AddWithValue("@listing", ad.Listing);
            connection.Open();
            ad.Id =(int)(decimal)command.ExecuteScalar();

        }

        public List<Ad> GetAllAds()
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM Ad
                            ORDER BY DATE DESC";

            connection.Open();
            var ads = new List<Ad>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                ads.Add(new Ad
                {
                    Id = (int)reader["Id"],
                    ListerName = reader.GetOrNull<string>("name"),
                    ListingDate = (DateTime)reader["Date"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    Listing = (string)reader["Listing"],
                });
            }

            return ads;

        }

        public void DeleteAd(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Ad WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
