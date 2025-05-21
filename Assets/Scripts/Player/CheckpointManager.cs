using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private static Vector3 savedPosition;
    private GameObject player;

    private List<GameObject> currentEnemies = new List<GameObject>();
    private List<GameObject> enemyBackups = new List<GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public static void SetCheckpoint(Vector3 position)
    {
        savedPosition = position;
    }

    public void RegisterEnemy(Enemy enemy, GameObject backup)
    {
        currentEnemies.Add(enemy.gameObject);
        enemyBackups.Add(backup);
    }

    public void Respawn()
    {
        // 1. Respawn del jugador
        player.transform.position = savedPosition;
        player.GetComponent<Health>().RestartLife();

        // 2. Eliminar enemics actuals
        foreach (var enemy in currentEnemies)
        {
            if (enemy != null) Destroy(enemy);
        }
        currentEnemies.Clear(); // 💥 Netegem la llista

        // 3. Instanciar de nou els backups
        foreach (var backup in enemyBackups)
        {
            GameObject newEnemy = Instantiate(backup, backup.transform.position, backup.transform.rotation);
            newEnemy.SetActive(true);
            currentEnemies.Add(newEnemy); // 🧠 Afegim la nova instància
        }
    }
}
