using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private bool canChange = false;
    [SerializeField] private float delay = 1.5f;

    public void ChangeScene(int sceneIndex)
    {
        StartCoroutine(DelayedSceneChange(sceneIndex));
    }

    private IEnumerator DelayedSceneChange(int sceneIndex)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
