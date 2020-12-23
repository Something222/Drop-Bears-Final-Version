using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unhide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("UnHide", 1f);
    }

    private void UnHide()
    {
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
