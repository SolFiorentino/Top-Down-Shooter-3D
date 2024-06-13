using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyCounter : MonoBehaviour
{
    public static EnemyCounter Instance;

    public Text enemyCounterText;
    private int enemiesDestroyed;

    private string currentSceneName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentSceneName = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        ResetCounter();
        UpdateEnemyCounterUI();
    }

    void Update()
    {
        // Comprobar si hemos cambiado de escena
        if (currentSceneName != SceneManager.GetActiveScene().name)
        {
            // Actualizar el nombre de la escena actual
            currentSceneName = SceneManager.GetActiveScene().name;

            // Restablecer la referencia enemyCounterText
            ReassignEnemyCounterText();

            // Reiniciar el contador si volvemos a la escena del juego
            if (currentSceneName == "Game") // Asegúrate de que el nombre de tu escena de juego es correcto
            {
                ResetCounter();
                UpdateEnemyCounterUI();
            }
        }
    }

    void ReassignEnemyCounterText()
    {
        // Intentar encontrar el Text en la escena actual por nombre
        GameObject textObject = GameObject.Find("EnemyCounterText");
        if (textObject != null)
        {
            enemyCounterText = textObject.GetComponent<Text>();
        }

        if (enemyCounterText == null)
        {
            Debug.LogWarning("EnemyCounter: No se puede encontrar 'EnemyCounterText' en la escena.");
        }
    }

    void ResetCounter()
    {
        enemiesDestroyed = 0;
    }

    public void EnemyDestroyed()
    {
        enemiesDestroyed++;
        UpdateEnemyCounterUI();

        if (enemiesDestroyed >= 10)
        {
            LoadVictoryScene();
        }
    }

    void UpdateEnemyCounterUI()
    {
        if (enemyCounterText != null)
        {
            enemyCounterText.text = "Enemies Destroyed: " + enemiesDestroyed;
        }
        else
        {
            Debug.LogWarning("EnemyCounter: No se puede actualizar el UI del contador porque enemyCounterText no está asignado.");
        }
    }

    void LoadVictoryScene()
    {
        SceneManager.LoadScene("Victory");
    }
}








