namespace Fun001;

public class HangmanTui(HangmanGame game) : IHangmanInterface
{
    public void Render()
    {
        Console.WriteLine($"Word: {game.GetWordDisplay()}");
        Console.WriteLine($"Remaining Attempts: {game.GetRemainingAttempts()}");

        // Draw the hangman based on remaining lives
        Console.WriteLine(GetHangmanDrawing(game.GetRemainingAttempts()));

        // Display the number of lives left
        Console.WriteLine($"Lives Left: {game.GetRemainingAttempts()}");
    }

    private string GetHangmanDrawing(int lives)
    {
        // Return the appropriate hangman drawing based on lives
        switch (lives)
        {
            case 6:
                return @"
 _______
 |     |
       |
       |
       |
       |
       |";
            case 5:
                return @"
 _______
 |     |
 O     |
       |
       |
       |
       |";
            case 4:
                return @"
 _______
 |     |
 O     |
 |     |
       |
       |
       |";
            case 3:
                return @"
 _______
 |     |
 O     |
/|     |
       |
       |
       |";
            case 2:
                return @"
 _______
 |     |
 O     |
/|\    |
       |
       |
       |";
            case 1:
                return @"
 _______
 |     |
 O     |
/|\    |
/      |
       |
       |";
            case 0:
                return @"
 _______
 |     |
 O     |
/|\    |
/ \    |
       |
       |";
            default:
                return @"
 _______
 |     |
       |
       |
       |
       |
       |";
        }
    }

    public void Play()
    {
        while (!game.IsGameOver())
        {
            Render();
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
    }
}
