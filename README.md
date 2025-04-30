# Card Game Assessment Documentation

## 1. Introduction

This document explains the design, implementation, and error handling of the multiplayer card game. The application is built in **C#** as a console application and is designed to run on **Windows**.

---

## 2. Requirements Overview

- **Players:** 6 players, each dealt 5 cards.
- **Deck:** Two standard 52-card decks plus 2 Jokers (value 14).
- **Card Values:**  
  - Number cards: face value  
  - J = 11, Q = 12, K = 13, A = 11  
  - Joker = 14
- **Scoring:** Each player’s score is the sum of their 5 card values.
- **Tie-breaker:**  
  - If players tie, calculate a “suit score” for tied players only.  
  - Suit values: Diamonds = 1, Hearts = 2, Spades = 3, Clubs = 4  
  - Suit score = multiplication of all 5 suit values.
- **Winner:** Player with the highest score (or highest suit score in a tie).
- **Exception Handling:** The application must handle exceptions gracefully and display all relevant information.

---

## 3. Technology Stack

- **Language:** C#
- **Platform:** .NET 6+ Console Application
- **IDE:** Visual Studio Code (VSCode) or any IDE you want to use.

---

## 4. Application Structure

- **Program.cs:** Main entry point, game logic, and error handling.
- **Classes:**
  - `Card`: Represents a playing card.
  - `Player`: Represents a player and their hand.
  - `CardGameException`: Custom exception for game-specific errors.

---

## 5. Key Features and Design Decisions

- **Deck Creation:**  
  Two standard decks (52 cards each) plus 2 Jokers are created and shuffled.
- **Card Dealing:**  
  Each player is dealt 5 random cards, removing cards from the deck as they are dealt.
- **Score Calculation:**  
  Each player’s score is calculated based on card values.
- **Tie-Breaking:**  
  If there is a tie, the suit score is calculated and compared.  
  _A code comment “Breaking ties” is included in the tie-breaker logic as required._
- **Error Handling:**  
  Comprehensive error handling is implemented using try-catch blocks and a custom exception class.
- **User Interface:**  
  The application displays each player’s hand, their score, and the winner(s).

---

## 6. Error Handling Approach

- **Custom Exception:**  
  `CardGameException` is used for all game-specific errors.
- **Validation:**  
  All input and state changes are validated (e.g., correct number of cards, valid card values).
- **Graceful Failure:**  
  The application catches and displays errors without crashing, and prompts the user before exiting.
- **Debugging Support:**  
  Inner exception messages and stack traces are displayed for unexpected errors.

---

## 7. How to Run the Application

1. **Prerequisites:**  
   - [.NET SDK](https://dotnet.microsoft.com/download) installed  
   - VSCode with [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)  
2. **Setup:**  
   - Open terminal in VSCode  
   - Navigate to the project folder:
      ```bash 
      cd CardGame
      ```
   - Run:
     ```bash
     dotnet run
     ```
3. **Output:**  
   - The console will display each player’s cards, scores, and the winner(s).

---

## 8. Example Output
**Initial Hands:**

**Player 1 (Score: 45):**
 10 of Hearts
 J of Spades
 7 of Clubs
 A of Diamonds
 Joker of Joker

**Player 2 (Score: 39):**
 9 of Clubs
 8 of Hearts
 Q of Diamonds
 2 of Spades
 K of Hearts

...

 Winner(s):
 - Player 1 with score 45


**If there is a tie, suit scores and tie-breaking logic will be displayed:**

- Breaking tie s
- Player 2 (Suit Score: 48)
- Player 5 (Suit Score: 24)

Winner(s):
- Player 2 with score 41 and suit score 48


---

## 9. Code Comments and Documentation

- The code is commented throughout to explain logic, especially in complex sections like deck creation, dealing, and tie-breaking.
- Special comments are included as per the assessment instructions (e.g., `// Breaking ties`).

---

## 10. Additional Notes

- The application is designed to be easily extensible (e.g., for more players or different rules).
- All randomization uses the `Random` class for fair dealing.
- The code follows C# best practices for readability and maintainability.
---
