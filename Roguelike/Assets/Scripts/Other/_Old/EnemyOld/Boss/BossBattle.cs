using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public event EventHandler OnBossDeath;

    public enum Stage
    {
        WaitingToStart,
        Stage_1,
        Stage_2,
        Stage_3,
    }

    [Header("Initial Stage")]
    [SerializeField] private ColliderTrigger _colliderTrigger;
    [SerializeField] private Slime _slime;
    [SerializeField] private NewPlayerController _player;
    [SerializeField] private Transform _bossEntranceDoor;

    // idea
    // count time when battle starts and let the skeleton hp be the slime remaining hp * time passed since battle
    // skeletonBoss.maxHealth = slime.currentHealth * timer;

    private Stage _stage;

    public EventHandler BossOnDamaged;
    public EventHandler BossOnDead;

    private void Awake()
    {
        spawnPositionList = new List<Vector3>();
        enemySpawnList = new List<GameObject>();

        foreach (Transform spawnPosition in _slime.transform.Find("SpawnPositions"))
        {
            spawnPositionList.Add(spawnPosition.position);
        }

        _stage = Stage.WaitingToStart;
    }

    private void Start()
    {
        _colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;

        slimePatrol = _slime.GetComponent<EnemyPatrolAttack>();
        slimePatrol.enabled = false;

        //slime.GetComponent<HealthSystem>().OnDamaged += BossBattle_OnDamaged;
        //slime.GetComponent<HealthSystem>().OnDead += BossBattle_OnDead;
        BossOnDamaged += BossBattle_OnDamaged;
        BossOnDead += BossBattle_OnDead;

        healthBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_slime.IsDamaged())
        {
            BossOnDamaged?.Invoke(this, EventArgs.Empty);
        }

        if (_stage == Stage.Stage_1)
        {
            if (!!_slime.isDead())
            {
                for (int i = 0; i < spawnPositionList.Count; i++)
                {
                    Transform spawnPosition = _slime.transform.Find("SpawnPositions").GetChild(i);
                    spawnPositionList[i] = spawnPosition.position;
                }
            }
        }

        // NEEDS TO BE REMOVED FROM UPDATE

        if (_stage == Stage.Stage_2)
        {
            TeleportEnemy();
        }

        if (_stage == Stage.Stage_3)
        {
            //SpawnSkeleton();
            if (skeletonBoss.CurrentHealth <= 0)
            {
                //EndBattle();
            }
        }


    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        StartBattle();
        Animator _bossEntranceDoorAnimator = _bossEntranceDoor.GetComponent<Animator>();
        _bossEntranceDoorAnimator.Play("ClosingDoorAnimation");
        AudioManager.Instance.PlaySound("OnDoorClosing");
        _colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void BossBattle_OnDamaged(object sender, System.EventArgs e)
    {
        Debug.Log("Boss damaged");

        float healthPercent = _slime.CurrentHealth / _slime.MaxHealth;

        switch (_stage)
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
            _slime.CurrentHealth = 0;
            BossOnDamaged -= BossBattle_OnDamaged;
            BossOnDead?.Invoke(this, EventArgs.Empty);
            EndBattle();
        }

        if (healthBar != null) UpdateUI();

        Debug.Log("Health: " + _slime.CurrentHealth + " Percent: " + healthPercent);
    }

    private void BossBattle_OnDead(object sender, System.EventArgs e)
    {
        Debug.Log("Boss dead");
        CancelInvoke();
        StopAllCoroutines();
        BossOnDead -= BossBattle_OnDead;
        Destroy(healthBar.gameObject);
    }

    private void StartBattle()
    {
        Debug.Log("Start battle");
        StartNextStage();
        if (healthBar != null) UpdateUI();
        healthBar.gameObject.SetActive(true);

    }

    private void StartNextStage()
    {
        switch (_stage)
        {
            case Stage.WaitingToStart:
                _stage = Stage.Stage_1;
                StageOne();
                break;
            case Stage.Stage_1:
                _stage = Stage.Stage_2;
                StageTwo();
                break;
            case Stage.Stage_2:
                _stage = Stage.Stage_3;
                StageThree();
                break;
        }
        Debug.Log("Starting next stage: " + _stage);
    }

    #region Stage 01

    /// <summary>
    /// patrol between two points (movement, flip)
    /// spawns enemies (instantiate prefabs)
    /// </summary>

    [Header("Stage 01")]
    [SerializeField] private GameObject pfSlime;
    [SerializeField] private GameObject pfSkeleton;
    [SerializeField] private Transform _bossLockCollider;
    [SerializeField] private int maxEnemyCount = 10; // Set your desired maximum enemy count here

    private EnemyPatrolAttack slimePatrol;
    private GameObject pfEnemy;

    private List<Vector3> spawnPositionList;

    public List<GameObject> enemySpawnList;
    private void StageOne()
    {
        Debug.Log("This is the stage 1");
        EnableBossLockCollider();
        SpawnEnemy();
        slimePatrol.enabled = true;
    }

    private void EnableBossLockCollider()
    {
        _bossLockCollider.gameObject.SetActive(true);
    }

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

    #endregion

    #region Stage 02

    [Header("Stage 02")]
    [SerializeField] private float yOffset = 6.5f;

    [SerializeField] private float fallYOffset = 1.4f;
    [SerializeField] private float fallingTime = 0.5f;
    [SerializeField] private float fallingCooldown = 1f;

    [SerializeField] private bool hasTeleported = false;

    private void StageTwo()
    {
        // exiting stage one
        Debug.Log("This is the stage 2");
        CancelInvoke("SpawnEnemy");
        slimePatrol.patrolEnabled = false;
        DestroyAllEnemies();
        //slime.GetComponent<EnemyPatrolOnly>().enabled = false;

        // starting stage two
    }

    private void TeleportEnemy()
    {
        if (_slime == null) return;

        if (!hasTeleported)
        {
            StartCoroutine(FallDown());
        }
    }

    private IEnumerator FallDown()
    {

        // resets condition
        hasTeleported = true;

        float elapsedTime = 0f;
        float totalDuration = fallingTime;

        // falls down at the speed based on the fallingTime value
        while (elapsedTime < totalDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / totalDuration); // Normalize the time

            // gets slime current position and place where should fall current position right before falling
            Vector3 startPosition = _slime.transform.position;
            Vector3 fallPosition = new Vector3(_player.transform.position.x, _player.transform.position.y + fallYOffset);

            // falls down
            _slime.transform.position = Vector3.Lerp(startPosition, fallPosition, t);
            yield return null;
        }

        // disintegration animation

        // enters cooldown
        yield return new WaitForSeconds(fallingTime + fallingCooldown);
        // gets current position during this frame
        Vector3 targetPosition = new Vector3(_player.transform.position.x, _player.transform.position.y + yOffset);
        // teleports above the player
        if (_slime != null)
        {
            _slime.transform.position = targetPosition;
        }
        hasTeleported = false;
    }


    private IEnumerator FallDown2()
    {
        _slime.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + yOffset);
        hasTeleported = true;
        yield return new WaitForSeconds(fallingTime);

        _slime.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y);
        yield return new WaitForSeconds(fallingTime);
        hasTeleported = false;
    }

    private IEnumerator CopyPlayerMovement()
    {
        Vector3 startPosition = _slime.transform.position;
        Vector3 targetPosition = new Vector3(_player.transform.position.x, _player.transform.position.y + yOffset);

        float elapsedTime = 0f;
        float totalDuration = fallingTime;

        while (elapsedTime < totalDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / totalDuration); // Normalize the time

            _slime.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        _slime.transform.position = targetPosition;
        yield return new WaitForSeconds(fallingTime);
        hasTeleported = false;
    }

    #endregion

    #region Stage 03

    /// <summary>
    /// slime dies (setActive(false))
    /// spanws skeleton with the slime current hp (Destroy(slime.gameobject)
    /// </summary>

    [Header("Stage 03")]
    [SerializeField] private GameObject pfskeletonBoss;
    private GameObject pfskeletonBossInstance;

    [SerializeField] private Skeleton skeletonBoss;
    private float skeletonHealthFromSlime;
    private float skeletonMaxHealth;
    [SerializeField] private Transform _platforms;

    private void StageThree()
    {
        Debug.Log("This is the stage 3");
        skeletonHealthFromSlime = _slime.CurrentHealth;
        skeletonMaxHealth = skeletonBoss.MaxHealth;

        MoveSlime();
        SpawnSkeleton();
        EnablePlatforms();

    }

    private void MoveSlime()
    {
        if (_slime == null) return;

        _slime.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 15f, _player.transform.position.z);
    }

    private void SpawnSkeleton()
    {
        Vector3 skeletonBossSpawnPosition = new Vector3(_slime.transform.position.x, _slime.transform.position.y);
        pfskeletonBossInstance = Instantiate(pfskeletonBoss, skeletonBossSpawnPosition, Quaternion.identity);

        skeletonBoss = pfskeletonBossInstance.GetComponent<Skeleton>();
        skeletonBoss.MaxHealth = skeletonHealthFromSlime;
        //skeletonBoss.currentHealth = skeletonHealthFromSlime;


        Debug.Log("This is the skeleton health value from slime: " + skeletonHealthFromSlime +
                    " This is the skeleton health value changed: " + skeletonBoss.CurrentHealth +
                    " Old max health from prefab: " + skeletonMaxHealth);

    }

    private void EnablePlatforms()
    {
        if (_platforms == null) return;

        _platforms.gameObject.SetActive(true);
    }

    #endregion

    #region End Battle

    private void EndBattle()
    {
        OnBossDeath?.Invoke(this, EventArgs.Empty);

        Destroy(pfskeletonBossInstance.gameObject);
        Destroy(_slime.gameObject);
    }

    private void DestroyAllEnemies()
    {
        foreach (GameObject enemy in enemySpawnList)
        {
            Destroy(enemy);
        }

    }

    #endregion

    #region UI

    /// <summary>
    /// Updates UI Elements
    /// </summary>

    [Header("UI")]
    [SerializeField] private BossHealthBar healthBar;

    private string bossName = "Slime Boss";
    //private string bossName2 = "Skeleton Boss";

    public void UpdateUI()
    {
        if (!_slime.isDead())
        {
            healthBar.UpdateHealthBar(_slime.MaxHealth, _slime.CurrentHealth, bossName);
        }

        //if (stage == Stage.Stage_3)
        //{
        //    healthBar.UpdateHealthBar((int)skeletonBoss.maxHealth, (int)skeletonBoss.currentHealth, bossName2);
        //}

        if (skeletonBoss.isDead())
        {
            //healthBar.gameObject.SetActive(false);
        }
    }

    #endregion
}