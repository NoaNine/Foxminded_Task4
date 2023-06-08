namespace GameGuessNumber
{
    public class Settings
    {
        public int MinValueOfHiddenNumber { get; set; }
        public int MaxValueOfHiddenNumber { get; set; }
        public int MaxNumberAttempts { get; set; }

        public void ValidSettings()
        {
            if (MinValueOfHiddenNumber > MaxValueOfHiddenNumber ||
                MinValueOfHiddenNumber < 0 ||
                MaxNumberAttempts < 1 ||
                MaxNumberAttempts < 1)
            {
                throw new ArgumentException();
            }
        }
    }
}
