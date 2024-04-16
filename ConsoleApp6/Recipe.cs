using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    internal class Recipe

    {
        // Getters and Setters-------------------------------------------------------------------------------------------
        public string Name { get; set; } // name of the recipe
        public List<Ingredient> Ingredients { get; set; }// list of the ingredients required for the recipe
        public List<string> Steps { get; set; } // list of steps to prepare the recipe
        public double ScaleFactor { get; set; }
        //----------------------------------------------------------------------------------------------------------------

        // default constructor -------------------------------------------------------------------------------------------
        public Recipe(string name, List<Ingredient> ingredients, List<string> steps)
        {
            Name = name;
            Ingredients = ingredients;
            Steps = steps;
        }
        //-----------------------------------------------------------------------------------------------------------------

        // method to display the recipe details ---------------------------------------------------------------------------
        public void DisplayRecipe()
        {
            //display the recipe name
            Console.WriteLine($"Recipe: {Name}");
            //displays the list of ingredients
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in Ingredients)
            {
                // displays each ingredient with quantity, unit of measurement and name
                Console.WriteLine($"- {ingredient.ScaledQuantity} {ingredient.Unit} {ingredient.Name}");
            }
            // displays the steps to prepare the recipe
            Console.WriteLine("\nSteps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                // displays each step with its correspomding index
                Console.WriteLine($"{i + 1}. {Steps[i]}");
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------

        // method to scale the ingredients by a given factor -----------------------------------------------------------------------
        public void ScaleIngredients(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                // parses the quanity of the ingredient to a double
                double quantity = double.Parse(ingredient.ScaledQuantity);
                //scales the ingredients by the chosen factors
                quantity *= factor;
                // updates the quantity of the ingredient
                ingredient.ScaledQuantity = quantity.ToString();
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------
        public void ResetScaleFactor()
        {
            foreach(var ingredient in Ingredients)// iterates through each ingredient in the recipe
            {
                // resets the scaled quantity of the ingredient to its original quantity
                ingredient.ScaledQuantity = ingredient.OriginalQuantity;
            }
           
        }
    }
}

//-----------------------------------------------------------END OF FILE---------------------------------------------------------------
