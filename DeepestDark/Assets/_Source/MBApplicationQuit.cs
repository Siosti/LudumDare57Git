using UnityEngine;

public class MBApplicationQuit : MonoBehaviour
{
    public void QuitApllication()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
