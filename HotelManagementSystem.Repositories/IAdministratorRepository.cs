// <copyright file="IAdministratorRepository.cs" company="My Company">
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
    /// Interface for classes, that work with Administrator entities.
    /// </summary>
    public interface IAdministratorRepository
    {
        /// <summary>
        /// Select administrator from database by login and password.
        /// </summary>
        /// <param name="login"> Administrator's login.</param>
        /// <param name="password"> Administrator's password.</param>
        /// <returns> Administrator with given login and password.</returns>
        Administrator GetAdministratorByLogin(string login, string password);
    }
}
