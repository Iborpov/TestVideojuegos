public class PlayerFSM
{
    public IState CurrentState { get; private set; }
    public IddleState iddleState { get; private set; }
    public RunState runState { get; private set; }
    public AttackState attackState { get; private set; }
    public PickState pickState { get; private set; }

    public PlayerFSM(Player player)
    {
        this.iddleState = new IddleState(player);
        this.runState = new RunState(player);
        this.attackState = new AttackState(player);
        this.pickState = new PickState(player);
    }

    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }

    public void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.FixedUpdate();
        }
    }
}
