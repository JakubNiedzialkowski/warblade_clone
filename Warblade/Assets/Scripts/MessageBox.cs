using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public GameObject messageBox;
    [SerializeField]
    private float delay = 3f;
    // Update is called once per frame
    void Update()
    {
        if(messageBox.activeSelf)
            delay -= Time.deltaTime;
        if(delay <= 0f)
        {
            messageBox.SetActive(false);
            delay = 5f;
        }
    }
}
