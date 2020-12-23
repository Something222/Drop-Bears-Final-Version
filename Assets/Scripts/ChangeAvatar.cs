using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAvatar : MonoBehaviour
{
    private Image Icon;
    [SerializeField] private Sprite[] avatar;
    private SquadSelection squadRef;
    // Start is called before the first frame update
    void Start()
    {
        Icon = GetComponent<Image>();
        squadRef = SquadSelection.instance;

    }

    // Update is called once per frame
    void Update()
    {
        Icon.sprite = avatar[squadRef.Squad[squadRef.Selected].GetComponent<Bears>().avatarNumber];
    }
}
