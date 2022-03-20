using RecipeApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RecipeApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Title = "Start";
        }

        private async void btnRandomRecipePage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RandomRecipePage());
        }

        private async void btnSearchPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }
    }
}
