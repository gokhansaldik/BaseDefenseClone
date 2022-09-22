namespace Interface
{
    public interface IState
    {
        void Tick();
        void OnExit();
        void OnEnter();
    }
}