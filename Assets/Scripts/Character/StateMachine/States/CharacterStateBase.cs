using UnityEngine;

public abstract class CharacterStateBase
{
    protected bool isRootState = false;
    protected CharacterStateMachine _context;
    protected CharacterStateFactory _factory;
    protected CharacterStateBase currentSuperState;
    protected CharacterStateBase currentSubState;
    public CharacterStateBase(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory)
    {
        _context = currentContext;
        _factory = characterStateFactory;
    }

    public abstract void EnterState();
    
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    
    public abstract void ExitState();
    
    public abstract void CheckSwitchStates();
    
    public abstract void InitializeSubState();

    public void UpdateStates()
    {
        UpdateState();
        
        if (currentSubState != null)
        {
            //Debug.Log(currentSubState.GetType().Name);
            currentSubState.UpdateStates();
        }
    }

    public void FixedUpdateStates()
    {
        FixedUpdateState();

        if (currentSubState != null)
        {
            currentSubState.FixedUpdateStates();
        }
    }

    protected void SwitchState(CharacterStateBase newState)
    {
        ExitState();

        newState.EnterState();
        
        if (isRootState)
        {
            _context.CurrentState = newState;
        } else if (currentSuperState != null)
        {
            currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(CharacterStateBase newSuperState)
    {
        currentSuperState = newSuperState;
    }

    protected void SetSubState(CharacterStateBase newSubState)
    {
        //todo check if already a sub state
        if(currentSubState == null || currentSubState.ToString() != newSubState.ToString())
        {
            currentSubState = newSubState;
            currentSubState.EnterState();
            newSubState.SetSuperState(this);
        }
    }
    
}
