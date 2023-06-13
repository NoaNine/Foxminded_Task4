namespace GameGuessNumber.Interface
{
    public interface IGame
    {
        event Notification? Notify;

        void StartGame();
    }
}