using ConsoleApp6;
using System.Collections.Generic;
using System;

class Program
{
    // list to store the recipes
    static List<Recipe> recipes = new List<Recipe>();

    // main method ---------------------------------------------------------------------------------------------------------------------
    static void Main(string[] args)
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();

        bool exit = false;

        // Main menu loop
        while (!exit)
        {
            Console.WriteLine("\nWelcome to Save a Recipe");
            Console.WriteLine("1. Add new recipe");
            Console.WriteLine("2. Display all recipes");
            Console.WriteLine("3. Clear all recipes");
            Console.WriteLine("4. Search for a recipe");
            Console.WriteLine("5. Scale ingredients by a factor");
            Console.WriteLine("6. Reset recipe scale factor");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 7");
            }

            switch (choice)
            {
                case 1:
                    AddRecipe();
                    break;
                case 2:
                    DisplayAllRecipes();
                    break;
                case 3:
                    ClearRecipes();
                    break;
                case 4:
                    Console.Write("Enter the name of the recipe to search: ");
                    string searchName = Console.ReadLine();
                    SearchRecipe(searchName);
                    break;
                case 5:
                    Console.Write("Enter the name of the recipe to scale ingredients: ");
                    string scaleName = Console.ReadLine();
                    Console.Write("Enter the scale type ('half', 'double', 'triple', or custom factor): ");
                    string scaleType = Console.ReadLine();
                    ScaleRecipeIngredients(scaleName, scaleType);
                    break;
                case 6:
                    Console.Write("Enter the name of the recipe to reset the scale factor: ");
                    string resetName = Console.ReadLine();
                    ResetRecipeScaleFactor(resetName);
                    break;
                case 7:
                    exit = true;
                    Console.WriteLine("\nGoodbye!");
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Please choose again.");
                    break;
            }
        }
    }

    // this is while loop that iterates through the menu until a user selects option 6
    // if a user enters a invalid answer it will display a error message
    // each option selected corresponds/ executes the method connected to that option
    //-----------------------------------------------------------------------------------------------------------------------------
    // add a recipe method -----------------------------------------------------------------------------------------------------------------
    static void AddRecipe()
    {
        Console.WriteLine("\nEnter the details for the new recipe:");
        Console.Write("Enter the name of your recipe: ");
        string recipeName = Console.ReadLine();
        List<Ingredient> ingredients = GetIngredients();
        List<string> steps = GetSteps();

        foreach (var ingredient in ingredients)
        {
            ingredient.OriginalQuantity = ingredient.ScaledQuantity;
        }

        Recipe recipe = new Recipe(recipeName, ingredients, steps);
        recipes.Add(recipe);
        Console.WriteLine("\nRecipe added successfully!");
    }
    // prompts a user to enter details to store for a recipe
    // after entering the required information the recipe will be stored

    //display all the stored recipes method--------------------------------------------------------------------------------------------
    static void DisplayAllRecipes()
    {
        if (recipes.Count == 0)
        {
            Console.WriteLine("\nNo recipes available.");
        }
        else
        {
            Console.WriteLine("\nAvailable Recipes:");
            int count = 1;
            foreach (var recipe in recipes)
            {
                Console.WriteLine($"\nRecipe {count}:");
                recipe.DisplayRecipe();
                count++;
            }
        }
    }
    // this method is responsible for displaying all saved recipes
    // it uses a foreach loop that iterates through the stored recipes whilst incrementing them

    // method to clear all the recipes -----------------------------------------------------------------------------------------------
    static void ClearRecipes()
    {
        recipes.Clear();
        Console.WriteLine("\nAll recipes cleared.");
    }
    // this method clears all recipes stored in the list
    // method to search for a recipe ------------------------------------------------------------------------------------------------
    static void SearchRecipe(string name)
    {
        Recipe foundRecipe = recipes.Find(recipe => recipe.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (foundRecipe != null)
        {
            Console.WriteLine($"\nRecipe '{name}' Found:");
            foundRecipe.DisplayRecipe();
        }
        else
        {
            Console.WriteLine($"\nRecipe '{name}' not found.");
        }
    }
    // this method is used to search for specific recipes
    // it finds the specific recipe by searching for its name inputed by the user that is then matched against any stored recipes with the same name
    // if a recipe with the same is found the DisplayRecipe will be called to display the searched recipe

    //method that scales the ingredients by half, double, triple or a custom amount -------------------------------------------------------
    static void ScaleRecipeIngredients(string name, string scaleType)
    {
        double factor = GetScaleFactor(scaleType);
        if (factor == -1)
        {
            Console.WriteLine("\nInvalid scale type. Available options are 'half', 'double', 'triple', or a custom scale factor.");
            return;
        }

        Recipe foundRecipe = recipes.Find(recipe => recipe.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (foundRecipe != null)
        {
            foundRecipe.ScaleIngredients(factor);
            Console.WriteLine($"\nIngredients for recipe '{name}' scaled by a factor of {factor}.");
        }
        else
        {
            Console.WriteLine($"\nRecipe '{name}' not found.");
        }
    }
    // this method is responsible for scaling the ingredients by half, double, triple or a custom amount
    // when the desired recipe is found and matches with a recipe stored in the list it will then scale the ingredients according to the selected option from the user
    // if no recipe is found a error message will be displayed

    // method to get the ingredients for the recipe --------------------------------------------------------------------------------------
    static List<Ingredient> GetIngredients()
    {
        List<Ingredient> ingredients = new List<Ingredient>();
        Console.Write("Enter the number of ingredients: ");
        int numIngredients = int.Parse(Console.ReadLine());
        for (int i = 0; i < numIngredients; i++)
        {
            Console.Write($"Enter the name of ingredient {i + 1}: ");
            string name = Console.ReadLine();
            Console.Write($"Enter the quantity of {name}: ");
            string quantity = Console.ReadLine();
            Console.Write($"Enter the unit of measurement for {name}: ");
            string unit = Console.ReadLine();
            ingredients.Add(new Ingredient { Name = name, ScaledQuantity = quantity, Unit = unit });
        }
        return ingredients;
    }
    // this method is for the user to input details for each ingredient of a recipe
    //prompts the user to enter the number of ingredients
    //prompts the user to enter the quantity of the ingredient
    // prompts the user to enter the unit of measurement for the ingredient
    // the method will iterate through these steps for each ingredient defined

    // method to get steps for a recipe ---------------------------------------------------------------------------------------------
    static List<string> GetSteps()
    {
        List<string> steps = new List<string>();
        Console.Write("Enter the number of steps required to cook this meal: ");
        int numSteps = int.Parse(Console.ReadLine());
        for (int i = 0; i < numSteps; i++)
        {
            Console.Write($"Enter step {i + 1}: ");
            steps.Add(Console.ReadLine());
        }
        return steps;
    }
    // this method is used to prompt the user to input the steps required to complete the recipe
    // the number of steps corollates to the amount of steps entered by the user

    //method to get the scale factor for the scaling for the ingredients --------------------------------------------------------------
    static double GetScaleFactor(string scaleType)
    {
        switch (scaleType.ToLower())
        {
            case "half":
                return 0.5;
            case "double":
                return 2.0;
            case "triple":
                return 3.0;
            default:
                if (double.TryParse(scaleType, out double customFactor) && customFactor > 0)
                {
                    return customFactor;
                }
                else
                {
                    return -1; // Indicates an invalid scale factor
                }
        }
    }
    //this method returns the scaling factor
    // it uses a switch statement to check the scale factor selected

    static void ResetRecipeScaleFactor(string name)
    {
        Recipe foundRecipe = recipes.Find(recipe=> recipe.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (foundRecipe != null)
        {
            foundRecipe.ResetScaleFactor();
            Console.WriteLine($"\nScale factor reset for recipe '{name}'.");
        }
        else
        {
            Console.WriteLine($"\nRecipe '{name}' not found.");
        }
    }
}
//---------------------------------------------------------END OF FILE------------------------------------------------------------------
