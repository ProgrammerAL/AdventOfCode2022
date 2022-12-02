//Part1();
Part2();

const char OpponentRock = 'A';
const char OpponentPaper = 'B';
const char OpponentScissors = 'C';

const char MyRock = 'X';
const char MyPaper = 'Y';
const char MyScissors = 'Z';

const char NeedToLose = 'X';
const char NeedToDraw = 'Y';
const char NeedToWin = 'Z';

const int WinPoints = 6;
const int DrawPoints = 3;
const int LossPoints = 0;

static void Part1()
{
    var ResultPoints = new Dictionary<GameResultType, int>()
    {
        {GameResultType.Win, WinPoints},
        {GameResultType.Draw, DrawPoints},
        {GameResultType.Loss, LossPoints},
    };

    var PlayPoints = new Dictionary<char, int>()
    {
        {MyRock, 1},
        {MyPaper, 2},
        {MyScissors, 3},
    };

    var readLines = File.ReadLines("input.txt");

    int points = 0;

    foreach (var line in readLines)
    {
        var opponent = line[0];
        var mine = line[2];

        var result = DetermineGameResult(opponent, mine);
        points += ResultPoints[result];
        points += PlayPoints[mine];
    }

    Console.WriteLine(points);
}

static void Part2()
{
    var PlayPoints = new Dictionary<PlayType, int>()
    {
        {PlayType.Rock, 1},
        {PlayType.Paper, 2},
        {PlayType.Scissors, 3},
    };

    var readLines = File.ReadLines("input.txt");

    int points = 0;

    foreach (var line in readLines)
    {
        var opponent = line[0];
        var needResult = line[2];

        var resultPlay = DetermineMyPlayForResult(opponent, needResult);
        points += resultPlay.Points;
        points += PlayPoints[resultPlay.MyPlay];
    }

    Console.WriteLine(points);
}

static (int Points, PlayType MyPlay) DetermineMyPlayForResult(char opponent, char needResult)
{
    if (opponent == OpponentRock)
    {
        return needResult switch
        {
            NeedToLose => (LossPoints, PlayType.Scissors),
            NeedToDraw => (DrawPoints, PlayType.Rock),
            NeedToWin => (WinPoints, PlayType.Paper),
        };
    }
    else if (opponent == OpponentPaper)
    {
        return needResult switch
        {
            NeedToLose => (LossPoints, PlayType.Rock),
            NeedToDraw => (DrawPoints, PlayType.Paper),
            NeedToWin => (WinPoints, PlayType.Scissors),
        };
    }
    else if (opponent == OpponentScissors)
    {
        return needResult switch
        {
            NeedToLose => (LossPoints, PlayType.Paper),
            NeedToDraw => (DrawPoints, PlayType.Scissors),
            NeedToWin => (WinPoints, PlayType.Rock),
        };
    }

    throw new Exception("Shouldn't make it here");
}

static GameResultType DetermineGameResult(char opponent, char mine)
{
    if (opponent == OpponentRock)
    {
        return mine switch
        {
            MyRock => GameResultType.Draw,
            MyPaper => GameResultType.Win,
            MyScissors => GameResultType.Loss,
        };
    }
    else if (opponent == OpponentPaper)
    {
        return mine switch
        {
            MyRock => GameResultType.Loss,
            MyPaper => GameResultType.Draw,
            MyScissors => GameResultType.Win,
        };
    }
    else if (opponent == OpponentScissors)
    {
        return mine switch
        {
            MyRock => GameResultType.Win,
            MyPaper => GameResultType.Loss,
            MyScissors => GameResultType.Draw,
        };
    }

    throw new Exception("Shouldn't make it here");
}

enum GameResultType
{
    Unknown,
    Win,
    Draw,
    Loss
}


enum PlayType
{
    Unknown,
    Rock,
    Paper,
    Scissors
}
