// <copyright file="IRoomRepository.cs" company="My Company">
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
    /// Interface for classes, that work with Room entities.
    /// </summary>
    public interface IRoomRepository
    {
        /// <summary>
        /// Gets all rooms from database.
        /// </summary>
        /// <returns> List of the rooms.</returns>
        IEnumerable<Room> GetAllRooms();

        /// <summary>
        /// Gets all free rooms from database.
        /// </summary>
        /// <returns> List of the free rooms.</returns>
        IEnumerable<Room> GetAllFreeRooms();

        /// <summary>
        /// Adds new room into database.
        /// </summary>
        /// <param name="id"> Id of the room.</param>
        /// <param name="category"> Category of the room.</param>
        /// <param name="places"> Count of places.</param>
        /// <param name="price"> Price of the room.</param>
        void AddRoom(int id, int category, int places, int price);

        /// <summary>
        /// Changes a given room.
        /// </summary>
        /// <param name="id"> Id of the room.</param>
        /// <param name="category"> New category.</param>
        /// <param name="places"> New count of places.</param>
        /// <param name="price"> New price.</param>
        void ChangeRoom(int id, int category, int places, int price);

        /// <summary>
        /// Deletes room from database.
        /// </summary>
        /// <param name="id"> Id of the room.</param>
        void DeleteRoom(int id);

        /// <summary>
        /// Gets list of the rooms that will be free soon.
        /// </summary>
        /// <param name="days"> Days, during which rooms will be free.</param>
        /// <returns> List of freed soon rooms.</returns>
        IEnumerable<Room> GetListOfFreedSoonRooms(int days);

        /// <summary>
        /// Filters rooms by any criteria.
        /// </summary>
        /// <param name="category"> Category of the room.</param>
        /// <param name="places"> Count of the places.</param>
        /// <param name="priceLow"> Lower price limit.</param>
        /// <param name="priceUp"> Upper price limit.</param>
        /// <param name="onlyFree"> Get only free rooms.</param>
        /// <returns> Filtering list of the rooms.</returns>
        IEnumerable<Room> FilterRooms(string category, int places, int priceLow, int priceUp, bool onlyFree);

        /// <summary>
        /// Gets info about given room.
        /// </summary>
        /// <param name="id"> Room id.</param>
        /// <returns> Info about room.</returns>
        IEnumerable<string> GetInfoAboutRoom(int id);
    }
}
