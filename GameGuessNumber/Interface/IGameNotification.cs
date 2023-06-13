namespace GameGuessNumber.Interface
{
    public interface IGameNotification : IGame
    {
        event Notification? Notify;
    }
}