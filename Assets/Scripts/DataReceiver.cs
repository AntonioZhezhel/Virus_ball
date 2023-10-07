using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataReceiver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private void Start()
    {
        DataHolder dataHolder = FindObjectOfType<DataHolder>();

        if (dataHolder != null)
        {
            text.SetText(dataHolder.textToPass);
        }
    }
}
