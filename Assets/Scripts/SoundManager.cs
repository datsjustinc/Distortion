using UnityEngine;
using Player;

/// <summary>
/// This class manages miscellaneous sound effects.
/// </summary>
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip damaged;
    [SerializeField] private PlayerMovement playerMovement;

    private void Update()
    {
        if (playerMovement.IsHit)
        {
            Debug.Log("Playing sound");
            audio.clip = damaged;
            audio.Play();
        }
    }
}
