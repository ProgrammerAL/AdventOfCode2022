using System.Collections.Immutable;

Part1();
Console.WriteLine();
Part2();

const int StacksCount = 9;

static void Part1()
{
    var readLines = File.ReadLines("input.txt");

    var stacks = Enumerable.Range(1, StacksCount).Select(x => new List<char>()).ToImmutableArray();

    var mapLines = readLines.Take(8);
    var instructionLines = readLines.Skip(10);

    foreach (var mapLine in mapLines)
    {
        var stackChunks = mapLine.Chunk(4).Select(x => new string(x).Trim().TrimStart('[').TrimEnd(']')).ToList();
        for (int i = 0; i < StacksCount; i++)
        {
            var stackChunk = stackChunks[i];
            if (!string.IsNullOrWhiteSpace(stackChunk))
            {
                var crateText = stackChunk.Single();
                stacks[i].Add(crateText);
            }
        }
    }

    foreach (var stack in stacks)
    {
        stack.Reverse();
    }

    foreach (var line in instructionLines)
    {
        var instruction = ParseInstructionLine(line);
        var fromStack = stacks[instruction.FromIndex];
        var toStack = stacks[instruction.ToIndex];

        for (int i = 0; i < instruction.Count; i++)
        {
            var crate = fromStack.Last();
            fromStack.RemoveAt(fromStack.Count - 1);
            toStack.Add(crate);
        }
    }

    foreach (var stack in stacks)
    {
        Console.Write(stack.Last());
    }
}

static void Part2()
{
    var readLines = File.ReadLines("input.txt");

    var stacks = Enumerable.Range(1, StacksCount).Select(x => new List<char>()).ToImmutableArray();

    var mapLines = readLines.Take(8);
    var instructionLines = readLines.Skip(10);

    foreach (var mapLine in mapLines)
    {
        var stackChunks = mapLine.Chunk(4).Select(x => new string(x).Trim().TrimStart('[').TrimEnd(']')).ToList();
        for (int i = 0; i < StacksCount; i++)
        {
            var stackChunk = stackChunks[i];
            if (!string.IsNullOrWhiteSpace(stackChunk))
            {
                var crateText = stackChunk.Single();
                stacks[i].Add(crateText);
            }
        }
    }

    foreach (var stack in stacks)
    {
        stack.Reverse();
    }

    foreach (var line in instructionLines)
    {
        var instruction = ParseInstructionLine(line);
        var fromStack = stacks[instruction.FromIndex];
        var toStack = stacks[instruction.ToIndex];

        var createsRange = fromStack.TakeLast(instruction.Count).ToList();
        toStack.AddRange(createsRange);

        for (int i = 0; i < instruction.Count; i++)
        {
            fromStack.RemoveAt(fromStack.Count - 1);
        }
    }

    foreach (var stack in stacks)
    {
        Console.Write(stack.Last());
    }
}


static Instruction ParseInstructionLine(string line)
{
    var count = ReadInstructionNumber(line, "move");
    var from = ReadInstructionNumber(line, "from");
    var to = ReadInstructionNumber(line, "to");

    return new Instruction(count, from, to);
}

static int ReadInstructionNumber(string line, string instructiontext)
{
    var numberStartIndex = line.IndexOf(instructiontext) + instructiontext.Length + 1;
    var endIndex = line.IndexOf(' ', numberStartIndex);
    if (endIndex == -1)
    {
        endIndex = line.Length;
    }

    var length = endIndex - numberStartIndex;

    var numberString = line.Substring(numberStartIndex, length).Trim();
    return int.Parse(numberString);
}


record Instruction(int Count, int From, int To)
{
    public int FromIndex => From - 1;
    public int ToIndex => To - 1;
}