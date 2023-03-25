namespace TextBasedBlackJack
{
	
static class GetInput
{
	public static int GetIntInputWithRange(string prompt, int min = int.MinValue, int max = int.MaxValue)
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
				// Dont need to check for both the min and max being the max absolute value because:
				// How would a number greater than int.MaxValue or a number less than int.MinValue be stored into an int?
				if (min == int.MinValue)
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
	
	public static int GetIntInputWithValues(string prompt, List<int> validInputs)
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
			else if (!validInputs.Contains(input))
			{
				Console.WriteLine("Invalid input. Please enter a valid input.");
				isValid = false;
			}
		}
		
		return input;
	}
	
	public static string GetStringInput(string prompt, List<String> validInputs)
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