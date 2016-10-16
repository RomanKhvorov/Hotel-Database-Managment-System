using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using HotelManagementSystem.Repositories;
using HotelManagementSystem.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using HotelManagementSystem.DesktopUI.Code;

namespace HotelManagementSystem.DesktopUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        IEnumerable<Room> rooms = new List<Room>();
        IEnumerable<Guest> guests = new List<Guest>();

        public MainWindow()
        {
            InitializeComponent();

            var roomsRep = new SqlRoomRepository(connection);
            var guestsRep = new SqlGuestRepository(connection);

            rooms = roomsRep.GetAllRooms();
            guests = guestsRep.GetAllGuests();

            dgRooms.ItemsSource = rooms;
            dgGuests.ItemsSource = guests;

            var sb = new StringBuilder();

            sb.AppendLine("№");
            sb.AppendLine("Category");
            sb.AppendLine("Places");
            sb.AppendLine("Prices");
            sb.AppendLine("TV");
            sb.AppendLine("Conditioner");
            sb.AppendLine("Internet");
            sb.AppendLine("Jacuzzi");

            tbInfoAboutRoom1.Text = sb.ToString();

            sb = new StringBuilder();

            sb.AppendLine("Name");
            sb.AppendLine("Passport");
            sb.AppendLine("Room");
            sb.AppendLine("Price");
            sb.AppendLine("Check in date");
            sb.AppendLine("Check out date");

            tbInfoAboutGuest.Text = sb.ToString();
                
            dpCheckOutDate.SelectedDate = DateTime.Now.AddDays(1);
        }

        private void dgRooms_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgRooms.SelectedIndex >= 0)
            {
                var id = ((Room)dgRooms.SelectedItem).Id;

                WriteInfoAboutRoom(id);

                tbUpdatedRoomId.Text = id.ToString();
                cbUpdatedRoomCategory.SelectedValue = ((Room)dgRooms.SelectedItem).CategoryName;
                cbUpdatedRoomPlaces.SelectedValue = ((Room)dgRooms.SelectedItem).Places;
                tbUpdatedRoomPrice.Text = ((Room)dgRooms.SelectedItem).Price.ToString();

                tbSettleRoomId.Text = id.ToString();
            }
        }

        private void WriteInfoAboutRoom(int id)
        {
            var info = (new SqlRoomRepository(connection)).GetInfoAboutRoom(id);

            var sb = new StringBuilder();

            foreach (var i in info)
            {
                sb.AppendLine(i);
            }

            sb.Replace("False", "-");
            sb.Replace("True", "+");

            tbInfoAboutRoom2.Text = sb.ToString();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool onlyFree = (bool)chbFreeRoomOnly.IsChecked;

                StringBuilder category = new StringBuilder();

                if (!(bool)chbEconom.IsChecked && !(bool)chbStandard.IsChecked && !(bool)chbSuit.IsChecked && !(bool)chbFamily.IsChecked)
                {
                    category.Append("EconomStandardSuitFamily");
                }
                else
                {
                    if ((bool)chbEconom.IsChecked) category.Append("Econom");
                    if ((bool)chbStandard.IsChecked) category.Append("Standard");
                    if ((bool)chbSuit.IsChecked) category.Append("Suit");
                    if ((bool)chbFamily.IsChecked) category.Append("Family");
                }

                int places = int.Parse(cbPlaces.SelectedValue.ToString());

                int priceLow = 0;

                if (tbLowPrice.Text != "")
                {
                    priceLow = int.Parse(tbLowPrice.Text);
                }

                int priceUp = 100000;

                if (tbUpPrice.Text != "")
                {
                    priceUp = int.Parse(tbUpPrice.Text);
                }

                var roomsRep = new SqlRoomRepository(connection);

                rooms = roomsRep.FilterRooms(category.ToString(), places, priceLow, priceUp, onlyFree);

                dgRooms.ItemsSource = rooms;
            }
            catch
            {
                MessageBox.Show("Some problem occured. Check input please.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            var roomsRep = new SqlRoomRepository(connection);

            rooms = roomsRep.GetAllRooms();

            dgRooms.ItemsSource = rooms;

            chbFreeRoomOnly.IsChecked = false;
            chbEconom.IsChecked = false;
            chbStandard.IsChecked = false;
            chbSuit.IsChecked = false;
            chbFamily.IsChecked = false;
            cbPlaces.SelectedIndex = 0;
            tbLowPrice.Text = "";
            tbUpPrice.Text = "";
        }

        private void btnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = 0;

                try
                {
                    id = ((Room)dgRooms.SelectedItem).Id;
                }
                catch
                {
                    throw new Exception("Select room please.");
                }

                if (!((Room)dgRooms.SelectedItem).IsFree)
                {
                    throw new Exception("Room isn't free currently. You can't delete it.");
                }

                if (MessageBox.Show("Do you really want to delete this room?", "Info", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var roomsRep = new SqlRoomRepository(connection);

                    roomsRep.DeleteRoom((int)id);

                    rooms = roomsRep.GetAllRooms();

                    dgRooms.ItemsSource = rooms;

                    tbUpdatedRoomId.Text = "";
                    tbSettleRoomId.Text = "";                   

                    MessageBox.Show("Room was successfully deleted.", "Info", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = 0;
                var category = "";
                var places = 0;
                var price = 0.0;

                try
                {
                    id = int.Parse(tbNewRoomId.Text);

                    category = cbNewRoomCategory.SelectedValue.ToString();

                    places = int.Parse(cbNewRoomPlaces.SelectedValue.ToString());

                    price = double.Parse(tbNewRoomPrice.Text);
                }
                catch
                {
                    throw new Exception("Some problem occured. Check input please.");
                }

                if (id <= 0 || price <= 0)
                {
                    throw new Exception("ID and price must be positive numbers.");
                }

                var roomsRep = new SqlRoomRepository(connection);
                var allRooms = roomsRep.GetAllRooms();
                
                if (allRooms.Select(room => room.Id).Contains(id))
                {
                    throw new Exception("Room №" + id + " already exists.");
                }

                int categoryID = 0;

                switch (category)
                {
                    case "Econom":
                        categoryID = 1;
                        break;
                    case "Standard":
                        categoryID = 2;
                        break;
                    case "Suit":
                        categoryID = 3;
                        break;
                    case "Family":
                        categoryID = 4;
                        break;
                }

                roomsRep.AddRoom(id, categoryID, places, (int)price);

                rooms = roomsRep.GetAllRooms();

                dgRooms.ItemsSource = rooms;

                tbNewRoomId.Text = "";
                cbNewRoomCategory.SelectedIndex = 0;
                cbNewRoomPlaces.SelectedIndex = 0;
                tbNewRoomPrice.Text = "";
                expAddRoom.IsExpanded = false;

                MessageBox.Show("New room was successfully added.", "Info", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnChangeRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbUpdatedRoomId.Text == "")
                    throw new Exception("Select room please.");

                var id = 0;
                var category = "";
                var places = 0;
                var price = 0.0;

                try
                {
                    id = int.Parse(tbUpdatedRoomId.Text);

                    category = cbUpdatedRoomCategory.SelectedValue.ToString();

                    places = int.Parse(cbUpdatedRoomPlaces.SelectedValue.ToString());

                    price = double.Parse(tbUpdatedRoomPrice.Text);
                }
                catch
                {
                    throw new Exception("Some problem occured. Check input please.");
                }

                if (price <= 0)
                {
                    throw new Exception("Price must be a positive number.");
                }

                int categoryID = 0;

                switch (category)
                {
                    case "Econom":
                        categoryID = 1;
                        break;

                    case "Standard":
                        categoryID = 2;
                        break;

                    case "Suit":
                        categoryID = 3;
                        break;

                    case "Family":
                        categoryID = 4;
                        break;
                }

                var roomsRep = new SqlRoomRepository(connection);
                var allRooms = roomsRep.GetAllRooms();

                if (!allRooms.Where(room => room.Id == id).First().IsFree)
                {
                    throw new Exception("Room isn't free currently. You can't change it.");
                }

                if (MessageBox.Show("Do you really want to change this room?", "Info", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {                    
                    roomsRep.ChangeRoom(id, categoryID, places, (int)price);

                    rooms = roomsRep.GetAllRooms();

                    dgRooms.ItemsSource = rooms;

                    expAddRoom.IsExpanded = false;

                    WriteInfoAboutRoom(id);

                    expChangeRoom.IsExpanded = false;

                    MessageBox.Show("Room was successfully changed.", "Info", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnSettleGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbSettleRoomId.Text == "")
                    throw new Exception("Select room please.");

                var id = 0;
                var name = "";
                var passport = "";
                var checkIn = DateTime.Now;
                var checkOut = DateTime.Now;

                try
                {
                    id = int.Parse(tbSettleRoomId.Text);

                    name = tbSettleGuestName.Text;

                    passport = tbSettleGuestPassport.Text;
                   
                    checkOut = (DateTime)dpCheckOutDate.SelectedDate;
                }
                catch
                {
                    throw new Exception("Some problem occured. Check input please.");
                }

                if (name == "" || passport == "")
                {
                    throw new Exception("'Name' and 'passport' fields can't be empty.");
                }

                if (checkIn >= checkOut)
                {
                    throw new Exception("Check in date must be greater than check out.");
                }

                var guestsRep = new SqlGuestRepository(connection);
                var roomsRep = new SqlRoomRepository(connection);

                if (!((Room)dgRooms.SelectedItem).IsFree)
                {
                    throw new Exception("Room isn't free currently. You can't settle guest here.");
                }

                guestsRep.SettleGuestInTheRoom(id, name, passport, checkIn, checkOut);

                rooms = roomsRep.GetAllRooms();
                guests = guestsRep.GetAllGuests();

                dgRooms.ItemsSource = rooms;
                dgGuests.ItemsSource = guests;

                expSettleGuest.IsExpanded = false;

                MessageBox.Show("Guest was successfully settled.", "Info", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void tbSearchGuests_TextChanged(object sender, TextChangedEventArgs e)
        {
            var guestsRep = new SqlGuestRepository(connection);

            if (tbSearchGuests.Text == "")
            {
                guests = guestsRep.GetAllGuests();
            }

            guests = guestsRep.GetGuestsByName(tbSearchGuests.Text);

            dgGuests.ItemsSource = guests;
        }

        private void btnShowFreedSoonRooms_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var days = int.Parse(cbFreedSoonRooms.SelectedValue.ToString());

                var roomsRep = new SqlRoomRepository(connection);

                var freedRooms = roomsRep.GetListOfFreedSoonRooms(days);

                dgFreedSoonRooms.ItemsSource = freedRooms;
                dgFreedSoonRooms.Visibility = Visibility.Visible;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dgGuests_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgGuests.SelectedIndex >= 0)
            {
                var id = ((Guest)dgGuests.SelectedItem).Id;

                var guestsRep = new SqlGuestRepository(connection);

                var guest = guestsRep.GetAllGuests().Where(g => g.Id == id).Single();

                tbChangeDateGuestName.Text = guest.Name;

                dpChangeCheckOutDate.SelectedDate = guest.CheckOutDate;

                WriteInfoAboutGuest(id);

                btnCheckOut.IsEnabled = true;
                btnPrintBill.IsEnabled = false;
                btnEvictGuest.IsEnabled = false;

                tbBill.Text = "";
            }
        }

        private void btnChangeCheckOutDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbChangeDateGuestName.Text == "")
                {
                    throw new Exception("Select guest please.");
                }

                var guestsRep = new SqlGuestRepository(connection);
                var id = ((Guest)dgGuests.SelectedItem).Id;
                var guest = guestsRep.GetAllGuests().Where(g => g.Id == id).Single();

                var newDate = (DateTime)dpChangeCheckOutDate.SelectedDate;

                if (newDate <= guest.CheckInDate || newDate < DateTime.Now.Date)
                {
                    throw new Exception("New check out date isn't correct.");
                }

                guestsRep.ChangeCheckOutDate(id, newDate);

                guests = guestsRep.GetAllGuests();
                dgGuests.ItemsSource = guests;

                dpChangeCheckOutDate.SelectedDate = newDate;
                expChangeCheckOutDate.IsExpanded = false;

                MessageBox.Show("Check out date was successfully changed.", "Info", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnShowLeavingSoonGuests_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var days = int.Parse(cbLeavingSoonGuests.SelectedValue.ToString());

                var guestsRep = new SqlGuestRepository(connection);

                var leavingGuests = guestsRep.GetListOfLeaveSoonGuests(days);

                dgLeavingSoonGuests.ItemsSource = leavingGuests;
                dgLeavingSoonGuests.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void WriteInfoAboutGuest(int id)
        {
            var guest = (new SqlGuestRepository(connection)).GetAllGuests().Where(g => g.Id == id).Single();
            var roomsRep = new SqlRoomRepository(connection);

            var sb = new StringBuilder();

            sb.AppendLine(guest.Name);
            sb.AppendLine(guest.Passport);
            sb.AppendLine(guest.Room.ToString());
            sb.AppendLine(roomsRep.GetInfoAboutRoom(guest.Room).ToArray()[3]);
            sb.AppendLine(guest.CheckInDate.Date.ToString("d"));
            sb.AppendLine(guest.CheckOutDate.Date.ToString("d"));

            tbInfoAboutGuest2.Text = sb.ToString();
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            var id = ((Guest)dgGuests.SelectedItem).Id;

            var guestRep = new SqlGuestRepository(connection);
            var roomsRep = new SqlRoomRepository(connection);

            var guest = guestRep.GetAllGuests().Where(g => g.Id == id).Single();

            var sb = new StringBuilder();

            sb.AppendLine("Hotel «Epam»");
            sb.AppendLine("Lviv");
            sb.AppendLine("221 Volodymyra Velykoho st.");
            sb.AppendLine();
            sb.AppendLine("Administrator " + CurrentAdministrator.Name);
            sb.AppendLine("----------------------------------");
            sb.AppendLine("Days of accomodation:      " + guestRep.CalculateTheLengthOfStay(id));
            sb.AppendLine("X");
            sb.AppendLine("Room's price:              " + roomsRep.GetInfoAboutRoom(guest.Room).ToArray()[3]);
            sb.AppendLine("----------------------------------");
            sb.AppendLine("Sum                            " + guestRep.CalculatePriceOfStay(id));
            sb.AppendLine("----------------------------------");
            sb.AppendLine(DateTime.Now.ToString());

            tbBill.Text = sb.ToString();

            btnPrintBill.IsEnabled = true;
            btnEvictGuest.IsEnabled = true;
        }

        private void PrintTextBox(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(tbBill.Text, new Font("Arial", 10), System.Drawing.Brushes.Black, 10, 25);
        }

        private void btnPrintBill_Click(object sender, RoutedEventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintTextBox;
            printDocument.Print();

            tbBill.Text = "";
        }

        private void btnEvictGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = 0;

                try
                {
                    id = ((Guest)dgGuests.SelectedItem).Id;
                }
                catch
                {
                    throw new Exception("Select guest please.");
                }

                if (MessageBox.Show("Do you really want to evict this guest?", "Info", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var guestsRep = new SqlGuestRepository(connection);
                    var roomsRep = new SqlRoomRepository(connection);

                    guestsRep.EvictGuest(id);

                    guests = guestsRep.GetAllGuests();
                    rooms = roomsRep.GetAllRooms();

                    dgGuests.ItemsSource = guests;
                    dgRooms.ItemsSource = rooms;

                    tbInfoAboutGuest2.Text = "";

                    btnCheckOut.IsEnabled = false;
                    btnPrintBill.IsEnabled = false;
                    btnEvictGuest.IsEnabled = false;

                    tbChangeDateGuestName.Text = "";
                    dpChangeCheckOutDate.ClearValue(DatePicker.SelectedDateProperty);

                    MessageBox.Show("Guest was successfully evicted.", "Info", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
