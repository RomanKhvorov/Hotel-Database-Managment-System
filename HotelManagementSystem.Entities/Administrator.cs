// <copyright file="Administrator.cs" company="My Company">
//     Copyright (c) My Company. All rights reserved.
// </copyright>
// <author>Roman Khvorov</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Entities
{
    /// <summary>
    /// Represents entity Administrator.
    /// </summary>
    public class Administrator
    {
        /// <summary>
        /// Gets or sets Id of administrator.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name of administrator.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets login of administrator.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets password of administrator.
        /// </summary>
        public string Password { get; set; }
    }
}
