using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FinalCutsceneDirector : MonoBehaviour
{
    [SerializeField] private AudioClip[] greenBearClips;
    [SerializeField] private AudioClip[] blueBearClips;
    [SerializeField] private AudioClip[] blackBearClips;
    [SerializeField] private AudioClip[] pinkBearClips;
    [SerializeField] private AudioClip[] redBearClips;
    [SerializeField] private AudioSource greenBearAudioS;
    [SerializeField] private AudioSource blueBearAudioS;
    [SerializeField] private AudioSource blackBearAudioS;
    [SerializeField] private AudioSource pinkBearAudioS;
    [SerializeField] private AudioSource redBearAudioS;
    [SerializeField] private Animator camAnim;
    [SerializeField] private Animator greenBearAnim;
    [SerializeField] private Animator blueBearAnim;
    [SerializeField] private Animator blackBearAnim;
    [SerializeField] private Animator pinkBearAnim;
    [SerializeField] private Animator redBearAnim;
    [SerializeField] private CinemachineStateDrivenCamera mainCamera;
    [SerializeField] private LoadScene sceneloader;
    private IEnumerator AudioFade(AudioSource bear,float time)
    {
        yield return new WaitForSeconds(time);
        while (bear.volume > .01)
        {
            bear.volume -= .0025f;
        }
    }
    private IEnumerator SwitchCams(int cam,float time)
    {
        yield return new WaitForSeconds(time);
       
        camAnim.SetInteger("Cam", cam);
    }
    private IEnumerator PlaySoundClip(AudioClip[] bearClips, AudioSource bearAudioS, int soundclip, float time)
    {
        yield return new WaitForSeconds(time);

        bearAudioS.PlayOneShot(bearClips[soundclip]);
    }
    private IEnumerator PlaySoundLoop(AudioClip[] bearClips, AudioSource bearAudioS, int soundclip, float time)
    {
        yield return new WaitForSeconds(time);
        bearAudioS.loop = true;
     
        bearAudioS.PlayOneShot(bearClips[soundclip]);

    }
    private IEnumerator PlayAnimation(Animator bear,int animClip,float time)
    {
        yield return new WaitForSeconds(time);
        bear.SetInteger("Anim", animClip);
    }
    void Start()
    {
        sceneloader.SetSceneToLoad(0);
        //i realise that i could just make a method but sometimes i wanna cut diferently and use slightly off timings and stuff
        StartCoroutine(SwitchCams(1, 1.5f));
        StartCoroutine(PlaySoundClip(greenBearClips, greenBearAudioS, 0, 1.5f));
        StartCoroutine(PlayAnimation(greenBearAnim, 1, 1.5f));
        StartCoroutine(PlayAnimation(greenBearAnim, 0, 4f));

        StartCoroutine(SwitchCams(2, 4f));
        StartCoroutine(PlayAnimation(blueBearAnim, 3, 4.3f));
        StartCoroutine(PlaySoundClip(blueBearClips, blueBearAudioS, 0, 4.25f));
        StartCoroutine(PlayAnimation(blueBearAnim, 0, 6.2f));

        StartCoroutine(SwitchCams(3, 6.2f));
        StartCoroutine(PlayAnimation(blackBearAnim, 2, 6.1f));
        StartCoroutine(PlaySoundClip(blackBearClips, blackBearAudioS, 0, 6.3f));
        StartCoroutine(PlayAnimation(blackBearAnim, 0, 8.5f));

        StartCoroutine(SwitchCams(4, 8.7f));
       StartCoroutine( PlayAnimation(pinkBearAnim, 4, 8.7f));
        StartCoroutine(PlaySoundClip(pinkBearClips, pinkBearAudioS, 0, 8.8f));
        StartCoroutine(PlayAnimation(pinkBearAnim, 0,11.9f));

        StartCoroutine(SwitchCams(6, 12.5f));

        StartCoroutine(SwitchCams(1, 15f));
        StartCoroutine(PlayAnimation(greenBearAnim, 3, 15f));
        StartCoroutine(PlaySoundClip(greenBearClips, greenBearAudioS, 1, 15f));
        StartCoroutine(PlayAnimation(greenBearAnim, 0, 26.5f));

        StartCoroutine(SwitchCams(2, 26.5f));
        StartCoroutine(PlayAnimation(blueBearAnim, 4, 25.5f));
        StartCoroutine(PlaySoundClip(blueBearClips, blueBearAudioS, 1, 25.7f));
        StartCoroutine(PlayAnimation(blueBearAnim, 0, 30.5f));

        StartCoroutine(SwitchCams(3, 30.5f));
        StartCoroutine(PlayAnimation(blackBearAnim, 2,30.5f));
        StartCoroutine(PlaySoundClip(blackBearClips, blackBearAudioS, 1, 30.5f));
        StartCoroutine(PlayAnimation(blackBearAnim, 0, 33.5f));

        StartCoroutine(SwitchCams(5, 33.5f));
        StartCoroutine(PlayAnimation(redBearAnim, 5, 33f));
        StartCoroutine(PlaySoundClip(redBearClips, redBearAudioS, 0, 34f));
        StartCoroutine(PlayAnimation(redBearAnim, 0, 36.5f));

        StartCoroutine(SwitchCams(4, 36.5f));
        StartCoroutine(PlayAnimation(pinkBearAnim, 3, 36.5f));
        StartCoroutine(PlaySoundClip(pinkBearClips, pinkBearAudioS, 1, 36.5f));

        StartCoroutine(SwitchCams(1, 40f));
        StartCoroutine(PlayAnimation(greenBearAnim, 1, 40f));
        StartCoroutine(PlaySoundClip(greenBearClips, greenBearAudioS, 2, 40f));

        StartCoroutine(SwitchCams(2, 43f));
        StartCoroutine(PlayAnimation(blueBearAnim, 3, 43f));
        StartCoroutine(PlaySoundClip(blueBearClips, blueBearAudioS, 2, 43f));

        StartCoroutine(PlayAnimation(blueBearAnim, 1, 46.5f));
        StartCoroutine(PlayAnimation(redBearAnim, 1, 46.5f));
        StartCoroutine(PlayAnimation(pinkBearAnim, 1, 46.5f));
        StartCoroutine(SwitchCams(7, 46.5f));
        StartCoroutine(PlaySoundLoop(blueBearClips, blueBearAudioS, 3,46.5f));
        StartCoroutine(PlaySoundLoop(greenBearClips, greenBearAudioS, 3, 46.5f));
        StartCoroutine(PlaySoundLoop(pinkBearClips, pinkBearAudioS, 2, 46.5f));
        StartCoroutine(PlaySoundLoop(redBearClips, redBearAudioS, 1,46.5f));

        StartCoroutine(PlaySoundClip(blackBearClips, blackBearAudioS, 2, 49f));
        StartCoroutine(PlayAnimation(blackBearAnim, 3, 49f));
        StartCoroutine(AudioFade(blueBearAudioS, 46.5f));
        StartCoroutine(AudioFade(redBearAudioS, 46.5f));
        StartCoroutine(AudioFade(pinkBearAudioS, 46.5f));
        StartCoroutine(AudioFade(greenBearAudioS, 46.5f));
        StartCoroutine(SwitchCams(8, 47f));
        sceneloader.Invoke("EnterLoadingScreen", 51f);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
