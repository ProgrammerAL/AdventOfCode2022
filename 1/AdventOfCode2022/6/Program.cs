using System.Collections.Immutable;
using System.Linq;

Part1();
Console.WriteLine();
Part2();


static void Part1()
{
    var input = File.ReadLines("input.txt").Single().AsSpan();

    for (int i = 0; i < input.Length - 3; i++)
    {
        var previous = input.Slice(0, i).ToArray();
        var current = input.Slice(i, 4).ToArray();

        //Check current character in span
        var onlyOneEach = current.GroupBy(x => x).All(x => x.Count() == 1);
        if (!onlyOneEach)
        {
            continue;
        }

        Console.WriteLine(new string(current));
        Console.WriteLine(i + 4);
        break;
    }
}

static void Part2()
{
    var input = File.ReadLines("input.txt").Single().AsSpan();

    for (int i = 0; i < input.Length - 3; i++)
    {
        var previous = input.Slice(0, i).ToArray();
        var current = input.Slice(i, 14).ToArray();

        //Check current character in span
        var onlyOneEach = current.GroupBy(x => x).All(x => x.Count() == 1);
        if (!onlyOneEach)
        {
            continue;
        }

        Console.WriteLine(new string(current));
        Console.WriteLine(i + 14);
        break;
    }
}

