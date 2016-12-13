// <copyright file="IGuestRepository.cs" company="My Company">
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
    /// Interface for classes, that work with Guest entities.
    /// </summary>
    public interface IGuestRepository
    {
        // Review IP: Bad approach to have methods with lot of arguments, use model instead
        /// <summary>
        /// Settles guest to the given room.
        /// </summary>
        /// <param name="roomId"> Room Id.</param>
        /// <param name="name"> Guest's name.</param>
        /// <param name="passport"> Guest's passport.</param>
        /// <param name="checkInDate"> Guest's check in date.</param>
        /// <param name="checkOutDate"> Guest's check out date.</param>
        void SettleGuestInTheRoom(int roomId, string name, string passport, DateTime checkInDate, DateTime checkOutDate);

        /// <summary>
        /// Gets all guests from database.
        /// </summary>
        /// <returns> List of the guests.</returns>
        IEnumerable<Guest> GetAllGuests();

        /// <summary>
        /// Changes check out date for given guest.
        /// </summary>
        /// <param name="id"> Guest Id.</param>
        /// <param name="newDate"> New check out date.</param>
        void ChangeCheckOutDate(int id, DateTime newDate);

        /// <summary>
        /// Searches guests by name.
        /// </summary>
        /// <param name="name"> Guest's name.</param>
        /// <returns> List of guests.</returns>
        IEnumerable<Guest> GetGuestsByName(string name);

        /// <summary>
        /// Gets list of guests, which are leaving soon.
        /// </summary>
        /// <param name="days"> Days, during which guests will leave.</param>
        /// <returns> List of leaving soon guests.</returns>
        IEnumerable<Guest> GetListOfLeaveSoonGuests(int days);

        /// <summary>
        /// Counts the days of accommodation.
        /// </summary>
        /// <param name="id"> Guest Id.</param>
        /// <returns> Days of accommodation.</returns>
        int CalculateTheLengthOfStay(int id);

        /// <summary>
        /// Calculates price of accommodation.
        /// </summary>
        /// <param name="id"> Guest Id.</param>
        /// <returns> Price of accommodation.</returns>
        int CalculatePriceOfStay(int id);

        /// <summary>
        /// Evicts given guest.
        /// </summary>
        /// <param name="id"> Guest Id.</param>
        void EvictGuest(int id);
    }
}
