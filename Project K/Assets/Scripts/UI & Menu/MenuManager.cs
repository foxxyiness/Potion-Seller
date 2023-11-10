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
        
    };
}