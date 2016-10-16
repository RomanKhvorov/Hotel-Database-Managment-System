// <copyright file="ICategoryRepository.cs" company="My Company">
//     Copyright (c) My Company. All rights reserved.
// </copyright>
// <author>Roman Khvorov</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementSystem.Entities;

namespace HotelManagementSystem.Repositories
{
    /// <summary>
    /// Interface for classes, that work with Category entities.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Get all categories from database.
        /// </summary>
        /// <returns> List of categories. </returns>
        IEnumerable<Category> SelectAll();
    }
}
