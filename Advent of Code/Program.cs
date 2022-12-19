// See https://aka.ms/new-console-template for more information


using Advent_of_Code.Days;
using System.Net.Http;

HttpClient client = new HttpClient()
{
    BaseAddress = new Uri("https://adventofcode.com/2022/day/"),
};




IDay day = new Day8();
await day.Setup(client);

day.Challenge1();
day.Challenge2();