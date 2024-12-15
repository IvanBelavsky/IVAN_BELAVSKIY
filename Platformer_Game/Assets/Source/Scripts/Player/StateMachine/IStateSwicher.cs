public interface IStateSwicher
{
    void SwichState<T>() where T : IState;
}