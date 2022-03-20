using Newtonsoft.Json;
using RecipeApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    public class RecipeDetailsViewModel : ViewModelBase
    {
        public int RecipeId { get; set; }
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

        public async Task GetRecipe()
        {
            IsBusy = true;
            string url = $"https://api.spoonacular.com/recipes/{RecipeId}/information?apiKey=3f274f3d82344e6ea7370def36989cff";
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage resp = await httpClient.GetAsync(url);
                if (resp.IsSuccessStatusCode)
                {
                    string content = await resp.Content.ReadAsStringAsync();
                    Recipe recipe = JsonConvert.DeserializeObject<Recipe>(content);
                    Recipe = recipe;
                    if(recipe.AnalyzedInstructions.Count == 0)
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
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }
            IsBusy = false;
        }
    }
}
