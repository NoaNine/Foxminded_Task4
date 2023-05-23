namespace GameGuessNumber
{
    interface INumberGenerator
    {
        int Number { get; }

        void GenerateNumber();
    }
}
