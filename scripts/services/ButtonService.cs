using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonService : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
