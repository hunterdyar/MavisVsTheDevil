namespace MavisVsTheDevil.Engine;

public abstract class StateBase
{
	protected StateMachine _machine;
	public Action OnEnterState;
	public Action OnExitState;
	protected StateBase(StateMachine machine)
	{
		_machine = machine;
	}

	public virtual void OnEnter()
	{
		OnEnterState?.Invoke();
	}

	public virtual void OnExit()
	{
		OnExitState?.Invoke();
	}

	public virtual void Tick(float delta)
	{
		
	}

	public virtual void TypeKeyPressed(char key)
	{
		
	}
}