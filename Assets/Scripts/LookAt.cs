using UnityEngine;

/// <summary>
/// This class allows object to look at player.
/// </summary>
public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform playerCam;

    /// <summary>
    /// This function is called later that update function
    /// </summary>
    private void LateUpdate()
    {
        transform.LookAt(playerCam);
    }
}
