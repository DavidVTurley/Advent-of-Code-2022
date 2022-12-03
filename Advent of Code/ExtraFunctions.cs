namespace Advent_of_Code;

public static class ExtraFunctions
{
    public static async Task<string> MakeAdventOfCodeInputRequest(HttpClient client, Int32 day)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{day}/input");
        request.Headers.TryAddWithoutValidation("Cookie", "session=53616c7465645f5fbb1fcdcf1223961c690186431282a894ba304365b59d3f0fc2b61f7c44d8eed766a1c1aeb224f7d744c47db8b52bfdf002e1fd0637a212f5");

        HttpResponseMessage response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }



    // Extension methods
    public static T Next<T>(this T src) where T : Enum
    {
        T[] arr = (T[])Enum.GetValues(src.GetType());
        Int32 j = Array.IndexOf<T>(arr, src) + 1;
        return (arr.Length == j) ? arr[0] : arr[j];
    }

    public static T Previous<T>(this T src) where T : Enum
    {
        T[] arr = (T[])Enum.GetValues(src.GetType());
        Int32 j = Array.IndexOf<T>(arr, src) - 1;
        return (-1 == j) ? arr[^1] : arr[j];
    }
}