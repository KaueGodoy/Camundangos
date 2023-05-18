using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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
        SpawnEnemy();
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

            if (randomNumber < 60)
            {
                pfEnemy = pfSlime;
            }
            else if (randomNumber < 20)
            {
                pfEnemy = pfSkeleton;
            }

            pfEnemy = Instantiate(pfEnemy, spawnPosition, Quaternion.identity);
            enemySpawnList.Add(pfEnemy);

            Invoke("SpawnEnemy", 0.5f);

        }
    }


    private void SpawnEnemy2()
    {
        Vector3 spawnPosition = spawnPositionList[UnityEngine.Random.Range(0, spawnPositionList.Count)];

        int randomNumber = UnityEngine.Random.Range(0, 100);

        pfEnemy = pfSlime;

        if (randomNumber < 60) pfEnemy = pfSlime;
        if (randomNumber < 20) pfEnemy = pfSkeleton;

        pfEnemy = Instantiate(pfEnemy, spawnPosition, Quaternion.identity);

        Invoke("SpawnEnemy", 0.5f);

        //pfSlime = Instantiate(pfSlime, spawnPosition, Quaternion.identity);
        //pfSkeleton = Instantiate(pfSkeleton, spawnPosition, Quaternion.identity);

        enemySpawnList.Add(pfEnemy);
        //Debug.Log(enemySpawnList.Count);
    }

    private void StageOne()
    {
        Debug.Log("This is the stage 1");
    }

    private void StageTwo()
    {
        Debug.Log("This is the stage 2");
    }

    private void StageThree()
    {
        Debug.Log("This is the stage 3");
    }

    private void CheckEnemyAmount()
    {
        //int amount = 0;
        foreach (GameObject enemy in enemySpawnList)
        {

            if (enemySpawnList.Count <= 3)
            {
                Invoke("SpawnEnemy", 0.5f);

                //enemySpawnList.Remove(enemy);
            }
            else
            {
                CancelInvoke("SpawnEnemy");
            }


        }
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
