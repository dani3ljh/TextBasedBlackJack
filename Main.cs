namespace TextBasedBlackJack
{

enum Suit { Clubs, Diamonds, Hearts, Spades }

class Program
{
	private static readonly int MAX_PLAYERS = 4;
	
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
		
		Console.WriteLine($"Player Hand 1: {playerHands[0].Count} cards");
	}
	
	private static void GameRound()
	{
		if (isGameOver) return;
		
		BlackJackDeck playerHand = playerHands[currentPlayer];
		
		Console.Write($"Player {currentPlayer+1} has ");
		foreach (BlackJackCard card in playerHand) {
			Console.Write($"{card.Name} of {card.Suit}, ");
		}
		
		// Dont have to test for initial 21 here because it is done in the main loop
		// If player has Ace up and is first turn, ask for value
		if (turn == 0 && playerHand[1].Name == "Ace")
		{
			int value = GetInput.GetIntInputWithValues("Player {currentPlayer+1} has an Ace up, what value would you like it to be? (1 or 11): ", new List<int> {1, 11});
			
			playerHand[1].Value = value;
		}
		
	}
}

}