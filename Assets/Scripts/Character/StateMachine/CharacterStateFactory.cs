/// <summary>
/// add all possible root states and substates in here
/// </summary>
public class CharacterStateFactory
{
    CharacterStateMachine contex;
    public CharacterStateFactory(CharacterStateMachine currentContext)
    {
        contex = currentContext;
    }
    
    //root states
    public CharacterStateBase Grounded()
    {
        return new CharacterGroundedState(contex, this);
    }
    
    public CharacterStateBase InAir()
    {
        return new CharacterInAirState(contex, this);
    }
    
    //todo change this to a substate
    public CharacterStateBase Climbing()
    {
        return new CharacterClimbingState(contex, this);
    }

    //substates
    public CharacterStateBase Idle()
    {
        return new CharacterIdleState(contex, this);
    }

    public CharacterStateBase Walk()
    {
        return new CharacterWalkingState(contex, this);
    }
    
    public CharacterStateBase Run()
    {
        return new CharacterRuningState(contex, this);
    }

    public CharacterStateBase Jump()
    {
        return new CharacterJumpState(contex, this);
    }

    public CharacterStateBase SlideJump()
    {
        return new CharacterSlideJumpState(contex, this);
    }
    
    public CharacterStateBase Sliding()
    {
        return new CharacterSlideState(contex, this);
    }

    public CharacterStateBase Gliding()
    {
        return new CharacterGlideState(contex, this);
    }

    public CharacterStateBase Falling()
    {
        return new CharacterFallingState(contex, this);
    }
    
    //todo attack and interact states
}
