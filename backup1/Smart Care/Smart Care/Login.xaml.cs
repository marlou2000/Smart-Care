﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using MySql.Data.MySqlClient;

namespace Smart_Care
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        MySqlConnection conn;
        string connString;

        public object JsonConvert { get; private set; }

        public Login()
        {
            InitializeComponent();
        }

        private void BindEmployeeList()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.254.114:8080/smartcare/");
            //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = client.GetAsync("try.php").Result;
                if (response.IsSuccessStatusCode)
                {
                    var employees = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show(employees);
                }
                else
                {
                    MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }

            catch
            {
                MessageBox.Show("Connection Failure");
            }

        }

        private async void UploadFile_Cliked(object sender, EventArgs e)
        {
            
            var url = "http://192.168.254.114:8080/smartcare/removePatient.php";

            var client = new HttpClient();

            var data = new Dictionary<string, string>
{
                {"patientID", "96"}
            };

           // var res = client.PostAsync(url, new FormUrlEncodedContent(data));
            //var content = res.Content.ReadAsStringAsync();

            var httpResponseMessage = await client.PostAsync(url, new FormUrlEncodedContent(data));
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            MessageBox.Show(content);


        }



        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = usernameTextBox.Text;
            var password = passwordTextBox.Password;

            var url = "http://192.168.254.114:8080/smartcare/login.php";

            var client = new HttpClient();

            var data = new Dictionary<string, string>
{
                {"username", username},
                {"password", password}
            };

            // var res = client.PostAsync(url, new FormUrlEncodedContent(data));
            //var content = res.Content.ReadAsStringAsync();

            var httpResponseMessage = await client.PostAsync(url, new FormUrlEncodedContent(data));
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            try
            {
                if (content == "success")
                {
                    //string body = content.Substring(0, body.IndexOf("`"));
                    //MessageBox.Show(body);
                    Home homepage = new Home();
                    //this will open your child window
                    homepage.Show();
                    //this will close parent window. windowOne in this case
                    this.Close();
                }

                else
                {
                    MessageBox.Show("Invalid Credentials.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            catch
            {
                MessageBox.Show("Connection Failure");
            }



        }
    }
}
