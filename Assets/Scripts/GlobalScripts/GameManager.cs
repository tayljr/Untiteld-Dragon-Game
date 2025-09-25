using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IPauseable
{
    public GameObject _player;
    private static GameManager _instance;
    public static GameManager instance {  get { return _instance; } }   
    
    public bool isPaused = false;
    
    public bool inDialogue = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        
        StartCoroutine(FindPlayer(player =>
        {
            Debug.Log("found Player" + player.name);
        }));
    }

    public void OnPause()
    {
        isPaused = true;
        _player.SendMessage("OnPause");
    }
    public void OnResume()
    {
        isPaused = false;
        _player.SendMessage("OnResume");

    }

    // Update is called once per frame
    void Update()
    {
            Time.timeScale = isPaused ? 0f : 1f ;
    }
    //im going to real chatGPT made this, its kinda peam!
    public IEnumerator FindPlayer(System.Action<GameObject> onFound)
    {
        // Make a container for this piece of shit so you can hold something; otherwise it's a slippery fuck
        GameObject player = null;

        // Now we make a bounty for this fucker
        while (player == null)
        {
            // If you find that fucker, put that fucker in that little fucking container you just made
            player = GameObject.FindWithTag("Player");

            if (player == null)
            {
                // Now wait, otherwise this little function gonna blow up your funny little CPU
                yield return null;
            }
        }

        // Store the found bastard so other scripts can use it
        _player = player;

        // CALL TO THE WORLD THAT YOU JUST CAUGHT THIS SON OF A BITCH AND IF 
        // ANYBODY WANTS THIS FUCKER YOU CAN HAVE HIM!!!
        onFound?.Invoke(player);
    }

}
