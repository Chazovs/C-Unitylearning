using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    void Start()
    {
            DontDestroyOnLoad(this.gameObject);
    }
}
