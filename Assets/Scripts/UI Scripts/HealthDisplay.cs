using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private Image Health;
    [SerializeField] private Bears Hp;
    private Color initialColor = new Color();
    private bool onlyOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        if(Hp == null)
        {
            Hp = GetComponentInParent<Bears>();
        }
        Health = GetComponent<Image>();
        initialColor = Health.color;
    }

    // Update is called once per frame
    void Update()
    {
        Health.fillAmount = (float)Hp.Hp / Hp.TotalHP;
        if (Health.fillAmount >= 0.5f)
        {
            Health.color = initialColor;
        }
        else if (Health.fillAmount < 0.5f && Health.fillAmount >= 0.25f)
        {
            Health.color = Color.yellow;
        }
        else if (Health.fillAmount < 0.25f)
        {
            if (onlyOnce)
            {
                Health.color = Color.red;
                StartCoroutine(FlashHealth(Health));
            }
        }
    }
    

    IEnumerator FlashHealth(Image Health)
    {
        onlyOnce = false;
        Health.color = Color.white;
        yield return new WaitForSeconds(0.5f);
        Health.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        onlyOnce = true;
        

    }
}
