using Fun001;

IWordProvider wordProvider = new FileWordProvider("wordlist.txt");
var game = new HangmanGame(wordProvider);
var console = new HangmanTui(game);
console.Play();
