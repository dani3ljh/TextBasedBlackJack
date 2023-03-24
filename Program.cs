namespace TextBasedBlackJack
{

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
		
		int playerCount = GetPlayerCount();
		
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
	
	private static void SetupGame(int playerC = 2)
	{
		playerCount = playerC;
		currentPlayer = 0;
		turn = 0;
		
		deck = new BlackJackDeck();
		
		for (int i = 0; i < playerCount; i++) {
			playerHands.Add(new BlackJackDeck(false));
		}
		
		// Face down card
		foreach (BlackJackDeck hand in playerHands) {
			BlackJackCard card = deck[0];
			card.IsFaceUp = false;
			deck.RemoveAt(0);
		}
		
		// Face up card
		foreach (BlackJackDeck hand in playerHands) {
			BlackJackCard card = deck[0];
			card.IsFaceUp = true;
			deck.RemoveAt(0);
		}
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
			while(true)
			{
				Console.WriteLine($"Player {currentPlayer+1} has an Ace up, what value would you like it to be? (1 or 11): ");
				string? input = Console.ReadLine();
				int value;
				if (int.TryParse(input, out value) && (value == 1 || value == 11)) {
					playerHand[1].Value = value;
					break;
				}
			}
		}
		
	}

	private static int GetPlayerCount() 
	{
		int playerCount;
		
		while (true)
		{
			Console.Write("How many players: ");
			string? playerCountString = Console.ReadLine();
			if (!int.TryParse(playerCountString, out playerCount) || playerCount < 2 || playerCount > MAX_PLAYERS+1) 
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Invalid player count try again");
				if(playerCount > MAX_PLAYERS+1)
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine($"Max players is {MAX_PLAYERS}");
				}
				Console.Write("\n");
				Console.ResetColor();
				
				continue;
			}
			return playerCount;
		}
	}
}

enum Suit
{
	Spades,
	Clubs,
	Hearts,
	Diamonds
}

class BlackJackCard
{
	public Suit Suit { get; set; }
	public int Value { get; set; }
	public string Name { get; set; } 
	public bool IsFaceUp { get; set; }
	
	public BlackJackCard(Suit suit, int value, string name, bool isFaceUp = false)
	{
		Suit = suit;
		Value = value;
		Name = name;
		IsFaceUp = isFaceUp;
	}
	
	public bool IsValidCard()
	{
		if (Value < 1 || Value > 11) {
			return false;
		}
		
		// test if name is a number, if not, test if it is a face card or Ace
		int number;
		if (!int.TryParse(Name, out number)) {
			if (Name != "Ace" && Name != "Jack" && Name != "Queen" && Name != "King") {
				return false;
			}
		}
		
		return true;
	}
}

class BlackJackDeck : List<BlackJackCard>
{
	public BlackJackDeck(bool isFullAndShuffled = true)
	{
		if (isFullAndShuffled) {
			GenerateNormalDeck();
			Shuffle();
		}
	}
	
	public void GenerateNormalDeck()
	{
		Clear();
		
		foreach (Suit suit in Enum.GetValues(typeof(Suit))) {
			for (int i = 1; i <= 13; i++) 
			{
				int value = i;
				string name = i.ToString();
				
				if (i==1) {
					name = "Ace";
				}
				else if (i==11) 
				{
					value = 10;
					name = "Jack";
				} 
				else if (i==12) 
				{
					value = 10;
					name = "Queen";
				} 
				else if (i==13) 
				{
					value = 10;
					name = "King";
				}
				
				Add(new BlackJackCard(suit, value, name));
			}
		}
	}
	
	public int GetTotalValue()
	{
		int total = 0;
		foreach (BlackJackCard card in this) {
			total += card.Value;
		}
		return total;
	}
	
	// Add input sanitization on the Add and AddRange method aswell as the BlackJackDeck[] operator
	public new void Add(BlackJackCard card)
	{
		if (!card.IsValidCard()) {
			throw new Exception("Invalid card");
		}
		base.Add(card);
	}
	public new void AddRange(IEnumerable<BlackJackCard> cards)
	{
		foreach (BlackJackCard card in cards) {
			if (!card.IsValidCard()) {
				throw new Exception("Invalid card");
			}
		}
		base.AddRange(cards);
	}
	public new BlackJackCard this[int index]
	{
		get { return base[index]; }
		set
		{
			if (!value.IsValidCard()) {
				throw new Exception("Invalid card");
			}
			base[index] = value;
		}
	}
	
	public void Shuffle()
	{
		Random random = new Random();
		for (int i = 0; i < Count; i++) {
			int randomIndex = random.Next(0, Count);
			BlackJackCard temp = this[i];
			this[i] = this[randomIndex];
			this[randomIndex] = temp;
		}
	}
}
}