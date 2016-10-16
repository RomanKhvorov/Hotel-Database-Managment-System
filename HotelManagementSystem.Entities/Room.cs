// <copyright file="Room.cs" company="My Company">
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
    /// Represents entity Room
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Gets or sets id of the room.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets id of the category of the room.
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Gets or sets name of the category of the room.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets count of the places in the room.
        /// </summary>
        public int Places { get; set; }

        /// <summary>
        /// Gets or sets price of the room.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets room status.
        /// </summary>
        public bool IsFree { get; set; }

        /// <summary>
        /// Gets or sets date when room will be free.
        /// </summary>
        public DateTime FreeDate { get; set; } = DateTime.Now;
    }
}
