namespace BattleShipAPI.Exceptions
{
    public class NotYourTurnException : Exception
    {
        public NotYourTurnException(string msg) : base(msg)
        {
            
        }
    }
}
