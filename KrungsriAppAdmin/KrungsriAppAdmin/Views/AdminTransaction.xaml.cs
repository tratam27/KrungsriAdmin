using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KrungsriAppAdmin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminTransaction : ContentPage
    {
        ViewModel.AdminViewModel vm;

        public AdminTransaction()
        {
            InitializeComponent();

            BindingContext = vm = new ViewModel.AdminViewModel();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await vm.MonthlyTransaction();

        }
    }
}