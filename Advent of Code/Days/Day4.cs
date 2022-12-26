namespace Advent_of_Code.Days;

public class Day4 : IDay
{
    private (Int32 min1, Int32 max1, Int32 min2, Int32 max2) numbers = new();
    public async Task Setup(HttpClient client)
    {
        String input = await ExtraFunctions.MakeAdventOfCodeInputRequest(client, 4);
        //input = $"2-4,6-8\n2-3,4-5\n5-7,7-9\n2-8,3-7\n6-6,4-6\n2-6,4-8";
        String[] stringNums = input.Split('\n');

        foreach (String stringNum in stringNums)
        {
            if(stringNum == String.Empty) continue;
            String[] nums = stringNum.Split('-', ',');

            _numbers.Add(new(Int32.Parse(nums[0]), Int32.Parse(nums[1]), Int32.Parse(nums[2]), Int32.Parse(nums[3])));
        }


    }

    private readonly List<(Int32 min1, Int32 max1, Int32 min2, Int32 max2)> _numbers = new();

    public void Challenge1()
    {
        Int32 count = 0;
        foreach ((Int32 min1, Int32 max1, Int32 min2, Int32 max2) in _numbers)
        {
            if (min1 <= min2 && max1 >= max2 || min2 <= min1 && max2 >= max1)
            {
                count++;
            }
        }
    }

    public void Challenge2()
    {
        Int32 count = 0;
        foreach ((Int32 min1, Int32 max1, Int32 min2, Int32 max2) in _numbers)
        {
            if (Enumerable.Range(min1, max1 - min1 + 1).Intersect(Enumerable.Range(min2, max2 - min2 + 1)).Any())
            {
                count++;
            }
        }
    }
}