﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace KrungsriAppAdmin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MotherTabbedPage : Xamarin.Forms.TabbedPage
    {
        public MotherTabbedPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}