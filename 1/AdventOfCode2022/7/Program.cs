using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

Part1();
Console.WriteLine();
Part2();


static void Part1()
{
    Console.WriteLine("Part 1");
    var readLines = File.ReadLines("input.txt");
    var allDirs = new List<MyDirectory>();

    MyDirectory currentDir = new MyDirectory("/", null, new(), new());
    var rootDirectory = currentDir;

    foreach (var line in readLines)
    {
        if (line.StartsWith("dir "))
        {
            var newDir = MyDirectory.Create(line, currentDir);
            allDirs.Add(newDir);
            if (currentDir is object)
            {
                currentDir.Dirs.Add(newDir);
            }
        }
        else if (char.IsNumber(line[0]))
        {
            var newFile = MyFile.Create(line);
            currentDir!.Files.Add(newFile);
        }
        else if (line.StartsWith("$ cd"))
        {
            var cdCommand = line.Substring(5);
            if (cdCommand == "..")
            {
                currentDir = currentDir!.ParentDirectory!;
            }
            else if (cdCommand != "/")
            {
                currentDir = currentDir!.Dirs.Single(x => string.Equals(x.Name, cdCommand, StringComparison.OrdinalIgnoreCase));
            }
        }
    }

    var allDirsOfSize = allDirs.Where(x => x.CalculateSize() < 100_000).ToList();
    var totalSize = allDirsOfSize.Sum(x => x.CalculateSize());
    Console.WriteLine(totalSize);
}

static void Part2()
{
    Console.WriteLine("Part 2");

    var readLines = File.ReadLines("input.txt");
    var allDirs = new List<MyDirectory>();

    MyDirectory currentDir = new MyDirectory("/", null, new(), new());
    var rootDirectory = currentDir;

    foreach (var line in readLines)
    {
        if (line.StartsWith("dir "))
        {
            var newDir = MyDirectory.Create(line, currentDir);
            allDirs.Add(newDir);
            if (currentDir is object)
            {
                currentDir.Dirs.Add(newDir);
            }
        }
        else if (char.IsNumber(line[0]))
        {
            var newFile = MyFile.Create(line);
            currentDir!.Files.Add(newFile);
        }
        else if (line.StartsWith("$ cd"))
        {
            var cdCommand = line.Substring(5);
            if (cdCommand == "..")
            {
                currentDir = currentDir!.ParentDirectory!;
            }
            else if (cdCommand != "/")
            {
                currentDir = currentDir!.Dirs.Single(x => string.Equals(x.Name, cdCommand, StringComparison.OrdinalIgnoreCase));
            }
        }
    }

    var totalAvailable = 70_000_000;
    var requiredSpace = 30_000_000;
    var totalSizeUsed = rootDirectory.CalculateSize();
    var freeSpace = totalAvailable - totalSizeUsed;
    var sizeNeedToDelete = requiredSpace - freeSpace;
    var deletables = new List<MyDirectory>();

    foreach (var dir in rootDirectory.AllDirsPlusChildren())
    {
        var size = dir.CalculateSize();
        if (size >= sizeNeedToDelete)
        {
            deletables.Add(dir);
        }
    }
    
    var toDelete = deletables.OrderBy(x => x.CalculateSize()).First();
    Console.WriteLine(toDelete.CalculateSize());
}


public record MyDirectory(string Name, MyDirectory? ParentDirectory, List<MyDirectory> Dirs, List<MyFile> Files)
{
    public static MyDirectory Create(string input, MyDirectory? parentDirectory)
    {
        var name = input.Substring(4);
        return new MyDirectory(name, parentDirectory, new(), new());
    }

    public int CalculateSize()
        => Files.Sum(x => x.Size) + Dirs.Sum(x => x.CalculateSize());

    public string FullPath
    {
        get
        {
            var path = "";
            if (ParentDirectory is object)
            {
                path += ParentDirectory.FullPath + "/";
            }

            path += Name;
            return path;
        }
    }

    public IEnumerable<MyDirectory> AllDirsPlusChildren()
    {
        foreach (var dir in Dirs)
        {
            yield return dir;
            foreach(var child in dir.AllDirsPlusChildren())
            {
                yield return child;
            }
        }
    }
}

public record MyFile(string Name, int Size)
{
    public static MyFile Create(string input)
    {
        var fileInfos = input.Split(' ');
        var size = int.Parse(fileInfos[0]);
        var name = fileInfos[1];
        return new MyFile(name, size);
    }
}
