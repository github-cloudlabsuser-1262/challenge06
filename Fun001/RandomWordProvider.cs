namespace Fun001;

public class RandomWordProvider: IWordProvider
{
    private readonly List<string> _words;
    private readonly int _index;
    
    public RandomWordProvider(IEnumerable<string> words)
    {
        _words = [..words];
        _index = new Random().Next(_words.Count);
    }
    public string GetWord()
    {
        return _words[_index];
    }
}
