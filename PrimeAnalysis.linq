<Query Kind="Program">
  <Namespace>System.IO</Namespace>
</Query>

void Main()
{
	var filePath = @"C:\ClosestPrime\1e7_primes.txt";
	var allPrimes = System.IO.File.ReadAllLines(filePath);
	var primes = allPrimes.Select(x => int.Parse(x)).ToList();
	var diffs = new Dictionary<int,int>();
	for(int i = 1; i < primes.Count(); i++)
	{
		var diff = primes[i] - primes[i-1];
		if(!diffs.ContainsKey(diff))
			diffs.Add(diff,0);
		diffs[diff] +=1;
	}
	diffs.OrderBy(x => x.Key).Dump();
}

// Define other methods and classes here
