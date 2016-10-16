// <copyright file="SqlGuestRepository.cs" company="My Company">
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
    /// Represents repository for working with Guest entities.
    /// </summary>
    public class SqlGuestRepository : SqlBaseRepository, IGuestRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlGuestRepository"/> class by given connection. 
        /// </summary>
        /// <param name="connection"> Connection string.</param>
        public SqlGuestRepository(string connection) 
            : base(connection)
        {
        }

        /// <summary>
        /// Calculates price of accommodation.
        /// </summary>
        /// <param name="id"> Guest Id.</param>
        /// <returns> Price of accommodation.</returns>
        public int CalculatePriceOfStay(int id)
        {
            int sum = 0;

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spCalculatePriceOfStay", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@guestId", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sum = (int)reader["result"];
                        }
                    }     
                }
            }

            return sum;
        }

        /// <summary>
        /// Counts the days of accommodation.
        /// </summary>
        /// <param name="id"> Guest Id.</param>
        /// <returns> Days of accommodation.</returns>
        public int CalculateTheLengthOfStay(int id)
        {
            int days = 0;

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spCalculateTheLengthOfStay", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@guestId", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            days = (int)reader["result"];
                        }
                    }
                }
            }

            return days;
        }

        /// <summary>
        /// Changes check out date for given guest.
        /// </summary>
        /// <param name="id"> Guest Id.</param>
        /// <param name="newDate"> New check out date.</param>
        public void ChangeCheckOutDate(int id, DateTime newDate)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spChangeCheckOutDate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@newDate", newDate);               
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Evicts given guest.
        /// </summary>
        /// <param name="id"> Guest Id.</param>
        public void EvictGuest(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spEvictGuest", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets all guests from database.
        /// </summary>
        /// <returns> List of the guests.</returns>
        public IEnumerable<Guest> GetAllGuests()
        {
            var guestList = new List<Guest>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spGetAllGuests", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            guestList.Add(new Guest
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Passport = (string)reader["passport"],
                                Room = (int)reader["room"],
                                CheckInDate = (DateTime)reader["check_in_date"],
                                CheckOutDate = (DateTime)reader["check_out_date"]
                            });
                        }
                    }
                }
            }

            return guestList;
        }

        /// <summary>
        /// Searches guests by name.
        /// </summary>
        /// <param name="name"> Guest's name.</param>
        /// <returns> List of guests.</returns>
        public IEnumerable<Guest> GetGuestsByName(string name)
        {
            var guestList = new List<Guest>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spGetGuestsByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", name);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            guestList.Add(new Guest
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Passport = (string)reader["passport"],
                                Room = (int)reader["room"],
                                CheckInDate = (DateTime)reader["check_in_date"],
                                CheckOutDate = (DateTime)reader["check_out_date"]
                            });
                        }
                    }
                }
            }

            return guestList;
        }

        /// <summary>
        /// Gets list of guests, which are leaving soon.
        /// </summary>
        /// <param name="days"> Days, during which guests will leave.</param>
        /// <returns> List of leaving soon guests.</returns>
        public IEnumerable<Guest> GetListOfLeaveSoonGuests(int days)
        {
            var guestList = new List<Guest>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spGetListOfLeaveSoonGuests", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@days", days);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            guestList.Add(new Guest
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Passport = (string)reader["passport"],
                                Room = (int)reader["room"],
                                CheckInDate = (DateTime)reader["check_in_date"],
                                CheckOutDate = (DateTime)reader["check_out_date"]
                            });
                        }
                    }
                }
            }

            return guestList;
        }

        /// <summary>
        /// Settles guest to the given room.
        /// </summary>
        /// <param name="roomId"> Room Id.</param>
        /// <param name="name"> Guest's name.</param>
        /// <param name="passport"> Guest's passport.</param>
        /// <param name="checkInDate"> Guest's check in date.</param>
        /// <param name="checkOutDate"> Guest's check out date.</param>
        public void SettleGuestInTheRoom(int roomId, string name, string passport, DateTime checkInDate, DateTime checkOutDate)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("spSettleGuestInTheRoom", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@roomId", roomId);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@passport", passport);
                    command.Parameters.AddWithValue("@check_in_date", checkInDate);
                    command.Parameters.AddWithValue("@check_out_date", checkOutDate);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
