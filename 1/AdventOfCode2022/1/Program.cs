
Part2();

void Part1()
{
    var all = new List<int>();

    var current = new List<int>();

    var readLines = File.ReadLines("input.txt");
    foreach (var line in readLines)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            var total = current.Sum();
            all.Add(total);
            current.Clear();
        }
        else
        {
            var lineNum = int.Parse(line);
            current.Add(lineNum);
        }
    }

    var lastTotal = current.Sum();
    all.Add(lastTotal);


    var highest = all.Max();
    Console.WriteLine(highest);
}

void Part2()
{
    var all = new List<KeyValuePair<int, int>>();

    var current = new List<int>();

    var readLines = File.ReadLines("input.txt");
    int elfCount = 1;
    foreach (var line in readLines)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            var total = current.Sum();
            all.Add(new KeyValuePair<int, int>(elfCount, total));
            elfCount++;
            current.Clear();
        }
        else
        {
            var lineNum = int.Parse(line);
            current.Add(lineNum);
        }
    }

    var lastTotal = current.Sum();
    all.Add(new KeyValuePair<int, int>(elfCount, lastTotal));

    var topThreeTotalCalories = all.OrderByDescending(x => x.Value).Take(3).Sum(x => x.Value);
    Console.WriteLine(topThreeTotalCalories);
}
