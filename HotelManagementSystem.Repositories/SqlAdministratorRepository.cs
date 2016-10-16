// <copyright file="SqlAdministratorRepository.cs" company="My Company">
//     Copyright (c) My Company. All rights reserved.
// </copyright>
// <author>Roman Khvorov</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using HotelManagementSystem.Entities;

namespace HotelManagementSystem.Repositories
{
    /// <summary>
    /// Represents repository for working with Administrator entities.
    /// </summary>
    public class SqlAdministratorRepository : SqlBaseRepository, IAdministratorRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlAdministratorRepository"/> class by given connection. 
        /// </summary>
        /// <param name="connection"> Connection string.</param>
        public SqlAdministratorRepository(string connection) 
            : base(connection)
        {
        }

        /// <summary>
        /// Select administrator from database by login and password.
        /// </summary>
        /// <param name="login"> Administrator's login.</param>
        /// <param name="password"> Administrator's password.</param>
        /// <returns> Administrator with given login and password.</returns>
        public Administrator GetAdministratorByLogin(string login, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("spGetAdministratorByLogin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Administrator admin = null;
                         
                        if (reader.Read())
                        {
                            admin = new Administrator();
                            admin.Id = (int)reader["id"];
                            admin.Name = (string)reader["name"];
                            admin.Login = (string)reader["Login"];
                        }

                        return admin;
                    }
                }
            }
        }
    }
}
