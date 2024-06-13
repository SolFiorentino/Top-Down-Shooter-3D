using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Función para cargar la escena del juego
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
