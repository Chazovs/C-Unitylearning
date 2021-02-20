using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    void Start()
    {
            DontDestroyOnLoad(this.gameObject);
    }
}
