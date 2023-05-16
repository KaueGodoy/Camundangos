using System.Collections;
using System.Collections.Generic;
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


    private List<Vector3> spawnPositionList;
    private Stage stage;

    private void Awake()
    {
        spawnPositionList = new List<Vector3>();

        foreach (Transform spawnPosition in transform.Find("SpawnPositions"))
        {
            spawnPositionList.Add(spawnPosition.position);
        }

        stage = Stage.WaitingToStart;


    }

    private void Start()
    {
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;

        slime.GetComponent<HealthSystem>().OnDamaged += BossBattle_OnDamaged;
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        StartBattle();
        colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void BossBattle_OnDamaged(object sender, System.EventArgs e)
    {
        float healthPercent = slime.currentHealth / slime.maxHealth;

        switch (stage)
        {
            default:
            case Stage.Stage_1:
                if (healthPercent >= 0.7f)
                {
                    StartNextStage();
                }
                break;


        }
        // takes damage

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
            default:
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
    }


    private void SpawnEnemy()
    {
        Vector3 spawnPosition = spawnPositionList[Random.Range(0, spawnPositionList.Count)];
        Instantiate(pfEnemy, spawnPosition, Quaternion.identity);
        //Invoke("SpawnEnemy", .5f);
    }
}
