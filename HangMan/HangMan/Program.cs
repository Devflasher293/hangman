using System;
using System.Text;

class HangmanGame
{
    private string[] words = { "keyboard", "banana", "house", "phone", "clothes" };
    private string selectedWord;
    private char[] guessedWord;
    private StringBuilder incorrectGuesses;
    private int remainingGuesses = 10;

    public void Start()
    {
        // Select a random word from the array
        Random random = new Random();
        selectedWord = words[random.Next(words.Length)];

        // Initialize guessedWord with underscores
        guessedWord = new char[selectedWord.Length];
        for (int i = 0; i < guessedWord.Length; i++)
        {
            guessedWord[i] = '_';
        }

        // Initialize incorrectGuesses
        incorrectGuesses = new StringBuilder();

        // Start the game loop
        while (remainingGuesses > 0 && !IsWordGuessed())
        {
            Console.Clear();
            DisplayGameStatus();
            Console.WriteLine("Enter a letter or guess the whole word:");
            string input = Console.ReadLine().ToLower();

            if (input.Length == 1)
            {
                MakeLetterGuess(input[0]);
            }
            else if (input.Length == selectedWord.Length)
            {
                MakeWordGuess(input);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a single letter or guess the whole word.");
            }
        }

        Console.Clear();
        DisplayGameStatus();

        if (IsWordGuessed())
        {
            Console.WriteLine("Congratulations! You guessed the word: " + selectedWord);
        }
        else
        {
            Console.WriteLine("Sorry, you ran out of guesses. The word was: " + selectedWord);
        }
    }

    private void DisplayGameStatus()
    {
        Console.WriteLine("Word: " + string.Join(" ", guessedWord));
        Console.WriteLine("Incorrect Guesses: " + incorrectGuesses);
        Console.WriteLine("Remaining Guesses: " + remainingGuesses);
        Console.WriteLine();
    }

    private void MakeLetterGuess(char letter)
    {
        if (selectedWord.Contains(letter))
        {
            for (int i = 0; i < selectedWord.Length; i++)
            {
                if (selectedWord[i] == letter)
                {
                    guessedWord[i] = letter;
                }
            }
        }
        else
        {
            if (!incorrectGuesses.ToString().Contains(letter))
            {
                incorrectGuesses.Append(letter + " ");
                remainingGuesses--;
            }
        }
    }

    private void MakeWordGuess(string word)
    {
        if (word == selectedWord)
        {
            guessedWord = selectedWord.ToCharArray();
        }
        else
        {
            remainingGuesses -= 2; // Penalize for guessing the whole word incorrectly
        }
    }

    private bool IsWordGuessed()
    {
        return new string(guessedWord) == selectedWord;
    }

    static void Main(string[] args)
    {
        HangmanGame hangman = new HangmanGame();
        hangman.Start();
    }
}
