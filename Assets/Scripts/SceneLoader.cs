using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Funci�n para cargar la escena del juego
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
