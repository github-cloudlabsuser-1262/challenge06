namespace Fun001;

public class FileWordProvider : IWordProvider
{
    private readonly RandomWordProvider _randomWordProvider;

    public FileWordProvider(string path)
    {
        // load words from file, one word per line
        var words = File.ReadAllLines(path);
        _randomWordProvider = new RandomWordProvider(words);
    }
    
    public string GetWord()
    {
        return _randomWordProvider.GetWord();
    }
}
