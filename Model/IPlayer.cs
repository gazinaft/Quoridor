namespace Model {
    public interface IPlayer {
        
        public int PlayerId { get; set; }

        public Cell StartCell { get; set; }

        public bool PlayerIsActive { get; set; }

        public int VictoryRow { get; set; }

        public Cell CurrentCell { get; set; }

        public int WallsCounter { get; set; }
        
        public IPlayerStrategy PlayerStrategy { get; set; } // таким чином не будемо переписувати реалізацію гравця в залежності від зміни алгоритму

        public void Decide(Game game);

        public ICommand LastStep { get; set; }
    }

}
