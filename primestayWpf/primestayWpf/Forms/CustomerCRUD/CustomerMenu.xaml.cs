﻿using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf.Forms;
using primestayWpf.src.auth;
using PrimestayWpf.Model;
using PrimestayWPF.DataAccessLayer.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace primestayWpf.Forms.CustomerCRUD
{
    public partial class CustomerMenu : Window
    {
        private readonly IDao<CustomerDto> dao;
        private ObservableCollection<Customer> CustomerList { get; set; } = new ObservableCollection<Customer>();

        public CustomerMenu(IDao<CustomerDto> _dao)
        {
            InitializeComponent();
            dao = _dao;
            CustomerListView.ItemsSource = CustomerList;
            UpdateList();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var customer = CustomerListView.SelectedItem as Customer;
            string text = $"Are you sure that you would like to delete{customer?.Phone ?? "this customer"}?";
            if(MessageBox.Show(text, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (MessageBox.Show($"Are you sure you would like to delete {customer.Phone}?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var res = dao.Delete(customer.Map(), Auth.AccessToken);
                    UpdateList();
                    if (res > 0) MessageBox.Show($"Customer{customer.Phone} was deleted");
                    else MessageBox.Show($"Could not delete {customer.Phone}, contact admin");
                }
            }
        }
        
        private void Add(object sender, RoutedEventArgs e)
        {

        }


        private void UpdateList()
        {
            var customers = dao.ReadAll(new CustomerDto(), Auth.AccessToken).Select(x => x.Map());
            CustomerList.Clear();
            customers.ToList().ForEach(x => CustomerList.Add(x));
        }

    }
}
