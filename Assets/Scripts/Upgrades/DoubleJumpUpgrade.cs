using UnityEngine;

public class DoubleJumpUpgrade : UpgradeMonoBehaviour
{
    public int newJumpCount = 2;
    public CharacterMovement characterMovement;
    public CharacterStateMachine characterMachine;
    private void OnEnable()
    {
        if (characterMovement != null)
        {
            characterMovement.SetJumpCount(newJumpCount);
        }

        if (characterMachine != null)
        {
            characterMachine.SetJumpCount(newJumpCount);
        }
    }

    private void OnDisable()
    {
        if (characterMovement != null)
        {
            characterMovement.ResetJumpCount();
        }
        if (characterMachine != null)
        {
            characterMachine.ResetJumpCount();
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}