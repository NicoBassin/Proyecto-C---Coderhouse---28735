using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    AudioSource audioPlayer;
    [SerializeField] private AudioClip spellOneAudio;
    [SerializeField] private AudioClip spellTwoAudio;
    [SerializeField] private float volume = 0.75f;

    void Awake(){
        FindObjectOfType<PlayerAttack>().OnAttackOne += SpellOneClip;
        FindObjectOfType<PlayerAttack>().OnAttackTwo += SpellTwoClip;
    }

    void Start(){
        audioPlayer = GetComponent<AudioSource>();
    }

    private void SpellOneClip(){
        audioPlayer.PlayOneShot(spellOneAudio, volume);
    }

    private void SpellTwoClip(){
        audioPlayer.PlayOneShot(spellTwoAudio, volume);
    }
}
