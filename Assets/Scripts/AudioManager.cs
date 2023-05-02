using UnityEngine;

/// <summary>
/// This class manages the game music.
/// </summary>
public class AudioManager : MonoBehaviour
{
    // make this class a singleton
    public static AudioManager audioManager;

    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioClip background;

    /// <summary>
    /// This function runs before the game starts.
    /// </summary>
    private void Awake()
    {
        // check for duplicates
        if (audioManager != null && audioManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// This function runs at start of game.
    /// </summary>
    private void Start()
    {
        musicPlayer.clip = background;
        musicPlayer.Play();
    }
}