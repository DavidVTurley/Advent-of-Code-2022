namespace Advent_of_Code.Days;

public class Day1 : IDay
{
    public Object Setup(HttpClient client)
    {
        //using HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), "1/input");
        //request.Headers.TryAddWithoutValidation("Cookie", "session=53616c7465645f5fbb1fcdcf1223961c690186431282a894ba304365b59d3f0fc2b61f7c44d8eed766a1c1aeb224f7d744c47db8b52bfdf002e1fd0637a212f5");

        //HttpResponseMessage response = await client.SendAsync(request);
        //response.EnsureSuccessStatusCode();
        //String plainInput = await response.Content.ReadAsStringAsync();

        String plainInput = ExtraFunctions.MakeAdventOfCodeInputRequest(client, 1);

        String[] splitFoods = plainInput.Split("\n\n");
        foreach (String food in splitFoods)
        {
            List<Int32> listOfCalories = new List<Int32>();
            foreach (String calorieString in food.Split("\n"))
            {
                Int32.TryParse(calorieString, out Int32 calorie);
                listOfCalories.Add(calorie);
            }
            _elves.Add(new Elf(listOfCalories));
        }

        _elves = _elves.OrderBy(elf => elf.TotalCalories).ToList();
        return _elves;
    }

    private List<Elf> _elves = new List<Elf>();

    public void Challenge1(Object input)
    {
        Console.WriteLine($"Biggest elf has {_elves.Last().TotalCalories} calories and is the most.");
    }

    public void Challenge2(Object input)
    {
        Int32 totalCaloriesSummed = _elves.TakeLast(3).Sum(elf => elf.TotalCalories);
        Console.WriteLine($"The top 3 elves by calories are carying {totalCaloriesSummed} calories.");
    }

    internal class Elf
    {
        public List<Int32> Calories;
        public Int32 TotalCalories = 0;

        public Elf(List<Int32> calories)
        {
            Calories = calories;
            calories.ForEach(calorie => { TotalCalories += calorie;});
        }
    }
}