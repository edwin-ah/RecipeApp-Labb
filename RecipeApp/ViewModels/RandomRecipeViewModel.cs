using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using RecipeApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    public class RandomRecipeViewModel : BaseViewModel
    {
        private const string URL = "https://api.spoonacular.com/recipes/random?apiKey=3f274f3d82344e6ea7370def36989cff";
        private Recipe recipe;
        public Recipe Recipe
        {
            get => recipe;
            set => SetProperty(ref recipe, value);
        }

        private bool hasNoInstructions;

        public bool HasNoInstructions
        {
            get => hasNoInstructions;
            set => SetProperty(ref hasNoInstructions, value);
        }

        private bool hasRecipe;

        public bool HasRecipe
        {
            get => hasRecipe;
            set => SetProperty(ref hasRecipe, value);
        }


        public async Task GetRecipe()
        {
            IsBusy = true;
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage resp = await httpClient.GetAsync(new Uri(URL));
                if (resp.IsSuccessStatusCode)
                {
                    string content = await resp.Content.ReadAsStringAsync();
                    RecipesRoot recipes = JsonConvert.DeserializeObject<RecipesRoot>(content);
                    Recipe = recipes.Recipes[0];
                    HasRecipe = true;
                    if (recipes.Recipes[0].AnalyzedInstructions.Count == 0)
                    {
                        HasNoInstructions = true;
                    }
                    else
                    {
                        HasNoInstructions = false;
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong, could not fetch data " + ex.ToString(), "OK");
            }
            IsBusy = false;
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void RaisePropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
