namespace TextBasedBlackJack
{

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

}