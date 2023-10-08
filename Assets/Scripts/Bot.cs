using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private GizmoDetect GizmoDetect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sphere")
        {
            gameObject.SetActive(false);
            Destroy(other.gameObject);
            GizmoDetect.UpdateList();
            GizmoDetect.CheckWin();
        }
    }
}