namespace ClientServerArchitecture.Client
{
    public class Сlient : IClient, IMessage
    {
        public string Title { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Message { get; set; }
        public bool IsHorizontal { get; set; }
    }
}