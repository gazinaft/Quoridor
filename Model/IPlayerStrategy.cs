namespace Model
{
    public interface IPlayerStrategy
    {
        public void Think(Game game);
    
        public void SendVictory(Game game);
    }
}
