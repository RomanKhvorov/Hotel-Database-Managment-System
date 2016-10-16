// <copyright file="SqlRoomRepository.cs" company="My Company">
//     Copyright (c) My Company. All rights reserved.
// </copyright>
// <author>Roman Khvorov</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using HotelManagementSystem.Entities;

namespace HotelManagementSystem.Repositories
{
    /// <summary>
    /// Represents repository for working with Room entities.
    /// </summary>
    public class SqlRoomRepository : SqlBaseRepository, IRoomRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlRoomRepository"/> class by given connection. 
        /// </summary>
        /// <param name="connection"> Connection string.</param>
        public SqlRoomRepository(string connection) 
            : base(connection)
        {
        }

        /// <summary>
        /// Adds new room into database.
        /// </summary>
        /// <param name="id"> Id of the room.</param>
        /// <param name="category"> Category of the room.</param>
        /// <param name="places"> Count of the places.</param>
        /// <param name="price"> Price of the room.</param>
        public void AddRoom(int id, int category, int places, int price)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spAddRoom", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@category", category);
                    command.Parameters.AddWithValue("@places", places);
                    command.Parameters.AddWithValue("@price", price);
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Changes a given room.
        /// </summary>
        /// <param name="id"> Id of the room.</param>
        /// <param name="category"> New category.</param>
        /// <param name="places"> New count of places.</param>
        /// <param name="price"> New price.</param>
        public void ChangeRoom(int id, int category, int places, int price)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spChangeRoom", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@category", category);
                    command.Parameters.AddWithValue("@places", places);
                    command.Parameters.AddWithValue("@price", price);
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletes room from database.
        /// </summary>
        /// <param name="id"> Id of the room.</param>
        public void DeleteRoom(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spDeleteRoom", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Filters rooms by any criteria.
        /// </summary>
        /// <param name="category"> Category of the room.</param>
        /// <param name="places"> Count of the places.</param>
        /// <param name="priceLow"> Lower price limit.</param>
        /// <param name="priceUp"> Upper price limit.</param>
        /// <param name="onlyFree"> Get only free rooms.</param>
        /// <returns> Filtering list of the rooms.</returns>
        public IEnumerable<Room> FilterRooms(string category, int places, int priceLow, int priceUp, bool onlyFree)
        {
            var roomList = new List<Room>(); 

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spFilterRooms", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@category", category);
                    command.Parameters.AddWithValue("@places", places);
                    command.Parameters.AddWithValue("@priceLow", priceLow);
                    command.Parameters.AddWithValue("@priceUp", priceUp);
                    command.Parameters.AddWithValue("@onlyFree", onlyFree);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roomList.Add(new Room
                            {
                                Id = (int)reader["id"],
                                CategoryID = (int)reader["category"],
                                CategoryName = (string)reader["categoryName"],
                                Places = (int)reader["places"],
                                Price = (int)reader["price"],
                                IsFree = (bool)reader["is_free"]
                            });
                        }
                    }
                }
            }

            return roomList;
        }

        /// <summary>
        /// Gets all free rooms from database.
        /// </summary>
        /// <returns> List of the free rooms.</returns>
        public IEnumerable<Room> GetAllFreeRooms()
        {
            var roomList = new List<Room>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spGetAllFreeRooms", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roomList.Add(new Room
                            {
                                Id = (int)reader["id"],
                                CategoryID = (int)reader["category"],
                                CategoryName = (string)reader["categoryName"],
                                Places = (int)reader["places"],
                                Price = (int)reader["price"],
                                IsFree = (bool)reader["is_free"]
                            });
                        }
                    }
                }
            }

            return roomList;
        }

        /// <summary>
        /// Gets all rooms from database.
        /// </summary>
        /// <returns> List of the rooms.</returns>
        public IEnumerable<Room> GetAllRooms()
        {
            var roomList = new List<Room>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spGetAllRooms", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roomList.Add(new Room
                            {
                                Id = (int)reader["id"],
                                CategoryID = (int)reader["category"],
                                CategoryName = (string)reader["categoryName"],
                                Places = (int)reader["places"],
                                Price = (int)reader["price"],
                                IsFree = (bool)reader["is_free"]
                            });
                        }
                    }
                }
            }

            return roomList;
        }

        /// <summary>
        /// Gets info about given room.
        /// </summary>
        /// <param name="id"> Room id.</param>
        /// <returns> Info about room.</returns>
        public IEnumerable<string> GetInfoAboutRoom(int id)
        {
            var info = new List<string>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spGetInfoAboutRoom", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            info.Add(reader["id"].ToString());
                            info.Add(reader["category"].ToString());
                            info.Add(reader["places"].ToString());
                            info.Add(reader["price"].ToString());
                            info.Add(reader["TV"].ToString());
                            info.Add(reader["conditioner"].ToString());
                            info.Add(reader["internet"].ToString());
                            info.Add(reader["jacuzzi"].ToString());
                        }
                    }
                }
            }

            return info;
        }

        /// <summary>
        /// Gets list of the rooms that will be free soon.
        /// </summary>
        /// <param name="days"> Days, during which rooms will be free.</param>
        /// <returns> List of freed soon rooms.</returns>
        public IEnumerable<Room> GetListOfFreedSoonRooms(int days)
        {
            var roomList = new List<Room>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spGetListOfFreedSoonRooms", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@days", days);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roomList.Add(new Room
                            {
                                Id = (int)reader["id"],
                                CategoryID = (int)reader["category"],
                                CategoryName = (string)reader["categoryName"],
                                Places = (int)reader["places"],
                                Price = (int)reader["price"],
                                IsFree = (bool)reader["is_free"],
                                FreeDate = (DateTime)reader["check_out_date"]
                            });
                        }
                    }
                }
            }

            return roomList;
        }
    }
}
