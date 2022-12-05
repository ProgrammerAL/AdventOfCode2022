Part1();
Part2();

static void Part1()
{
    var readLines = File.ReadLines("input.txt");

    int priorityTotal = 0;

    foreach (var line in readLines)
    {
        var first = line.AsSpan().Slice(0, line.Length / 2);
        var second = line.AsSpan().Slice(line.Length / 2);
        var sameChar = FindSameChar(first, second);
        priorityTotal += FindPriority(sameChar);
    }

    Console.WriteLine(priorityTotal);
}

static void Part2()
{
    var readLines = File.ReadLines("input.txt");

    int priorityTotal = 0;

    foreach (var groupLines in readLines.Chunk(3))
    {
        var sameChar = FindSameCharInLines(groupLines[0], groupLines[1], groupLines[2]);
        priorityTotal += FindPriority(sameChar);
    }

    Console.WriteLine(priorityTotal);
}

static char FindSameChar(ReadOnlySpan<char> first, ReadOnlySpan<char> second)
{
    foreach (var character in first)
    {
        if (second.Contains(character))
        {
            return character;
        }
    }

    throw new Exception("No matching character");
}

static char FindSameCharInLines(string first, string second, string third)
{
    foreach (var character in first)
    {
        if (second.Contains(character)
            && third.Contains(character))
        {
            return character;
        }
    }

    throw new Exception("No matching character");
}

static int FindPriority(char letter)
{
    if (char.IsUpper(letter))
    {
        return letter - 'A' + 27;
    }
    else
    {
        return letter - 'a' + 1;
    }
}