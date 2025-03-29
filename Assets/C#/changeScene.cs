public class changeScene : MonoBehaviour
{
    // Load scene by build index
    public void LoadMyScene(int sceneNumber)
    {
        StartCoroutine(PlayMySound(sceneNumber));
    }

    // Load scene by name
    public void LoadMyScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        StartCoroutine(PlayMySound(sceneName));
    }

    // Delayed load for int parameter
    private IEnumerator PlayMySound(int sceneNumber)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneNumber);
    }

    // Immediate load for string parameter
    private IEnumerator PlayMySound(string sceneName)
    {
        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene(sceneName);
    }
}