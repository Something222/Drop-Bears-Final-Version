using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgNumbers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sepuku());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z);
    }
    private IEnumerator Sepuku()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
