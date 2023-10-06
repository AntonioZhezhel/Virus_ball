using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private string TriggerDoor = "MainSphere";
    [SerializeField] protected UnityEvent EventDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainSphere")
        {
            EventDoor.Invoke();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MainSphere")
        {
            SceneManager.LoadScene(0);
        }
    }
}
