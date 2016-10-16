// <copyright file="SqlCategoryRepository.cs" company="My Company">
//     Copyright (c) My Company. All rights reserved.
// </copyright>
// <author>Roman Khvorov</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using HotelManagementSystem.Entities;

namespace HotelManagementSystem.Repositories
{
    /// <summary>
    /// Represents repository for working with Category entities.
    /// </summary>
    public class SqlCategoryRepository : SqlBaseRepository, ICategoryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCategoryRepository"/> class by given connection. 
        /// </summary>
        /// <param name="connection"> Connection string.</param>
        public SqlCategoryRepository(string connection) 
            : base(connection)
        {
        }

        /// <summary>
        /// Get all categories from database.
        /// </summary>
        /// <returns> List of categories. </returns>
        public IEnumerable<Category> SelectAll()
        {
            var categoryList = new List<Category>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Category", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoryList.Add(new Category
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                TV = (bool)reader["TV"],
                                Conditioner = (bool)reader["conditioner"],
                                Internet = (bool)reader["internet"],
                                Jacuzzi = (bool)reader["jacuzzi"]
                            });
                        }
                    }                  
                }
            }

            return categoryList;
        }
    }
}
