using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeTextForInput : MonoBehaviour
{
    public string inputController;
    public string inputKeyboard;
    private void Update()
    {
        string currentdevide = PlayerInput.GetPlayerByIndex(0).currentControlScheme;
        if (currentdevide == "Gamepad")
        {
            GetComponent<TMP_Text>().text = inputController;

        }
        else if (currentdevide == "Keyboard&Mouse")
        {
            GetComponent<TMP_Text>().text = inputKeyboard;
        }
    }
}
