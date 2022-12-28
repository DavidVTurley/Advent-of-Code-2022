namespace Advent_of_Code.Days;

public interface IDay
{
    public Object Setup(HttpClient client);
    public void Challenge1(Object input);
    public void Challenge2(Object input);
}