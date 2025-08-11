using UnityEngine;

public class DoubleJumpUpgrade : MonoBehaviour
{
    public int newJumpCount = 2;
    public CharacterMovement characterMovement;
    private void OnEnable()
    {
        if (characterMovement != null)
        {
            characterMovement.SetJumpCount(newJumpCount);
        }
    }

    private void OnDisable()
    {
        if (characterMovement != null)
        {
            characterMovement.ResetJumpCount();
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