﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KrungsriAppAdmin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHome : ContentPage
    {
        public AdminHome()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}