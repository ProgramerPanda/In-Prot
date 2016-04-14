using RestSharp.Portable;
using RestSharp.Portable.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
            
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            var request = new RestRequest("http://localhost:3000/oauth/token", Method.POST);
            var client = new RestSharp.Portable.HttpClient.RestClient("http://localhost:3000/");
            client.IgnoreResponseStatusCode = true;

            request.AddBody(new
            {
                client_id = "484635247e3c19981631ce647990334627ea514586be5fa42f5fee80cbf8bca2",
                client_secret = "e8a4103368c08631bb8cafcd431c75c2ca99bb35fa3dd75f08bc2eaa5376d75e",
                grant_type = "password",
                password = passwordBox.Password,
                username = textBox3.Text
            });

            var response = await client.Execute(request).ConfigureAwait(true);
            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;
            if (numericStatusCode == 200)
            {
                JsonDeserializer deserial = new JsonDeserializer();
                Token token = deserial.Deserialize<Token>(response);
                ApplicationState.CurrentToken = token;

                var dialog = new Windows.UI.Popups.MessageDialog("Zalogowano poprawnie, nastąpi przekierowanie do strony głównej");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                dialog.CancelCommandIndex = 0;

                var result = await dialog.ShowAsync();

                var btn = sender as Button;
                btn.Content = $"Result: {result.Label} ({result.Id})";
                Frame.Navigate(typeof(MainPage), null);
            }
            if (numericStatusCode == 401)
            {
                var dialog = new Windows.UI.Popups.MessageDialog("Niepoprawna nazwa użytkownika lub hasło");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                dialog.CancelCommandIndex = 0;

                var result = await dialog.ShowAsync();

                var btn = sender as Button;
                //btn.Content = $"Result: {result.Label} ({result.Id})";                
                passwordBox.Password = "";

            }
        }
    }
}
