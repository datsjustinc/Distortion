using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the level select scene transition.
/// </summary>
public class LevelSelect : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    public void OpenScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}