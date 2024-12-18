namespace Fun001;

public class HangmanGame(IWordProvider wordProvider, int maxAttempts = 6)
{
    private readonly string _word = wordProvider.GetWord();
    private readonly HashSet<char> _guessedLetters = new();
    private int _remainingAttempts = maxAttempts;

    public bool GuessLetter(char letter)
    {
        if (_guessedLetters.Contains(letter))
        {
            return false;
        }

        _guessedLetters.Add(letter);

        if (!_word.Contains(letter))
        {
            _remainingAttempts--;
        }

        return true;
    }

    public string GetWordDisplay()
    {
        return new string(_word.Select(c => _guessedLetters.Contains(c) ? c : '_').ToArray());
    }

    public bool IsGameOver()
    {
        return _remainingAttempts <= 0 || IsWordGuessed();
    }

    public bool IsWordGuessed()
    {
        return _word.All(c => _guessedLetters.Contains(c));
    }

    public int GetRemainingAttempts()
    {
        return _remainingAttempts;
    }
}
