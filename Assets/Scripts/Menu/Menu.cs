using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LaunchGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuickPlay()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
