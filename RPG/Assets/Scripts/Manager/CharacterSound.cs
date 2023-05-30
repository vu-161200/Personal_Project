using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Audios")]
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip hitSound;
    public AudioClip attackSound;
    public AudioClip buySellSound;
    public AudioClip healSound;
    public AudioClip atkBuffSound;
    public AudioClip blockSound;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(string type){
        switch (type)
        {
            case "jump":
                audioSource.PlayOneShot(jumpSound);
                break;
            case "land":
                audioSource.PlayOneShot(landSound);
                break;
            case "attack":
                audioSource.PlayOneShot(attackSound);
                break;
            case "hit":
                audioSource.PlayOneShot(hitSound);
                break;    
            case "buySell":
                audioSource.PlayOneShot(buySellSound);
                break; 
            case "heal":
                audioSource.PlayOneShot(healSound);
                break;
            case "atkBuff":
                audioSource.PlayOneShot(atkBuffSound);
                break; 
            case "block":
                audioSource.PlayOneShot(blockSound);
                break;
            default:
                break;
        }
    }
}
