// <copyright file="Category.cs" company="My Company">
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
    /// Represents entity Category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets Id of the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets availability of TV in the rooms of this category. 
        /// </summary>
        public bool TV { get; set; }

        /// <summary>
        /// Gets or sets availability of conditioner in the rooms of this category. 
        /// </summary>
        public bool Conditioner { get; set; }

        /// <summary>
        /// Gets or sets availability of Internet in the rooms of this category. 
        /// </summary>
        public bool Internet { get; set; }

        /// <summary>
        /// Gets or sets availability of jacuzzi in the rooms of this category. 
        /// </summary>
        public bool Jacuzzi { get; set; }
    }
}
