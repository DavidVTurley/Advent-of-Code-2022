namespace Advent_of_Code.Days;

public interface IDay
{
    public Task Setup(HttpClient client);
    public void Challenge1();
    public void Challenge2();
}