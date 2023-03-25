namespace TextBasedBlackJack
{
	
static class GetInput
{
	public static int GetIntInput(string prompt, int min = int.MinValue, int max = int.MaxValue)
	{
		int input = 0;
		bool isValid = false;
		
		while (!isValid)
		{
			Console.Write(prompt);
			isValid = int.TryParse(Console.ReadLine(), out input);
			
			if (!isValid)
			{
				Console.WriteLine("Invalid input. Please enter a number.");
			}
			else if (input < min || input > max)
			{
				if (min == int.MinValue && max == int.MaxValue)
					throw new Exception("What the fuck");
				else if (min == int.MinValue)
					Console.WriteLine($"Invalid input. Please enter a number less than {max}.");
				else if (max == int.MaxValue)
					Console.WriteLine($"Invalid input. Please enter a number greater than {min}.");
				else
					Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
				isValid = false;
			}
		}
		
		return input;
	}
	
	private static string GetStringInput(string prompt, List<String> validInputs)
	{
		string input = "";
		
		while (true)
		{
			Console.Write(prompt);
			input = Console.ReadLine() ?? "";
			
			if (validInputs.Contains(input)) break;
			
			Console.WriteLine("Invalid input. Please enter a valid input.");
		}
		
		return input;
	}
}

}