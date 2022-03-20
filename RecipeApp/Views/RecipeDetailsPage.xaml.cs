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
    public partial class RecipeDetailsPage : ContentPage
    {
        private RecipeDetailsViewModel vm;
        public RecipeDetailsPage(int recipeId)
        {
            InitializeComponent();
            vm = new RecipeDetailsViewModel { RecipeId = recipeId };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await vm.GetRecipe();
            //Om inte BindingContext är med här fungerar det inte med data binding
            BindingContext = vm;

            //UI blev konstigt med ScrollView som innehöll ListView, därför fylls ingredienser och instruktioner härifrån
            FillIngredients();
            FillInstructions();
        }

        private void FillIngredients()
        {
            foreach (var ingredient in vm.Recipe.ExtendedIngredients)
            {
                StackLayout ingredientRow = new StackLayout();

                ingredientRow.Children.Add(new Label
                {
                    Text = ingredient.Original,
                    Padding = new Thickness(10, 5, 10, 0)
                });

                IngredientsList.Children.Add(ingredientRow);
            }
        }

        private void FillInstructions()
        {
            if (vm.HasNoInstructions)
            {
                return;
            }

            foreach (var instruction in vm.Recipe.AnalyzedInstructions[0].Steps)
            {
                StackLayout instructionRow = new StackLayout
                {
                    Padding = new Thickness(5, 10, 0, 20),
                    Orientation = StackOrientation.Horizontal
                };

                StackLayout frameStack = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center
                };

                Frame numberFrame = new Frame
                {
                    HeightRequest = 25,
                    MinimumHeightRequest = 25,
                    WidthRequest = 25,
                    MinimumWidthRequest = 25,
                    CornerRadius = 150,
                    BackgroundColor = Color.Transparent,
                    BorderColor = Color.Black,
                    Padding = new Thickness(0),
                    Margin = new Thickness(10, 0, 0, 0),
                    Content = new Label 
                    {
                        Text = instruction.Number.ToString(),
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 12,
                        FontAttributes = FontAttributes.Bold
                    }
                };

                frameStack.Children.Add(numberFrame);
                instructionRow.Children.Add(frameStack);

                Label instructionText = new Label
                {
                    Text = instruction.Step,
                    Margin = new Thickness(10, 0, 0, 0)
                };

                instructionRow.Children.Add(instructionText);

                InstructionsList.Children.Add(instructionRow);
            }

        }
    }
}