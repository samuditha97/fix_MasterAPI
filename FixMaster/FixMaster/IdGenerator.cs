using System;
namespace FixMaster
{
	public class IdGenerator
	{
        private static readonly Random Random = new Random();

        public static string GenerateUniqueId(int length = 5)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}

