namespace Input
{
    public interface IInput
    {
        InputButton Move { get; }
        InputState ViewDelta { get; }
        InputButton LeftClick { get; }
    }
}