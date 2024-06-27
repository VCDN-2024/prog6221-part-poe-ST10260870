public class Ingredient
{
    public string Name { get; set; }
    public double Calories { get; set; }
    public string Unit { get; set; }
    public double Quantity { get; set; }
    public string FoodGroup { get; set; } // Changed to a single string

    public Ingredient(string name, double calories, string unit, double quantity, string foodGroup)
    {
        Name = name;
        Calories = calories;
        Unit = unit;
        Quantity = quantity;
        FoodGroup = foodGroup;
    }

    public override string ToString()
    {
        return $"{Name}, {Calories} calories, {Quantity} {Unit}, {FoodGroup}";
    }
}
