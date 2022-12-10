using System.Text;

namespace Advent_of_Code.Days;

public class Day6 : IDay
{
    public async Task Setup(HttpClient client)
    {
        String input = await ExtraFunctions.MakeAdventOfCodeInputRequest(client, 6);
        //dataStreams = new List<String>
        //{
        //    "bvwbjplbgvbhsrlpgdmjqwftvncz",
        //    "nppdvjthqldpwncqszvftbrmjlhg",
        //    "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",
        //    "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw",
        //};

        DataStreams.AddRange(input.Split('\n', StringSplitOptions.RemoveEmptyEntries));
    }

    public List<String> DataStreams { get; } = new();

    public void Challenge1()
    {
        Int32 count = 0;
        foreach (String dataStream in DataStreams)
        {
            Char letterToCheck;
            Boolean recheckAfterJump = true;
            for (Int32 i = 3; i < dataStream.Length; i++)
            {
                if (recheckAfterJump)
                {
                    // charsToCheck
                    Int32 charsToCheck = 4
                    for (int j = 0; j < i+ charsToCheck; j++)
                    {
                        
                    }

                    Int32 index0 = i - 3;
                    Int32 index1 = i - 2;
                    Int32 index2 = i - 1;
                    if (dataStream[index1] == dataStream[index0] || dataStream[index2] == dataStream[index0])
                    {
                        continue;
                    }
                    if (dataStream[index2] == dataStream[index1])
                    {
                        i++;
                        continue;
                    }
                    else
                    {
                        recheckAfterJump = false;
                    }
                }

                letterToCheck = dataStream[i];
                String checkOut = "";
                checkOut += "" + dataStream[i - 3] + dataStream[i - 2] + dataStream[i - 1] + dataStream[i];
                
                if (dataStream[i-1] == letterToCheck)
                {
                    i += 2;
                    recheckAfterJump = true;
                }
                else if (dataStream[i-2] == letterToCheck)
                {
                    i += 1;
                    recheckAfterJump = true;
                }
                else if (dataStream[i-3] == letterToCheck)
                {}
                else
                {
                    count += i+1;
                    break;
                }
            }
        }
    }

    public void Challenge2()
    {
        throw new NotImplementedException();
    }
}