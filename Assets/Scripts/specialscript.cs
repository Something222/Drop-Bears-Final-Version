using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialscript : MonoBehaviour
{
    private AudioSource Osti;
    public void playOsti()
    {
        Osti.PlayOneShot(Osti.clip);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
