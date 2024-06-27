using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ST10260870_Prog6221_POE
{
    public partial class MainWindow : Window
    {
        private List<Ingredient> ingredients;
        private List<string> steps;
        private List<Recipe> recipes;

        public MainWindow()
        {
            InitializeComponent();

            ingredients = new List<Ingredient>();
            steps = new List<string>();
            recipes = new List<Recipe>();
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string caloriesText = txtCalories.Text;
            string unit = txtUnit.Text;
            string quantityText = txtQuantity.Text;
            string foodGroup = txtFoodGroup.Text;
            if (double.TryParse(caloriesText, out double calories) && double.TryParse(quantityText, out double quantity))
            {
                Ingredient ingredient = new Ingredient(name, calories, unit, quantity, foodGroup);

                if (calories > 350)
                {
                    MessageBox.Show("Warning: This ingredient has over 350 calories.", "High Calorie Warning");
                }

                ingredients.Add(ingredient);

                lstIngredients.ItemsSource = null;
                lstIngredients.ItemsSource = ingredients;
                lstIngredients.DisplayMemberPath = "Name";

                ClearIngredientFields();
            }
            else
            {
                MessageBox.Show("Invalid input for calories or quantity. Please enter a valid number.");
            }
        }

        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            string step = txtStep.Text;
            steps.Add(step);

            lstSteps.ItemsSource = null;
            lstSteps.ItemsSource = steps;

            ClearStepField();
        }

        private void DisplayRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = txtRecipeName.Text;
            string ingredientsInfo = string.Join(Environment.NewLine, ingredients.Select(i => i.ToString()));
            string stepsInfo = string.Join(Environment.NewLine, steps);

            double totalCalories = ingredients.Sum(i => i.Calories);
            string recipeInfo = $"Recipe Name: {recipeName}" + Environment.NewLine +
                                $"Ingredients: {ingredientsInfo}" + Environment.NewLine +
                                $"Total Calories: {totalCalories}" + Environment.NewLine +
                                $"Steps: {stepsInfo}";

            MessageBox.Show(recipeInfo, "Recipe Information");
        }

        private void EnterRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = txtRecipeName.Text;

            Recipe recipe = new Recipe(recipeName, new List<Ingredient>(ingredients), new List<string>(steps));
            recipes.Add(recipe);

            ClearRecipeFields();
            MessageBox.Show("Recipe entered successfully.");

            // Update the recipes ListBox
            lstRecipes.ItemsSource = null;
            lstRecipes.ItemsSource = recipes;
            lstRecipes.DisplayMemberPath = "RecipeName";
        }

        private void ScalingButton_Click(object sender, RoutedEventArgs e)
        {
            string scalingFactorText = txtScalingFactor.Text;

            if (double.TryParse(scalingFactorText, out double scalingFactor))
            {
                if (scalingFactor > 0)
                {
                    foreach (Ingredient ingredient in ingredients)
                    {
                        ingredient.Quantity *= scalingFactor;
                    }

                    lstIngredients.ItemsSource = null;
                    lstIngredients.ItemsSource = ingredients;
                    lstIngredients.DisplayMemberPath = "Name";

                    MessageBox.Show("Ingredients scaled successfully.", "Scaling");
                }
                else
                {
                    MessageBox.Show("Scaling factor must be greater than 0.", "Invalid Scaling Factor");
                }
            }
            else
            {
                MessageBox.Show("Invalid input for scaling factor. Please enter a valid number.", "Invalid Scaling Factor");
            }
        }

        private void ResetQuantitiesButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                ingredient.Quantity = 0;
                ingredient.Calories = 0;
            }

            ClearIngredientFields();
            MessageBox.Show("Quantities reset successfully.", "Reset Quantities");
        }

        private void ClearIngredientFields()
        {
            txtName.Text = "";
            txtCalories.Text = "";
            txtUnit.Text = "";
            txtQuantity.Text = "";
            txtFoodGroup.Text = "";
        }

        private void ClearStepField()
        {
            txtStep.Text = "";
        }

        private void ClearRecipeFields()
        {
            txtRecipeName.Text = "";
            ingredients.Clear();
            steps.Clear();
            lstIngredients.ItemsSource = null;
            lstSteps.ItemsSource = null;
        }

        private void ClearRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            ClearRecipeFields();
            MessageBox.Show("Recipe cleared successfully.", "Clear Recipe");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void FilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            string filter = txtFilter.Text.ToLower();

            if (double.TryParse(filter, out double maxCalories))
            {
                var filteredRecipes = recipes.Where(r => r.Ingredients.Sum(i => i.Calories) <= maxCalories).ToList();
                DisplayFilteredRecipes(filteredRecipes);
            }
            else
            {
                var filteredRecipes = recipes.Where(r => r.Ingredients.Any(i => i.Name.ToLower().Contains(filter) ||
                                                                                i.FoodGroup.ToLower().Contains(filter))).ToList();
                DisplayFilteredRecipes(filteredRecipes);
            }
        }

        private void DisplayFilteredRecipes(List<Recipe> filteredRecipes)
        {
            if (filteredRecipes.Count > 0)
            {
                string filteredRecipesInfo = string.Join(Environment.NewLine, filteredRecipes.Select(r =>
                {
                    string ingredientsInfo = string.Join(", ", r.Ingredients.Select(i => i.ToString()));
                    return $"Recipe Name: {r.RecipeName}\nIngredients: {ingredientsInfo}\nSteps: {string.Join(", ", r.Steps)}";
                }));

                MessageBox.Show(filteredRecipesInfo, "Filtered Recipes");
            }
            else
            {
                MessageBox.Show("No recipes match the filter criteria.", "Filtered Recipes");
            }
        }
    }
}
