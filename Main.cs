﻿namespace TextBasedBlackJack
{

enum Suit { Clubs, Diamonds, Hearts, Spades }

class Program
{
	private static readonly int MAX_PLAYERS = 5;
	
	private static BlackJackDeck deck = new BlackJackDeck();
	private static List<BlackJackDeck> playerHands = new List<BlackJackDeck>();
	
	private static int playerCount = 0;
	
	private static int currentPlayer = 0;
	
	private static int turn = 0;
	
	private static bool isGameOver = false;
	
	private static void Main(string[] args)
	{
		// Ask for player count
		// Setup game
		// Game loop
		//   - Ask for player input
		//   - Check for ace and ask for value if upcard, make sure not to ask if it would bust
		//   - Check if player busts, then remove player from game
		//   - If not, check if player has 21 and if so, end game
		//   - If not, go to next player
		
		Console.Clear();
		
		int playerCount = GetInput.GetIntInputWithRange("How many players? ", 1, MAX_PLAYERS);
		
		Console.Clear();
		
		SetupGame(playerCount);
		
		// test for intital 21
		for(int i = 0; i < playerCount; i++)
		{
			if(playerHands[i].GetTotalValue() == 21)
			{
				Console.WriteLine($"Player {i+1} has 21 and wins!");
				isGameOver = true;
				Console.ReadLine();
			}
		}
		
		while (!isGameOver) {
			GameRound();
			if (turn != 0) break; // Temp
		}
	}
	
	private static void GameRound()
	{
		if (isGameOver) return;
		
		BlackJackDeck playerHand = playerHands[currentPlayer];
		
		Console.WriteLine($"Player {currentPlayer+1} has {playerHand.ToString()}");
		
		// Dont have to test for initial 21 here because it is done in the main method
		// If player has Ace up and is first turn, ask for value
		// Also if the player has both aces then you dont need to ask for value
		if (turn == 0 && playerHand[1].Name == "Ace" && playerHand[0].Name != "Ace")
		{
			String prompt = $"Player {currentPlayer+1} has an Ace up, what value would you like it to be? (1 or 11): ";
			int value = GetInput.GetIntInputWithValues(prompt, new List<int> {1, 11});
			
			playerHand[1].Value = value;
		}
		
		currentPlayer++;
		
		if (currentPlayer >= playerCount) {
			currentPlayer = 0;
			turn++;
		}
	}
	
	private static void SetupGame(int playerCount = 2)
	{
		Program.playerCount = playerCount;
		currentPlayer = 0;
		turn = 0;
		
		deck = new BlackJackDeck();
		playerHands = new List<BlackJackDeck>();
		
		for (int i = 0; i < playerCount; i++) {
			playerHands.Add(new BlackJackDeck(false));
		}
		
		// Face down card
		foreach (BlackJackDeck hand in playerHands) {
			BlackJackCard card = deck[0];
			card.IsFaceUp = false;
			hand.Add(card);
			deck.RemoveAt(0);
		}
		
		// Face up card
		foreach (BlackJackDeck hand in playerHands) {
			BlackJackCard card = deck[0];
			card.IsFaceUp = true;
			hand.Add(card);
			deck.RemoveAt(0);
		}
	}
}

}