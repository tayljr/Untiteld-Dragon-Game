using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GateScript : MonoBehaviour, IInteractable
{
    public event IInteractable.BoolDelegate InteractEvent;

    private BoxCollider boxCollider;

    public int KeysNeededForPass = 1;

    public bool isOpen = false;

    bool animationFinished;
    [SerializeField]
    private GameObject CameraPrefab;
    [SerializeField]
    private Transform CameraPos;

    Animator animator;
    private void Awake()
    {
        //gameManager = GameManager.instance
    }
    public void StartInteract(GameObject interactor)
    {
        int currentKeys = CollectiblesCollector.Instance.Keys;
        //if (currentKeys == KeysNeededForPass)
        //{
        //    StartCoroutine(OpenRoutine());
        //} // paul you are a fucking idiot
        if (currentKeys >= CollectiblesCollector.Instance.Keys)
        {
            StartCoroutine(OpenRoutine());
        }
       
        else
        {
            animator.SetTrigger("Shake");
        }
    }

    public void StopInteract(GameObject interactor)
    {

    }
    public IEnumerator OpenRoutine()
    {
        yield return null;
        var CamInstance = Instantiate(CameraPrefab,CameraPos.position,CameraPos.rotation);
        animator.SetTrigger("Unlock");

        yield return new WaitUntil(CheckBool);

        Destroy(CamInstance);
        boxCollider.enabled = false;
        StopAllCoroutines();
    }
    public bool CheckBool()
    {
            return animationFinished;
    }
    public void SetFinished()
    {
        animationFinished = true; 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsOpen", isOpen);
    }

}
