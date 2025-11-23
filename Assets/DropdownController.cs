using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour, IMoveHandler, ISubmitHandler
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    [SerializeField]
    private GameObject arrowLeft;
    [SerializeField]
    private GameObject arrowRight;

    private bool isFocused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        dropdown = GetComponentInChildren<TMP_Dropdown>();
        EvaluateDropdown();
    }
    public void OnSubmit(BaseEventData eventData)
    {
            dropdown.Show();
            isFocused = true;
    }

    public void OnMove(AxisEventData eventData)
    {
        if (eventData.moveDir == MoveDirection.Right)
        {
            dropdown.value = Mathf.Min(dropdown.value + 1, dropdown.options.Count - 1);
        }
        if (eventData.moveDir == MoveDirection.Left)
        {
            dropdown.value = Mathf.Max(dropdown.value - 1, 0);
        }
        EvaluateDropdown();
    }
    
    public void EvaluateDropdown()
    {
        if (dropdown.value == 0)
        {
            arrowLeft.SetActive(false);
        }
        else
        {
            arrowLeft.SetActive(true);
        }
        if (dropdown.value == dropdown.options.Count - 1)
        {
            arrowRight.SetActive(false);
        }
        else
        {
            arrowRight.SetActive(true);
        }
    }
    public void MoveLeft()
    {
        dropdown.value = Mathf.Max(dropdown.value - 1, 0);
        EvaluateDropdown();
    }
    public void MoveRight()
    {
        dropdown.value = Mathf.Min(dropdown.value + 1, dropdown.options.Count - 1);
        EvaluateDropdown();
    }
    // Update is called once per frame
}
