using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private GizmoDetect GizmoDetect;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Sphere") 
        {
            // LeanTween.delayedCall(gameObject, 0.1f, () => {
            //     if (other != null)
            //         Destroy(other.gameObject);
            //     Destroy(gameObject);
            // });
            //Destroy(gameObject);
            gameObject.SetActive(false);
            Destroy(other.gameObject);
            GizmoDetect.UpdateList();
            GizmoDetect.CheckWin();

        }
    }
}