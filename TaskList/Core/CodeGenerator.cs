using System;
using System.Text;

namespace filmblogu.Core
{
    public class CodeGenerator
    {
        public string RandomPassword(int lenght)
        {
            var passwordBuilder = new StringBuilder();

            for (int i = 0; i < lenght; i++)
            {
                int path = _random.Next(1, 4);

                switch (path)
                {
                    case 1:
                        passwordBuilder.Append(RandomString(1, true)); //lowercase
                        break;
                    case 2:
                        passwordBuilder.Append(RandomNumber(0, 9)); //number
                        break;
                    case 3:
                        passwordBuilder.Append(RandomString(1)); //uppercase
                        break;
                    default:
                        break;
                }
            }
            
            return passwordBuilder.ToString();
        }

        private readonly Random _random = new Random();

        // Generates a random number within a range.
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.

            // char is a single Unicode character
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
