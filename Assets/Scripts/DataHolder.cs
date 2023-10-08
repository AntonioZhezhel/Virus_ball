using UnityEngine;


public class DataHolder : MonoBehaviour
{
    public string TextToPass;
    private static DataHolder Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}