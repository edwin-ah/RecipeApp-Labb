using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using RecipeApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    class SearchPageViewModel : BaseViewModel
    {
        private const string URL = "https://api.spoonacular.com/recipes/complexSearch?apiKey=3f274f3d82344e6ea7370def36989cff";

        // Dictionaries som används för att bilda söksträngen till API.
        private Dictionary<string, string> cuisines = new Dictionary<string, string>
        {
            { "Inget specificerat", "" },
            { "Afrika", "&cuisine=African" },
            { "Amerikansk", "&cuisine=American" },
            { "Brittisk", "&cuisine=British" },
            { "Europeiskt", "&cuisine=European" },
            { "Italienskt", "&cuisine=Italian" },
            { "Mellan östern", "&cuisine=Middle%20Eastern" },
            { "Japanskt", "&cuisine=Japanese" },
            { "Koreanskt", "&cuisine=Korean" },
        };
        private Dictionary<string, string> diets = new Dictionary<string, string>
        {
            { "Inget specificerat", "" },
            { "Glutenfritt", "&diet=Gluten%20Free" },
            { "Vegetariskt", "&diet=Vegetarian" },
            { "Lakto-Vegetariskt", "&diet=Lacto-Vegetarian" },
            { "Ovo-Vegetariskt", "&diet=Ovo-Vegetarian" },
            { "Veganskt", "&diet=Ovo-Vegan" },
            { "Pescetarian", "&diet=Pescetarian" },
        };
        private Dictionary<string, string> intolerances = new Dictionary<string, string>
        {
            { "Inget specificerat", "" },
            { "Mejeriprodukter", "&intolerances=Dairy" },
            { "Ägg", "&intolerances=Egg" },
            { "Gluten", "&intolerances=Gluten" },
            { "Jordnötter", "&intolerances=Peanut" },
            { "Skaldjur", "&intolerances=Shellfish" },
            { "Soja", "&intolerances=Soy" },
        };
        //Dessa listor används för att visa Cuisines, Diets och Intolerance i view
        public List<string> Cuisines { get; }
        public List<string> Diets { get; }
        public List<string> Intolerances { get; }

        public string SelectedCuisine { get; set; }

        public string SelectedDiet { get; set; }

        public string SelectedIntolerance { get; set; }

        public string SearchText { get; set; }

        public ObservableRangeCollection<Recipe> RecipeList { get; set; } = new ObservableRangeCollection<Recipe>();

        private bool hasNoResults;

        public bool HasNoResults
        {
            get => hasNoResults;
            set => SetProperty(ref hasNoResults, value);
        }

        public ICommand GetRecipesCommand { get; }


        public SearchPageViewModel()
        {
            Title = "Sök efter recept";
            GetRecipesCommand = new AsyncCommand(GetRecipes);
            Cuisines = cuisines.Keys.ToList();
            Diets = diets.Keys.ToList();
            Intolerances = intolerances.Keys.ToList();
        }

        public void OnSelectedCuisine(int index)
        {
            SelectedCuisine = Cuisines[index];
        }

        public void OnSelectedDiet(int index)
        {
            SelectedDiet = Diets[index];
        }

        public void OnSelectedIntolerance(int index)
        {
            SelectedIntolerance = Intolerances[index];
        }

        public void OnTextChanged(string text)
        {
            SearchText = text;
        }

        private string MakeSearchString()
        {
            string searchString = "";

            if(SearchText.Length > 0)
            {
                searchString += "&query=" + SearchText;
            }
            if(SelectedCuisine != null)
            {
                searchString += cuisines[SelectedCuisine];
            }
            if(SelectedDiet != null)
            {
                searchString += diets[SelectedDiet];
            }
            if(SelectedIntolerance != null)
            {
                searchString += intolerances[SelectedIntolerance];
            }

            return searchString;
        }
        public async Task GetRecipes()
        {
            IsBusy = true;
            try
            {
                RecipeList.Clear();
                var recipes = await FetchRecipesFromAPI();
                RecipeList.AddRange(recipes);
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ett fel uppstod, säkerställ att sökrutan inte är tom", "OK");
            }
            
            IsBusy = false;
        }

        private async Task<IEnumerable<Recipe>> FetchRecipesFromAPI()
        {
            string searchUrl = URL;
            searchUrl += MakeSearchString();
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage resp = await httpClient.GetAsync(new Uri(searchUrl));
                if (resp.IsSuccessStatusCode)
                {
                    string content = await resp.Content.ReadAsStringAsync();
                    RecipesRoot recipesRoot = JsonConvert.DeserializeObject<RecipesRoot>(content);
                    if(recipesRoot.Results.Count() == 0)
                    {
                        HasNoResults = true;
                    }
                    else
                    {
                        HasNoResults = false;
                    }
                    var recipes = recipesRoot.Results;
                    return recipes;
                }
                return null;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
                return null;
            }
        }
    }
}
