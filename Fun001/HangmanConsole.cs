namespace Fun001;

public interface IHangmanInterface
{
    void Play();
}

public class HangmanConsole(HangmanGame game) : IHangmanInterface
{
    public void Play()
    {
        while (!game.IsGameOver())
        {
            Console.WriteLine($"Word: {game.GetWordDisplay()}");
            Console.WriteLine($"Remaining Attempts: {game.GetRemainingAttempts()}");
            Console.Write("Guess a letter: ");
            var line = Console.ReadLine();
            switch (line)
            {
                case { Length: 0 }:
                case null:
                    continue;
                default:
                {
                    var guess = line[0];
                    game.GuessLetter(guess);
                    break;
                }
            }
        }

        Console.WriteLine(game.IsWordGuessed()
            ? "Congratulations! You've guessed the word!"
            : "Game Over! Better luck next time.");
    }
}
