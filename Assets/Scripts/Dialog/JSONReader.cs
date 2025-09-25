using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset textJSON;

    [System.Serializable]
    public class Message
    {
        public bool targetResponding;
        public string saying;
    }

    [System.Serializable]
    public class MessageList
    {
        public Message[] message;
    }

    public MessageList myMessageList = new MessageList();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myMessageList = JsonUtility.FromJson<MessageList>(textJSON.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
