using UnityEngine;

public class GlideUpgrade : MonoBehaviour
{
    public CharacterMovement characterMovement;
    private void OnEnable()
    {
        if (characterMovement != null)
        {
            characterMovement.SetCanGlide(true);
        }
    }
    private void OnDisable()
    {
        if (characterMovement != null)
        {
            characterMovement.SetCanGlide(false);
        }
    }
}
