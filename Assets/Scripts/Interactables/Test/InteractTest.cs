using UnityEngine;

public class InteractTest : MonoBehaviour
{
    private IInteractable interactable;
    public Material offMaterial;
    public Material onMaterial;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        interactable = gameObject.GetComponent<IInteractable>();
        if (interactable != null) interactable.InteractEvent += OnBlock;
    }
    
    private void OnBlock(bool isOn)
    {
        if (isOn)
        {
            gameObject.GetComponent<MeshRenderer>().material = onMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = offMaterial;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
