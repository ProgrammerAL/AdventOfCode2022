using System;
using System.Collections.Immutable;

Part1();
Part2();

static void Part1()
{
    var readLines = File.ReadLines("input.txt");

    int count = 0;

    var loadedRanges = new List<ImmutableArray<int>>();

    foreach (var line in readLines)
    {
        var sections = line.Split(",");
        var range1 = DetermineRange(sections[0]);
        var range2 = DetermineRange(sections[1]);

        if(DoesContainFullAssignmentPair(range1, range2)
            || DoesContainFullAssignmentPair(range2, range1))
        {
            count++;
        }
    }

    Console.WriteLine(count);
}

static void Part2()
{
    var readLines = File.ReadLines("input.txt");

    int count = 0;

    var loadedRanges = new List<ImmutableArray<int>>();

    foreach (var line in readLines)
    {
        var sections = line.Split(",");
        var range1 = DetermineRange(sections[0]);
        var range2 = DetermineRange(sections[1]);

        if (DoesRangeOverlap(range1, range2))
        {
            count++;
        }
    }

    Console.WriteLine(count);
}

static bool RangesAreDifferent(ImmutableArray<int> x, ImmutableArray<int> y)
{
    return !x.SequenceEqual(y);
}

static ImmutableArray<int> DetermineRange(string text)
{
    var numbers = text.Split('-');
    var first = int.Parse(numbers[0]);
    var second = int.Parse(numbers[1]);
    var length = second - first + 1;
    return Enumerable.Range(first, length).ToImmutableArray();
}

static bool DoesContainFullAssignmentPair(ImmutableArray<int> outerRange, ImmutableArray<int> innerRange)
{
    var result = outerRange.First() <= innerRange.First()
        && outerRange.Last() >= innerRange.Last();

    return result;
}

static bool DoesRangeOverlap(ImmutableArray<int> outerRange, ImmutableArray<int> innerRange)
{
    return outerRange.Any(x => innerRange.Contains(x));
}
