namespace CubeTower.Core.States
{
    public interface IState
    {
        void Enter();
    }

    public interface IExitableState : IState
    {
        void Exit();
    }

    public interface IUpdateableState : IState
    {
        void Update();
    }
}