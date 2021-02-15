using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    Image timerBar;
    private float maTime;
    private float timeLeft;
    public Timer()
    {
        maTime = timeLeft = Constants.totalTime;
        timerBar = GameObjects.timerBarImage.GetComponent<Image>();
    }
    public void UpdateTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maTime;
        }
        else
        {
            Settings.endType = "sadEnd";
            SceneManager.LoadScene("Menu");
        }
    }
}
