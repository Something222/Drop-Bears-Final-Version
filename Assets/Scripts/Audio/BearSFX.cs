using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSFX : MonoBehaviour
{
    // Start is called before the first frame update
    public static BearSFX instance = null;
    //Ya its alotta of arrays but i feel like its more organized this way instead of memorizing the ranges within
    //the arrays for different types of clips just call a different array 
    //Sound clip Notes just put the clips in the appropriate array and bang the bears will have it 
    //if you add arrays follow the naming convention of the bears enumcolor then type of clip 
    [Header("GreenBearClips")]
    [SerializeField] private AudioClip[] greenAttackClips;
        [SerializeField] private AudioClip[] greenIdleClips;
        [SerializeField] private AudioClip[] greenHurtClips;
    [SerializeField] private AudioClip[] greenMoveClips;
    [SerializeField] private AudioClip[] greenAbility1Clips;
    [SerializeField] private AudioClip[] greenAbility1AltClips;
    [SerializeField] private AudioClip[] greenAbility2Clips;
    [SerializeField] private AudioClip[] greenAbility2AltClips;

    [Header("BlueBearClips")]
    [SerializeField] private AudioClip[] blueAttackClips;
        [SerializeField] private AudioClip[] blueIdleClips;
        [SerializeField] private AudioClip[] blueHurtClips;
    [SerializeField] private AudioClip[] blueMoveClips;
    [SerializeField] private AudioClip[] blueAbility1Clips;
    [SerializeField] private AudioClip[] blueAbility1AltClips;
    [SerializeField] private AudioClip[] blueAbility2Clips;
    [SerializeField] private AudioClip[] blueAbility2AltClips;

    [Header("PinkBearClips")]
    [SerializeField] private AudioClip[] pinkAttackClips;
        [SerializeField] private AudioClip[] pinkIdleClips;
        [SerializeField] private AudioClip[] pinkHurtClips;
    [SerializeField] private AudioClip[] pinkMoveClips;
    [SerializeField] private AudioClip[] pinkAbility1Clips;
    [SerializeField] private AudioClip[] pinkAbility1AltClips;
    [SerializeField] private AudioClip[] pinkAbility2Clips;
    [SerializeField] private AudioClip[] pinkAbility2AltClips;

    [Header("BlackBearClips")]
    [SerializeField] private AudioClip[] blackAttackClips;
        [SerializeField] private AudioClip[] blackIdleClips;
        [SerializeField] private AudioClip[] blackHurtClips;
    [SerializeField] private AudioClip[] blackMoveClips;
    [SerializeField] private AudioClip[] blackAbility1Clips;
    [SerializeField] private AudioClip[] blackAbility1AltClips;
    [SerializeField] private AudioClip[] blackAbility2Clips;
    [SerializeField] private AudioClip[] blackAbility2AltClips;

    [Header("RedBearClips")]
    [SerializeField] private AudioClip[] redAttackClips;
        [SerializeField] private AudioClip[] redIdleClips;
        [SerializeField] private AudioClip[] redHurtClips;
    [SerializeField] private AudioClip[] redMoveClips;
    [SerializeField] private AudioClip[] redAbility1Clips;
    [SerializeField] private AudioClip[] redAbility1AltClips;
    [SerializeField] private AudioClip[] redAbility2Clips;
    [SerializeField] private AudioClip[] redAbility2AltClips;



    #region properties

    public AudioClip[] GreenAttackClips { get => greenAttackClips;  }
    public AudioClip[] GreenIdleClips { get => greenIdleClips; }
    public AudioClip[] GreenHurtClips { get => greenHurtClips;  }
    public AudioClip[] BlueAttackClips { get => blueAttackClips;  }
    public AudioClip[] BlueIdleClips { get => blueIdleClips; }
    public AudioClip[] BlueHurtClips { get => blueHurtClips;}
    public AudioClip[] PinkAttackClips { get => pinkAttackClips; }
    public AudioClip[] PinkIdleClips { get => pinkIdleClips; }
    public AudioClip[] PinkHurtClips { get => pinkHurtClips; }
    public AudioClip[] BlackAttackClips { get => blackAttackClips;}
    public AudioClip[] BlackIdleClips { get => blackIdleClips;  }
    public AudioClip[] BlackHurtClips { get => blackHurtClips; }
    public AudioClip[] RedAttackClips { get => redAttackClips;}
    public AudioClip[] RedIdleClips { get => redIdleClips; }
    public AudioClip[] RedHurtClips { get => redHurtClips; }
    public AudioClip[] GreenAbility1Clips { get => greenAbility1Clips; set => greenAbility1Clips = value; }
    public AudioClip[] GreenAbility2Clips { get => greenAbility2Clips; set => greenAbility2Clips = value; }
    public AudioClip[] BlueAbility1Clips { get => blueAbility1Clips; set => blueAbility1Clips = value; }
    public AudioClip[] BlueAbility2Clips { get => blueAbility2Clips; set => blueAbility2Clips = value; }
    public AudioClip[] PinkAbility1Clips { get => pinkAbility1Clips; set => pinkAbility1Clips = value; }
    public AudioClip[] PinkAbility2Clips { get => pinkAbility2Clips; set => pinkAbility2Clips = value; }
    public AudioClip[] BlackAbility1Clips { get => blackAbility1Clips; set => blackAbility1Clips = value; }
    public AudioClip[] BlackAbility2Clips { get => blackAbility2Clips; set => blackAbility2Clips = value; }
    public AudioClip[] RedAbility1Clips { get => redAbility1Clips; set => redAbility1Clips = value; }
    public AudioClip[] RedAbility2Clips { get => redAbility2Clips; set => redAbility2Clips = value; }
    public AudioClip[] RedMoveClips { get => redMoveClips; set => redMoveClips = value; }
    public AudioClip[] BlackMoveClips { get => blackMoveClips; set => blackMoveClips = value; }
    public AudioClip[] PinkMoveClips { get => pinkMoveClips; set => pinkMoveClips = value; }
    public AudioClip[] BlueMoveClips { get => blueMoveClips; set => blueMoveClips = value; }
    public AudioClip[] GreenMoveClips { get => greenMoveClips; set => greenMoveClips = value; }
    public AudioClip[] RedAbility2AltClips { get => redAbility2AltClips; set => redAbility2AltClips = value; }
    public AudioClip[] RedAbility1AltClips { get => redAbility1AltClips; set => redAbility1AltClips = value; }
    public AudioClip[] BlackAbility2AltClips { get => blackAbility2AltClips; set => blackAbility2AltClips = value; }
    public AudioClip[] BlackAbility1AltClips { get => blackAbility1AltClips; set => blackAbility1AltClips = value; }
    public AudioClip[] PinkAbility2AltClips { get => pinkAbility2AltClips; set => pinkAbility2AltClips = value; }
    public AudioClip[] PinkAbility1AltClips { get => pinkAbility1AltClips; set => pinkAbility1AltClips = value; }
    public AudioClip[] BlueAbility2AltClips { get => blueAbility2AltClips; set => blueAbility2AltClips = value; }
    public AudioClip[] BlueAbility1AltClips { get => blueAbility1AltClips; set => blueAbility1AltClips = value; }
    public AudioClip[] GreenAbility2AltClips { get => greenAbility2AltClips; set => greenAbility2AltClips = value; }
    public AudioClip[] GreenAbility1AltClips { get => greenAbility1AltClips; set => greenAbility1AltClips = value; }
    #endregion properties
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    

    }
  
}
