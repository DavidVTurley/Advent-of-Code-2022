using System.IO.Pipes;
using System.Reflection.Metadata.Ecma335;

namespace Advent_of_Code.Days;

public class Day2 : IDay
{
    //https://adventofcode.com/2022/day/2#part2

    public async Task Setup(HttpClient client)
    {
        //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,"2/input");
        //request.Headers.TryAddWithoutValidation("Cookie", "session=53616c7465645f5fbb1fcdcf1223961c690186431282a894ba304365b59d3f0fc2b61f7c44d8eed766a1c1aeb224f7d744c47db8b52bfdf002e1fd0637a212f5");

        //HttpResponseMessage response = await client.SendAsync(request);
        //response.EnsureSuccessStatusCode();
        //String plainInput = await response.Content.ReadAsStringAsync();
        
        String input = await ExtraFunctions.MakeAdventOfCodeInputRequest(client, 2);

        List<String> individualTurns = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();
        individualTurns.ForEach(turn =>
        {
            _turns.Add((
                (Signs)Char.ToUpper(turn[0]) - 64, 
                (Signs)Char.ToUpper(turn[2]) - 87));
        });
    }


    private readonly List<(Signs opponent, Signs player)> _turns = new();

    public void Challenge1()
    {
        Int32 score = 0;

        foreach ((Signs opponent, Signs player) turn in _turns)
        {
            score += (Int32)turn.player;
            if (turn.player == turn.opponent)
            {
                score += 3;
                continue;
            }

            switch (turn.player)
            {
                case Signs.Rock:
                {
                    if (turn.opponent == Signs.Scissor) score += 6;
                    break;
                }
                case Signs.Paper:
                {
                    if (turn.opponent == Signs.Rock) score += 6;
                    break;
                }
                case Signs.Scissor:
                {
                    if (turn.opponent == Signs.Paper) score += 6;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        Console.WriteLine($"I scored {score} points!");
    }

    public void Challenge2()
    {
        Int32 score = 0;

        foreach ((Signs opponent, Signs player) turn in _turns)
        {
            // 1 means lose, 2 means draw, 3 means win
            switch (turn.player)
            {
                // Lose
                case Signs.Rock:
                    score += (Int32) turn.opponent.Previous();
                    break;
                // Draw
                case Signs.Paper:
                    score += 3 + (Int32)turn.opponent;
                    break;
                // Win
                case Signs.Scissor:
                    score += 6 + (Int32) turn.opponent.Next();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        Console.WriteLine($"The total score with the corrected rule set is: {score}");
    }

    private enum Signs
    {
        Rock = 1,
        Paper,
        Scissor,
    }
}
