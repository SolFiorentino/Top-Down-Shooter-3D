using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   

    public void ReturnToGame()
    {
        SceneManager.LoadScene("Menu");
    }
}

