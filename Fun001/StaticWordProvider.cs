namespace Fun001;

public class StaticWordProvider(string word) : IWordProvider
{
    public string GetWord()
    {
        return word;
    }
}
