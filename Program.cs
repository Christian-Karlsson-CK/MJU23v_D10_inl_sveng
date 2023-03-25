﻿namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (command == "load")
                {
                    //FIXME crashes on System.IO.FileNotFoundException
                    //TODO Add path to dict folder so user only need input dictionary filename.
                    if (argument.Length == 2)
                    {
                        using (StreamReader sr = new StreamReader(argument[1]))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                    }
                    else if(argument.Length == 1)
                    {
                        using (StreamReader sr = new StreamReader(defaultFile))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                    }
                }
                else if (command == "list")
                {
                    //FIXME crashes on non initiated dictionary (System.NullReferenceException)
                    foreach (SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                else if (command == "new")
                {
                    //FIXME System.NullReferenceException (if no 'load' has been done)
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if(argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedishWord = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string englishWord = Console.ReadLine();
                        dictionary.Add(new SweEngGloss(swedishWord, englishWord));
                    }
                }
                else if (command == "delete")
                {
                    
                    if (argument.Length == 3)
                    {
                        //FIXME System.NullReferenceException (if no 'load' has been done)
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++) {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        //FIXME if index == -1 gives System.ArgumentOutOfRangeException
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedishWord = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string englishWord = Console.ReadLine();

                        //FIXME System.NullReferenceException (if no 'load' has been done)
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == swedishWord && gloss.word_eng == englishWord)
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        //FIXME System.NullReferenceException
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
                    {   
                        Console.WriteLine("Write word to be translated: ");
                        string word = Console.ReadLine();
                        //FIXME System.NullReferenceException
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == word)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == word)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                }

                //TODO add help option
                else if (command == "help")
                {
                    Console.WriteLine("help:                show this help text");
                    Console.WriteLine("load:                load default dictionary file");
                    Console.WriteLine("load /file/:         load user defined file");
                    Console.WriteLine("list:                lists all words from dictionary file");
                    Console.WriteLine("new:                 adds new word to dictionary");
                    Console.WriteLine("new s e:             adds new word to dictionary were 's' is swedish word and e is 'english' word");
                    Console.WriteLine("delete:              delete word from dictionary");
                    Console.WriteLine("delete s e:          delete word from dictionary were 's' is swedish word and e is 'english' word");
                    Console.WriteLine("translate:           translates word from dictionary");
                    Console.WriteLine("translate word:      translates word from dictionary were 'word' is word to be translated");
                }

                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }
    }
}