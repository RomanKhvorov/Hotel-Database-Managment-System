// <copyright file="Guest.cs" company="My Company">
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
    /// Represents entity Guest.
    /// </summary>
    public class Guest
    {
        /// <summary>
        /// Gets or sets guest's Id. 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets guest's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets guest's passport.
        /// </summary>
        public string Passport { get; set; }

        /// <summary>
        /// Gets or sets Id of the room, where guest is living.
        /// </summary>
        public int Room { get; set; }
        
        /// <summary>
        /// Gets or sets date of guest's check in. 
        /// </summary>                 
        public DateTime CheckInDate { get; set; }

        /// <summary>
        /// Gets or sets date of guest's check out.
        /// </summary>           
        public DateTime CheckOutDate { get; set; }
    }
}
