using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void MemulaiPermainan()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {

        SceneManager.LoadScene("MainMenu");

    }
}
