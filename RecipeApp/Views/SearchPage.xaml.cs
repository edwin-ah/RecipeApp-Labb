using RecipeApp.Models;
using RecipeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private SearchPageViewModel vm;
        public SearchPage()
        {
            InitializeComponent();
            vm = new SearchPageViewModel();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            //Hämta switch och ändra färgen varje gång den blir toggled
            var switchComponent = (Switch)sender;
            switchComponent.OnColor = e.Value ? Color.FromHex("#1a6558") : Color.FromHex("#767577");
            switchComponent.ThumbColor = e.Value ? Color.FromHex("#fff") : Color.FromHex("#f4f3f4");
            stackFilters.IsVisible = e.Value;

        }
        private void searchBarSearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            vm.OnTextChanged(e.NewTextValue);
        }

        private void pickerCuisine_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int index = picker.SelectedIndex;
            if(index != -1)
            {
                vm.OnSelectedCuisine(index);
            }
        }

        private void pickerDiets_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int index = picker.SelectedIndex;
            if (index != -1)
            {
                vm.OnSelectedDiet(index);
            }
        }

        private void pickerIntolerances_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int index = picker.SelectedIndex;
            if (index != -1)
            {
                vm.OnSelectedIntolerance(index);
            }
        }

        private async void listRecipes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var recipe = (Recipe)e.Item;
            int recipeId = recipe.Id;
            //Skicka med Id till detailspage
            var detailsPage = new RecipeDetailsPage(recipeId);
            await Navigation.PushAsync(detailsPage);
        }
    }
}