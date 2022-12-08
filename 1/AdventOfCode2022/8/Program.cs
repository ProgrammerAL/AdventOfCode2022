using System.Collections.Immutable;
using System.Linq;

Part1();
Console.WriteLine();
Part2();


static void Part1()
{
    Console.WriteLine("Part 1");

    var readLines = File.ReadLines("input.txt").ToImmutableArray();
    var rows = readLines.Length;
    var columns = readLines[0].Length;

    var trees = new Tree[rows][];
    for (int row = 0; row < rows; row++)
    {
        var line = readLines[row];
        trees[row] = new Tree[columns];

        for (int column = 0; column < columns; column++)
        {
            var columnHeight = int.Parse(line[column].ToString());
            var tree = new Tree(columnHeight);
            trees[row][column] = tree;

            //The edges are all visible
            if (row == 0 || row == rows - 1
                || column == 0 || column == columns - 1)
            {
                tree.IsVisible = true;
            }
        }
    }

    //Check left to right, ignoring edges
    foreach (var treeLine in trees.Skip(1))
    {
        var smallestTree = new Tree(Height: -100);
        foreach (var tree in treeLine)
        {
            if (tree.Height > smallestTree.Height)
            {
                tree.IsVisible = true;
                smallestTree = tree;
            }
        }
    }

    //Check right to left, ignoring edges
    foreach (var treeLine in trees.Skip(1))
    {
        var smallestTree = new Tree(Height: -100);
        var rightToLeftTrees = treeLine.Reverse();
        foreach (var tree in rightToLeftTrees)
        {
            if (tree.Height > smallestTree.Height)
            {
                tree.IsVisible = true;
                smallestTree = tree;
            }
        }
    }

    //Check top to bottom, ignoring edges
    for (int column = 1; column < columns; column++)
    {
        var smallestTree = new Tree(Height: -100);

        for (int row = 0; row < rows; row++)
        {
            var tree = trees[row][column];
            if (tree.Height > smallestTree.Height)
            {
                tree.IsVisible = true;
                smallestTree = tree;
            }
        }
    }

    //Check bottom to top, ignoring edges
    for (int column = columns - 1; column > 0; column--)
    {
        var smallestTree = new Tree(Height: -100);

        for (int row = rows - 1; row > 0; row--)
        {
            var tree = trees[row][column];
            if (tree.Height > smallestTree.Height)
            {
                tree.IsVisible = true;
                smallestTree = tree;
            }
        }
    }

    var visibleCount = 0;
    foreach (var row in trees)
    {
        foreach (var tree in row)
        {
            if (tree.IsVisible)
            {
                visibleCount++;
            }
        }
    }

    Console.WriteLine(visibleCount);
}

static void Part2()
{
    Console.WriteLine("Part 2");

    var readLines = File.ReadLines("input.txt").ToImmutableArray();
    var rows = readLines.Length;
    var columns = readLines[0].Length;

    var trees = new Tree[rows][];
    for (int row = 0; row < rows; row++)
    {
        var line = readLines[row];
        trees[row] = new Tree[columns];

        for (int column = 0; column < columns; column++)
        {
            var columnHeight = int.Parse(line[column].ToString());
            trees[row][column] = new Tree(columnHeight);
        }
    }

    for (int row = 0; row < rows; row++)
    {
        for (int column = 0; column < columns; column++)
        {
            var tree = trees[row][column];

            //Calculate Scenic Score
            int upScore = 0;
            int downScore = 0;
            int leftScore = 0;
            int rightScore = 0;
            //Check Left
            for (int toLeftColumn = column - 1; toLeftColumn >= 0; toLeftColumn--)
            {
                var leftTree = trees[row][toLeftColumn];
                leftScore++;
                if (leftTree.Height >= tree.Height)
                {
                    break;
                }
            }

            //Check Right
            for (int toRightColumn = column + 1; toRightColumn < columns; toRightColumn++)
            {
                var rightTree = trees[row][toRightColumn];
                rightScore++;
                if (rightTree.Height >= tree.Height)
                {
                    break;
                }
            }

            //Check Up
            for (int toUpRow = row - 1; toUpRow >= 0; toUpRow--)
            {
                var upTree = trees[toUpRow][column];
                upScore++;
                if (upTree.Height >= tree.Height)
                {
                    break;
                }
            }

            //Check Down
            for (int toDownRow = row + 1; toDownRow < rows; toDownRow++)
            {
                var downTree = trees[toDownRow][column];
                downScore++;
                if (downTree.Height >= tree.Height)
                {
                    break;
                }
            }

            tree.ScenicScore = upScore  * leftScore * downScore * rightScore;
        }
    }

    var best = new Tree(0);

    foreach(var rowTrees in trees)
    {
        foreach(var tree in rowTrees)
        {
            if(tree.ScenicScore > best.ScenicScore)
            {
                best = tree;
            }
        }
    }

    Console.WriteLine(best.ScenicScore);
}

public record Tree(int Height)
{
    public bool IsVisible { get; set; } = false;
    public int ScenicScore { get; set; }
}