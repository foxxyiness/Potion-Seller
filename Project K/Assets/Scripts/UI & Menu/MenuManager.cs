using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI___Menu
{
    public class MenuManager : MonoBehaviour
    {
        
        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("QU/IT");

        }

        public void Death()
        {
            SceneManager.LoadScene("FailScene");
        }

        public void DeathLoad()
        {
            SceneManager.LoadSceneAsync("FailScene");
        }
        public void GameStart()
        {
            SceneManager.LoadScene("MainScene");
        }

        public void TutorialStart()
        {
            SceneManager.LoadScene("Tutorial");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

    };
}