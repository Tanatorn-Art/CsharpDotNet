using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public enum Color { Red, Green, Ivory, Yellow, Blue }
public enum Nationality { Englishman, Spaniard, Ukrainian, Japanese, Norwegian }
public enum Pet { Dog, Snails, Fox, Horse, Zebra }
public enum Drink { Coffee, Tea, Milk, OrangeJuice, Water }
public enum Hobby { Chess, Football, Dancing, Painting, Reading }

public static class ZebraPuzzle
{
    public static List<(List<Color>, List<Nationality>, List<Pet>, List<Drink>, List<Hobby>)> MakePermutations(CancellationToken cancellationToken)
    {
        // Applying immediate constraints: Norwegian lives in the first house(10), Norwegian lives next to the blue house(15) and the milk drinker lives in the middle(9)
        // The green house is immediately to the right of the ivory house - so they must operate in pairs, with Color.Green following Color.Ivory
        var colors = GetPermutations(Enum.GetValues(typeof(Color)).Cast<Color>().ToList())
            .Where(p => p[1] == Color.Blue && p.Zip(p.Skip(1), (a, b) => (a, b)).Any(pair => pair.a == Color.Ivory && pair.b == Color.Green)).ToList();
        var nationalities = GetPermutations(Enum.GetValues(typeof(Nationality)).Cast<Nationality>().ToList())
            .Where(p => p[0] == Nationality.Norwegian).ToList();
        var pets = GetPermutations(Enum.GetValues(typeof(Pet)).Cast<Pet>().ToList());
        var drinks = GetPermutations(Enum.GetValues(typeof(Drink)).Cast<Drink>().ToList())
            .Where(p => p[2] == Drink.Milk).ToList();
        var hobbies = GetPermutations(Enum.GetValues(typeof(Hobby)).Cast<Hobby>().ToList());

        var solution = new List<(List<Color>, List<Nationality>, List<Pet>, List<Drink>, List<Hobby>)>();


        Parallel.ForEach(colors, (colorPermutation, state) =>
        {
            if (cancellationToken.IsCancellationRequested)
            {
                state.Stop();
                return;
            }
            foreach (var nationalityPermutation in nationalities)
            {
                foreach (var petPermutation in pets)
                {
                    foreach (var drinkPermutation in drinks)
                    {
                        foreach (var hobbyPermutation in hobbies)
                        {
                            var candidate = (colorPermutation, nationalityPermutation, petPermutation, drinkPermutation, hobbyPermutation);
                            //Checking if candidate pass the complex constraints.
                            if (ComplexConstraints(candidate))
                            {
                                lock (solution)
                                {
                                    solution.Add((colorPermutation, nationalityPermutation, petPermutation, drinkPermutation, hobbyPermutation));
                                }
                                state.Stop();
                                return;
                            }
                        }
                    }
                }
            }
        });
        return solution;
    }

    private static bool ComplexConstraints((List<Color> colors, List<Nationality> nationalities, List<Pet> pets, List<Drink> drinks, List<Hobby> hobbies) candidate)
    {
        var colors = candidate.colors;
        var nationalities = candidate.nationalities;
        var pets = candidate.pets;
        var drinks = candidate.drinks;
        var hobbies = candidate.hobbies;

        return colors[nationalities.IndexOf(Nationality.Englishman)] == Color.Red &&
               nationalities[pets.IndexOf(Pet.Dog)] == Nationality.Spaniard &&
               colors[drinks.IndexOf(Drink.Coffee)] == Color.Green &&
               nationalities[drinks.IndexOf(Drink.Tea)] == Nationality.Ukrainian &&
               pets[hobbies.IndexOf(Hobby.Dancing)] == Pet.Snails &&
               colors[hobbies.IndexOf(Hobby.Painting)] == Color.Yellow &&
               Math.Abs(hobbies.IndexOf(Hobby.Reading) - pets.IndexOf(Pet.Fox)) == 1 &&
               Math.Abs(hobbies.IndexOf(Hobby.Painting) - pets.IndexOf(Pet.Horse)) == 1 &&
               drinks[hobbies.IndexOf(Hobby.Football)] == Drink.OrangeJuice &&
               nationalities[hobbies.IndexOf(Hobby.Chess)] == Nationality.Japanese;
    }


    private static List<List<T>> GetPermutations<T>(List<T> list)
    {
        var result = new List<List<T>>();
        Permute(list, 0, list.Count - 1, result);
        return result;
    }


    private static void Permute<T>(List<T> list, int start, int end, List<List<T>> result)
    {
        if (start == end)
        {
            result.Add(new List<T>(list));
        }
        else
        {
            for (int i = start; i <= end; i++)
            {
                Swap(list, start, i);
                Permute(list, start + 1, end, result);
                Swap(list, start, i); // backtrack
            }
        }
    }

    private static void Swap<T>(List<T> list, int i, int j)
    {
        T temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

    public static Nationality DrinksWater()
    {
        using (var cts = new CancellationTokenSource())
        {
            var result = MakePermutations(cts.Token);
            foreach (var alternatives in result)
            {
                var nationalities = alternatives.Item2;
                var drinks = alternatives.Item4;
                int waterIndex = drinks.IndexOf(Drink.Water);
                if (waterIndex != -1)
                {
                    return nationalities[waterIndex];
                }
            }
        }
        throw new Exception("No solution found!");
    }

    public static Nationality OwnsZebra()
    {
        using (var cts = new CancellationTokenSource())
        {
            var result = MakePermutations(cts.Token);
            foreach (var alternatives in result)
            {
                var nationalities = alternatives.Item2;
                var pets = alternatives.Item3;
                int zebraIndex = pets.IndexOf(Pet.Zebra);
                if (zebraIndex != -1)
                {
                    return nationalities[zebraIndex];
                }
            }
        }
        throw new Exception("No solution found!");
    }
}