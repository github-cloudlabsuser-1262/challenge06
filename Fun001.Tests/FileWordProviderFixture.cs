using System;
using System.Collections.Generic;
using System.IO;
using static System.Environment;

namespace Fun001.Tests;

public class FileWordProviderFixture : IDisposable
{
    public string Path { get; }
    public readonly List<string> Words; 

    public FileWordProviderFixture()
    {
        Words = ["word1", "word2", "word3"];
        // Setup: Create a temporary file with many words
        Path = System.IO.Path.GetTempFileName();
        File.WriteAllText(Path, string.Join(NewLine, Words));
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        
        // Teardown: Delete the temporary file
        if (File.Exists(Path))
        {
            File.Delete(Path);
        }
    }
}
