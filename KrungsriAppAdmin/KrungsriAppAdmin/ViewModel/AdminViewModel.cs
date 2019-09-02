using KrungsriAppAdmin.Models;
using KrungsriAppAdmin.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KrungsriAppAdmin.ViewModel
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AdminViewModel()
        {
            SignInButton = new Command(async => LogIn());
            QrCodeModel qrCode = new QrCodeModel
            {
                Name = Preferences.Get("Name", ""),
                BookBank = Preferences.Get("BookBank", ""),
                MoneyAmount = Preferences.Get("MoneyAmount", ""),
                Ref = Preferences.Get("Ref", ""),
                ExpiryDate = Preferences.Get("ExpiryDate", "")
            };
            QRCodeValue = JsonConvert.SerializeObject(qrCode);
            PushToCreateQRCode = new Command(PushToQRPage);
            PushToInputMon = new Command(PushToInputMoney);
            Name = Preferences.Get("Name", "");
            MoneyAmount = Preferences.Get("MoneyAmount", "");
            Reference = Preferences.Get("Ref", "");
            ExpiryDate = Preferences.Get("ExpiryDate", "");
            BackToHome = new Command(BackHome);
        }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        //public string Reference { get; set; }
        private string reference;

        public string Reference
        {
            get { return reference; }
            set
            {
                reference = value;
                OnPropertyChanged(nameof(Reference));
            }
        }

        //public string ExpiryDate { get; set; }
        private string expiryDate;

        public string ExpiryDate
        {
            get { return expiryDate; }
            set
            {
                expiryDate = value;
                OnPropertyChanged(nameof(ExpiryDate));
            }
        }
        //public List<MonthlyTrans> Trans { get; set; }
        private ObservableCollection<MonthlyTrans> trans;

        public ObservableCollection<MonthlyTrans> Trans
        {
            get { return trans; }
            set
            {
                trans = value;
                OnPropertyChanged(nameof(Trans));
            }
        }

        public string QRCodeValue { get; set; }
        private string moneyAmount;
        public string MoneyAmount
        {
            get { return moneyAmount; }
            set
            {
                moneyAmount = value;
                OnPropertyChanged(nameof(MoneyAmount));
            }
        }

        public ICommand SignInButton { get; set; }
        public ICommand PushToCreateQRCode { get; set; }
        public ICommand PushToInputMon { get; set; }
        public ICommand BackToHome { get; set; }
        public async void BackHome()
        {
            await MonthlyTransaction();
            Application.Current.MainPage.Navigation.PushAsync(new MotherTabbedPage());
        }
        async Task LogIn()
        {
            try
            {
                var objToApi = new
                {
                    UserName = UserName,
                    Password = Password
                };

                StringContent request = new StringContent($"{JsonConvert.SerializeObject(objToApi)}", Encoding.UTF8, "application/json");
                var client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync("http://192.168.1.34:5000/api/auth/adminlogin", request);
                //response.EnsureSuccessStatusCode();                    
                string responseBody = await response.Content.ReadAsStringAsync();
                LoginModel loginModel = JsonConvert.DeserializeObject<LoginModel>(responseBody);

                if (loginModel.token != null)
                {
                    Preferences.Set("BookBank", loginModel.BookBank);
                    Preferences.Set("Name", loginModel.Name);
                    Preferences.Set("AdminId", loginModel.AdminId);
                    //await MonthlyTransaction();
                    Application.Current.MainPage.Navigation.PushAsync(new MotherTabbedPage());
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
        public async Task MonthlyTransaction()
        {
            try
            {
                var objToApi = new
                {
                    AdminId = Preferences.Get("AdminId", "")                    
                };

                StringContent request = new StringContent($"{JsonConvert.SerializeObject(objToApi)}", Encoding.UTF8, "application/json");
                var client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync("http://192.168.1.34:5000/api/admin/admintranmonthly", request);
                //response.EnsureSuccessStatusCode();                    
                string responseBody = await response.Content.ReadAsStringAsync();
                ObservableCollection<MonthlyTrans> listTrans = JsonConvert.DeserializeObject<ObservableCollection<MonthlyTrans>>(responseBody);
                if (listTrans != null)
                {
                    Trans = listTrans;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
        async Task AddTransaction()
        {
            try
            {
                var objToApi = new
                {
                    AdminId = Preferences.Get("AdminId",""),
                    MoneyAmount = Preferences.Get("MoneyAmount","")                    
                };

                StringContent request = new StringContent($"{JsonConvert.SerializeObject(objToApi)}", Encoding.UTF8, "application/json");
                var client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync("http://192.168.1.34:5000/api/admin/addtranbeforescan", request);
                //response.EnsureSuccessStatusCode();                    
                string responseBody = await response.Content.ReadAsStringAsync();
                TranBfScan tranModel = JsonConvert.DeserializeObject<TranBfScan>(responseBody);
                if (tranModel != null)
                {
                    Preferences.Set("ExpiryDate",tranModel.ExpiryDate);
                    Preferences.Set("Ref", tranModel.Ref);                    
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void PushToQRPage()
        {
            Preferences.Set("MoneyAmount", MoneyAmount);
            await AddTransaction();
            Application.Current.MainPage.Navigation.PushAsync(new GenQrCode());            
        }
        private void PushToInputMoney()
        {
            Application.Current.MainPage.Navigation.PushAsync(new InputMoney());
        }        
        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
