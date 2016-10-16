// <copyright file="SqlBaseRepository.cs" company="My Company">
//     Copyright (c) My Company. All rights reserved.
// </copyright>
// <author>Roman Khvorov</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Repositories
{
    /// <summary>
    /// Abstract class for classes, that work with data from the database.
    /// </summary>
    public abstract class SqlBaseRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlBaseRepository"/> class by given connection. 
        /// </summary>
        /// <param name="connection"> Connection string.</param>
        protected SqlBaseRepository(string connection)
        {
            this.ConnectionString = connection;
        }

        /// <summary>
        /// Gets or sets connection string for connection.
        /// </summary>
        protected string ConnectionString { get; set; }
    }
}
