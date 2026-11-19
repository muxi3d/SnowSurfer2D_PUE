using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
  public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void GoToCharacters()
    {
        SceneManager.LoadScene("Characters");
    }
  public void GoToCredits()
    {
       SceneManager.LoadScene("Credits");
    }
  
  public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
    public void GoToMainMenu()
    {
        Debug.Log("Returning to main menu...");
        SceneManager.LoadScene("Menu");
    }
    public void SelectCharacter(int characterIndex)
    {
        // Store the selected character index in PlayerPrefs
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
        GoToMainMenu();
    }
}

