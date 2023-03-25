namespace TextBasedBlackJack
{

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