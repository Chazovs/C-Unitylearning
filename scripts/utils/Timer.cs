using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    Image timerBar;
    private float maTime;
    private float timeLeft;
    public Timer(GameObject timerBarImage)
    {
        maTime = timeLeft = Constants.totalTime;
        timerBar = timerBarImage.GetComponent<Image>();
    }
    public void updateTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maTime;
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }
}
