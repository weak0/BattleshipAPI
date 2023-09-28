namespace BattleShipAPI.Exceptions
{
    public class OutOfGameBoardException : Exception
    {
        public OutOfGameBoardException(string msg) : base(msg)
        {
            
        }
    }
}
