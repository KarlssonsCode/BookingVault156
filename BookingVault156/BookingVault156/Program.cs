namespace BookingVault156
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Methods.Running();
            while (true)
            {
                Methods.ShowDwellers();
                Methods.AddDweller();
                Console.Clear();
            }
            //Methods.AddRoom();

        }
    }
}