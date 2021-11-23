﻿using primestayWpf.Forms;
using primestayWpf.src.auth;
using PrimestayWPF.DataAccessLayer;
using PrimestayWPF.DataAccessLayer.DAO;
using PrimestayWPF.DataAccessLayer.DTO;
using System.Windows;

namespace primestayWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDataContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = RestDataContext.GetInstance();
        }


        private void hotelCrudBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Auth.IsLoggedIn) new HotelMenu(DaoFactory.Create<HotelDto>(_context, Auth.AccessToken)).ShowDialog();
            else MessageBox.Show("Login to acces Hotels", "Error", MessageBoxButton.OK);
        }

        private void authScreenbtn_Click(object sender, RoutedEventArgs e)
        {
            new AuthWindow(DaoFactory.Create<UserDto>(_context, Auth.AccessToken)).ShowDialog();
        }

        private void roomTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Auth.IsLoggedIn) new RoomTypeMenu(DaoFactory.Create<RoomTypeDto>(_context, Auth.AccessToken)).ShowDialog();
            else MessageBox.Show("Login to access Room types", "Error", MessageBoxButton.OK);
        }

        private void customerCrudBtn_Click(object sender, RoutedEventArgs e)
        {
            new CustomerMenu(DaoFactory.Create<CustomerDto>(_context, Auth.AccessToken)).ShowDialog();
        }
    }
}
