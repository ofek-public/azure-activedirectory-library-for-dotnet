﻿using Microsoft.Experimental.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using TestApp.PCL;
using Xamarin.Forms;

namespace AdalXamarinFormsTestApp
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            var secondPageButton = new Button
            {
                Text = "Second Page"
            };

            secondPageButton.Clicked += browseButton_Clicked;

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    secondPageButton
				}
            };
        }

        async void browseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SecondPage());
        }
    }
}