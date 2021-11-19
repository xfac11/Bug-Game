namespace Bug.L_System
{
    interface IInstruction
    {
        char Symbol { get; set; }
        void Operation();

    }
}