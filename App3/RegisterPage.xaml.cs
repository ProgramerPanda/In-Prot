using RestSharp.Portable;
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
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            this.InitializeComponent();
            
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestSharp.Portable.HttpClient.RestClient("http://localhost:3000/");
            var request = new RestRequest("http://localhost:3000/api/v1/users/sign_up", Method.POST);
            client.IgnoreResponseStatusCode = true;

            request.AddBody(new
            {
                first_name = textBox.Text,
                last_name = textBox1.Text,
                profile_name = textBox2.Text,
                email = textBox3.Text,
                password = passwordBox.Password,
                password_confirmation = passwordBox1.Password
            });
            var response = await client.Execute(request).ConfigureAwait(true);
            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            if (numericStatusCode == 200)
            {
                var dialog = new Windows.UI.Popups.MessageDialog("Zarejestrowano poprawnie, nastąpi przekierowanie do strony głównej");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                dialog.CancelCommandIndex = 0;

                var result = await dialog.ShowAsync();

                var btn = sender as Button;
                btn.Content = $"Result: {result.Label} ({result.Id})";

                Frame.Navigate(typeof(MainPage), null);
            }
            /*
            if (numericStatusCode== <nr response, email already taken >){

                var dialog = new Windows.UI.Popups.MessageDialog("email jest zajęty, podaj inny email lub zaloguj się");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                dialog.CancelCommandIndex = 0;

                var result = await dialog.ShowAsync();

                var btn = sender as Button;
                btn.Content = $"Result: {result.Label} ({result.Id})";
            }
            */

            /*
            if (numericStatusCode== <nr response, username already taken >){

                var dialog = new Windows.UI.Popups.MessageDialog("username jest zajęty, podaj inny username");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });
                dialog.CancelCommandIndex = 0;

                var result = await dialog.ShowAsync();

                var btn = sender as Button;
                btn.Content = $"Result: {result.Label} ({result.Id})";
            }
            */

        }
    }
}
