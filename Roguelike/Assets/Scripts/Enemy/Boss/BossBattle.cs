using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
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
    [SerializeField] private GameObject pfEnemy;
    [SerializeField] private Slime slime;

    public EventHandler BossOnDead;

    private Stage stage;

    private List<Vector3> spawnPositionList;
    private List<GameObject> enemySpawnList;

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
        BossOnDead += BossBattle_OnDead;


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
            CheckStage();
        }


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
            BossOnDead?.Invoke(this, EventArgs.Empty);
        }
        Debug.Log("Health: " + slime.currentHealth + " Percent: " + healthPercent);
    }

    /*
    private void BossBattle_OnDamaged(object sender, System.EventArgs e)
    {
        switch (stage)
        {
            case Stage.Stage_1:
                if ()
                {
                    StartNextStage();
                }
                break;
            case Stage.Stage_2:
                if ()
                {
                    StartNextStage();
                }
                break;
        }

    }*/

    private void BossBattle_OnDead(object sender, System.EventArgs e)
    {
        Debug.Log("Boss dead");
        DestroyAllEnemies();
        CancelInvoke("SpawnEnemy");
    }

    private void StartBattle()
    {
        Debug.Log("Start battle");
        StartNextStage();
        SpawnEnemy();
    }

    private void StartNextStage()
    {
        switch (stage)
        {
            case Stage.WaitingToStart:
                stage = Stage.Stage_1;
                break;
            case Stage.Stage_1:
                stage = Stage.Stage_2;
                break;
            case Stage.Stage_2:
                stage = Stage.Stage_3;
                break;
        }
        Debug.Log("Starting next stage: " + stage);
    }


    private void SpawnEnemy()
    {
        int amount = 0;
        foreach (GameObject enemy in enemySpawnList)
        {
            
            if (enemySpawnList.Count >= 3)
            {
                CancelInvoke("SpawnEnemy");

                //enemySpawnList.Remove(enemy);
            }
            else
            {
                Invoke("SpawnEnemy", 0.5f);

            }


        }

        Vector3 spawnPosition = spawnPositionList[UnityEngine.Random.Range(0, spawnPositionList.Count)];
        GameObject slime = Instantiate(pfEnemy, spawnPosition, Quaternion.identity);

        Invoke("SpawnEnemy", 0.5f);

        enemySpawnList.Add(slime);
        Debug.Log(enemySpawnList.Count);

        //CheckEnemyAmount();

    }

    private void CheckEnemyAmount()
    {


        /*
        if (enemySpawnList.Count > 3)
        {
            CancelInvoke("SpawnEnemy");
        }*/
    }

    private void DestroyAllEnemies()
    {
        foreach (GameObject enemy in enemySpawnList)
        {
            Destroy(enemy);
        }
    }
}
