using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LaunchGame()
    {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// If we don't go through CharacterSelection, we will start the game with 1 player and 3 IAs
    /// </summary>
    public void QuickPlay()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
