using UnityEngine;

public class GlideUpgrade : UpgradeMonoBehaviour
{
    public CharacterMovement characterMovement;
    public CharacterStateMachine characterMachine;
    private void OnEnable()
    {
            if (characterMovement != null) characterMovement.SetCanGlide(true);
            if (characterMachine != null) characterMachine.SetCanGlide(true);
    }
    private void OnDisable()
    {
            if (characterMovement != null) characterMovement.SetCanGlide(false);
            if (characterMachine != null) characterMachine.SetCanGlide(false);
    }
}
