namespace SmartLibrary.ConsoleApp
{
    public abstract class Widget
    {
        public Widget? Successor { get; private set; }
        public abstract Task DrawAsync();

        public Widget SetSuccessor(Widget? successor)
        {
            Successor = successor;
            return this;
        }
    }
}
