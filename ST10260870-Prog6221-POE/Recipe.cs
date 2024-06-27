using System.Collections.Generic;

public class Recipe
{
    public string RecipeName { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<string> Steps { get; set; }

    public Recipe(string recipeName, List<Ingredient> ingredients, List<string> steps)
    {
        RecipeName = recipeName;
        Ingredients = ingredients;
        Steps = steps;
    }

    public override string ToString()
    {
        string ingredientsInfo = string.Join(", ", Ingredients);
        string stepsInfo = string.Join(", ", Steps);
        return $"Recipe Name: {RecipeName}\nIngredients: {ingredientsInfo}\nSteps: {stepsInfo}";
    }
}
