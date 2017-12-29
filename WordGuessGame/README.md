# Word Guessing Game Console App

**Author**: Ariel R. Pedraza <br />
**Version**: 1.0.0

## Overview
This application is a Word Guessing Game where a word is chosen at random from a word bank. You are able to add to, remove from, or view word bank before starting a game.
During the game, underscores will appear for unguessed characters in the word. Total guesses and guess history is displayed. Then you will enter one character to guess. If correct, the underscore will be replaced with the character entered, and if not, hidden characters do not change. History and guess count is updated either way. This continues until the entire word is guessed correctly.

## Getting Started
The following is required to run the program.
1. Visual Studio 2017 
2. The .NET desktop development workload enabled 

## Example
```
Program starting...
1. Start Game
2. View Word Bank
3. Add to Word Bank
4. Remove from Word Bank
5. Exit Game
Enter selection: 1

_ _ _ _ _ _ Total Guesses: 0
History: 
Enter a letter to guess: S

S _ _ _ _ _ Total Guesses: 1
History: S
Enter a letter to guess:
```

## Architecture
This application is created using ASP.NET Core 2.0 Console applicaitons. <br />
*Language*: C# <br />
*Type of Applicaiton*: Console Application <br />