public interface IStateMachine<T>
{
    public T CurrentState { get; set; }
    public void ChangeState(T newState);
    public void OnUpdateState();
}
