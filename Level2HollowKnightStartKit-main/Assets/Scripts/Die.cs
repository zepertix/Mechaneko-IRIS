using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerRespawner : MonoBehaviour
{
    public string respawnChildName = "RespawnPoint";

    private void OnDestroy()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}