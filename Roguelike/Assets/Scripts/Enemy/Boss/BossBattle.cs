using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public enum Stage
    {
        WaitingToStart,
        Stage_1,
        Stage_2,
        Stage_3,
    }

    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private Slime slime;
    private EnemyPatrolOnly slimePatrol;

    private GameObject pfEnemy;
    [SerializeField] private GameObject pfSlime;
    [SerializeField] private GameObject pfSkeleton;

    private Stage stage;

    private List<Vector3> spawnPositionList;
    public List<GameObject> enemySpawnList;

    public EventHandler BossOnDamaged;
    public EventHandler BossOnDead;

    private void Awake()
    {
        spawnPositionList = new List<Vector3>();
        enemySpawnList = new List<GameObject>();

        foreach (Transform spawnPosition in transform.Find("SpawnPositions"))
        {
            spawnPositionList.Add(spawnPosition.position);
        }

        stage = Stage.WaitingToStart;
    }

    private void Start()
    {
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
        slimePatrol = slime.GetComponent<EnemyPatrolOnly>();
        slimePatrol.enabled = false;

        //slime.GetComponent<HealthSystem>().OnDamaged += BossBattle_OnDamaged;
        //slime.GetComponent<HealthSystem>().OnDead += BossBattle_OnDead;
        BossOnDamaged += BossBattle_OnDamaged;
        BossOnDead += BossBattle_OnDead;

        UpdateUI();
        healthBar.gameObject.SetActive(false);

    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        StartBattle();
        colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void Update()
    {
        if (slime.currentHealth != slime.maxHealth)
        {
            BossOnDamaged?.Invoke(this, EventArgs.Empty);
            //CheckStage();
        }

        UpdateUI();


    }

    private void CheckStage()
    {
        float healthPercent = slime.currentHealth / slime.maxHealth;

        switch (stage)
        {
            case Stage.Stage_1:
                if (healthPercent <= 0.7f)
                {
                    StartNextStage();
                }
                break;
            case Stage.Stage_2:
                if (healthPercent <= 0.3f)
                {
                    StartNextStage();
                }
                break;
        }

        if (healthPercent <= 0)
        {
            slime.currentHealth = 0;
            BossOnDamaged -= BossBattle_OnDamaged;

            BossOnDead?.Invoke(this, EventArgs.Empty);
        }
        Debug.Log("Health: " + slime.currentHealth + " Percent: " + healthPercent);
    }


    private void BossBattle_OnDamaged(object sender, System.EventArgs e)
    {
        Debug.Log("Boss damaged");

        float healthPercent = slime.currentHealth / slime.maxHealth;

        switch (stage)
        {
            case Stage.Stage_1:
                if (healthPercent <= 0.7f)
                {
                    StartNextStage();
                }
                break;
            case Stage.Stage_2:
                if (healthPercent <= 0.3f)
                {
                    StartNextStage();
                }
                break;
        }

        if (healthPercent <= 0)
        {
            UpdateUI();
            slime.currentHealth = 0;
            BossOnDamaged -= BossBattle_OnDamaged;

            BossOnDead?.Invoke(this, EventArgs.Empty);
        }
        Debug.Log("Health: " + slime.currentHealth + " Percent: " + healthPercent);
    }

    private void BossBattle_OnDead(object sender, System.EventArgs e)
    {
        Debug.Log("Boss dead");
        DestroyAllEnemies();
        CancelInvoke("SpawnEnemy");
        Destroy(healthBar.gameObject);
    }

    private void StartBattle()
    {
        Debug.Log("Start battle");
        StartNextStage();
        healthBar.gameObject.SetActive(true);

    }

    private void StartNextStage()
    {
        switch (stage)
        {
            case Stage.WaitingToStart:
                stage = Stage.Stage_1;
                StageOne();
                break;
            case Stage.Stage_1:
                stage = Stage.Stage_2;
                StageTwo();
                break;
            case Stage.Stage_2:
                stage = Stage.Stage_3;
                StageThree();
                break;
        }
        Debug.Log("Starting next stage: " + stage);
    }

    [SerializeField]
    private int maxEnemyCount = 10; // Set your desired maximum enemy count here

    private void SpawnEnemy()
    {
        if (enemySpawnList.Count < maxEnemyCount)
        {
            Vector3 spawnPosition = spawnPositionList[UnityEngine.Random.Range(0, spawnPositionList.Count)];

            int randomNumber = UnityEngine.Random.Range(0, 100);

            pfEnemy = pfSlime;

            if (randomNumber < 60) pfEnemy = pfSlime;
            if (randomNumber < 20) pfEnemy = pfSkeleton;

            pfEnemy = Instantiate(pfEnemy, spawnPosition, Quaternion.identity);
            enemySpawnList.Add(pfEnemy);

        }

        Invoke("SpawnEnemy", 0.5f);
        Debug.Log("number of enemies alive: " + enemySpawnList.Count);

    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        enemySpawnList.Remove(enemy);
    }

    private void StageOne()
    {
        Debug.Log("This is the stage 1");
        SpawnEnemy();
        slimePatrol.enabled = true;

        //slime.GetComponent<EnemyPatrolOnly>().enabled = true;

    }

    private void StageTwo()
    {
        Debug.Log("This is the stage 2");
        CancelInvoke("SpawnEnemy");
        slimePatrol.patrolEnabled = false;
        //slimePatrol.enabled = false;

        //slime.GetComponent<EnemyPatrolOnly>().enabled = false;

        float upForce = 5f;
        slime.transform.position = new Vector3(slime.transform.position.x, slime.transform.position.y + upForce);

        /*
        Rigidbody2D slimeRb = slime.GetComponent<Rigidbody2D>();
        float forceMagnitude = 3f;
        float targetY = 5f;
        float currentY = slime.transform.position.y;
        float displacement = targetY - currentY;

        if (Mathf.Abs(displacement) > 0.1f)
        {
            Vector2 force = new Vector2(0f, displacement * forceMagnitude);
            slimeRb.AddForce(force);
        }
        else
        {
            slimeRb.velocity = Vector2.zero;
        }*/
    }

    private void StageThree()
    {
        Debug.Log("This is the stage 3");
    }

    private void DestroyAllEnemies()
    {
        foreach (GameObject enemy in enemySpawnList)
        {
            Destroy(enemy);
        }

        BossOnDead -= BossBattle_OnDead;

    }

    [SerializeField] private BossHealthBar healthBar;
    private string bossName = "Slime Boss";
    public void UpdateUI()
    {
        healthBar.UpdateHealthBar(slime.maxHealth, slime.currentHealth, bossName);
    }
}
