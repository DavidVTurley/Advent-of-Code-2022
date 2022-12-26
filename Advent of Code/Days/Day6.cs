namespace Advent_of_Code.Days;

public class Day6 : IDay
{
    public async Task Setup(HttpClient client)
    {
        String input = await ExtraFunctions.MakeAdventOfCodeInputRequest(client, 6);
        //input = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";
        DataStreams.AddRange(input.Split('\n', StringSplitOptions.RemoveEmptyEntries));
    }

    public List<String> DataStreams { get; } = new();

    public Int32 CheckSignal(String signal, Int32 testingDepth)
    {
        Char letterToCheck;
        Boolean recheckAfterJump = true;
        for (Int32 i = testingDepth-1; i < signal.Length; i++)
        {
            Boolean foundDouble = false;
            if (recheckAfterJump)
            {
                recheckAfterJump = false;

                List<Int32> indexes = new List<Int32>(testingDepth-1);
                for (Int32 j = i -1; j > i-testingDepth; j--)
                {
                    indexes.Add(j);
                }

                String q = signal.Substring(i - testingDepth+1, testingDepth);
                for (Int32 x = 0; x < indexes.Count; x++)
                {
                    Int32 index1 = indexes[x];
                    
                    for (Int32 j = 1; j < indexes.Count; j++)
                    {
                        Int32 index2 = indexes[j];
                        if(index1 == index2) continue;
                        if (signal[index1] != signal[index2]) continue;

                        recheckAfterJump = true;
                        i += testingDepth - index2 -2;
                        break;
                    }
                    if(recheckAfterJump) break;
                }
                if(recheckAfterJump) continue;
            }

            String qq = signal.Substring(i - testingDepth + 1, testingDepth);
            letterToCheck = signal[i];
            for (Int32 j = i-1; j > i-testingDepth; j--)
            {
                var qqq = signal[j];
                if (signal[j] != letterToCheck) continue;

                Int32 newI = j + testingDepth - 1;
                if (newI - i >= 2) recheckAfterJump = true;

                i = newI;
                foundDouble = true;
                break;
            }

            if (!foundDouble)
            {
                return i+1;
            }
            
        }

        return -1;
    }

    public Int32 CheckNewBitSignal(String signal)
    {
        Int32 depthOfTest = 4;
        Int32 count = depthOfTest-1;
        // Too simple misses the 
        Boolean signalStartFound = false;

        while (signalStartFound != true)
        {
            Boolean reCheckClean = false;
            while (!reCheckClean)
            {
                Int32 check = ReCheckSignal(signal, depthOfTest);
                if (check == -1)
                {
                    reCheckClean = true;
                }
                else
                {
                    count += check;
                }
            }

            signalStartFound = true;
            for (Int32 compareValue = count - 1; compareValue > count - depthOfTest; compareValue--)
            {
                if (signal[count] != signal[compareValue]) continue;
                signalStartFound = false;
                count++;
                break;
            }
        }

        return count;
    }

    public Int32 ReCheckSignal(String signal, Int32 signalDepth)
    {
        for (int i = 0; i < signalDepth; i++)
        {
            
        }

        return -1;
    }

    public void Challenge1()
    {
        Int32 count = 0;
        Int32 returnIndex = CheckSignal(DataStreams[0], 4);
        if (returnIndex == -1) throw new Exception();

        count += returnIndex;

    }

    public void Challenge2()
    {
        throw new NotImplementedException();
    }
}