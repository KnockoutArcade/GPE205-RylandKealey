using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The static instance of this class - there can only be one.
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// List of Player controllers (not the actual pawns)
    /// </summary>
    public List<PlayerController> players;

    /// <summary>
    /// List of all enemies in the scene
    /// </summary>
    public List<AIController> enemies;

    /// <summary>
    /// The map generator object. The game manager will use its functions to create the map
    /// </summary>
    public MapGenerator MapGen;

    /// <summary>
    /// The main camera in the scene
    /// </summary>
    public CameraFollow Cam;

    /// <summary>
    /// The score and Lives UI object
    /// </summary>
    public TextMeshProUGUI scoreDisplay;
    
    // Prefabs
    public GameObject playerControllerPrefab;
    public GameObject[] enemyControllerPrefab;
    public int enemyControllerPrefabIndex;
    public GameObject tankPawnPrefab;
    public GameObject[] enemyPawnPrefab;
    public int enemyPawnPrefabIndex;
    // Array of Player Spawns
    public PawnSpawnPoint[] playerSpawnTransforms;
    public int randomPlayerSpawn;
    // List of enemy spawns
    public EnemySpawnPoint[] enemySpawnTransforms;
    public int randomEnemySpawn;

    // Game States
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;
    #endregion Variables

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Allocate Memory for player list
        players = new List<PlayerController>();
        // Allocate Memory for enemy list
        enemies = new List<AIController>();
    }

    private void Update()
    {
        // Check for any player deaths
        if (players.Count > 0)
        {
            foreach (PlayerController pc in players)
            {
                if (!pc.pawnIsAlive && pc.lives > 0)
                {
                    RespawnPlayer(pc);
                }

                if (!pc.pawnIsAlive && pc.lives <= 0)
                {
                    ActivateGameOverScreen();
                }
            }
        }
    }

    private void Start()
    {
        ActivateTitleScreen();
    }

    public void SpawnPlayer()
    {
        // Spawn the Player Controller at (0, 0, 0) with no rotation
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);

        // Find all the spawnPoints in the level
        playerSpawnTransforms = FindObjectsByType<PawnSpawnPoint>(FindObjectsSortMode.None);
        randomPlayerSpawn = UnityEngine.Random.Range(0, playerSpawnTransforms.Length - 1);

        // Spawn the Pawn and connect it to that controller
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransforms[randomPlayerSpawn].transform.position, playerSpawnTransforms[randomPlayerSpawn].transform.rotation);

        // Get the PlayerController component and Pawn component
        PlayerController newController = newPlayerObj.GetComponent<PlayerController>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        // Hook them up
        newController.pawn = newPawn;
        newPawn.playerController = newController;

        // Set the player score to 0
        newController.score = 0;

        // Find the camera point component to know where the camera should be placed
        CameraFollowPoint campoint = newPawnObj.GetComponent<CameraFollowPoint>();
        // Set the Camera to follow this new pawn
        Cam.target = campoint.CameraTransform;

    }

    public void RespawnPlayer(PlayerController pc)
    {
        // Find all the spawnPoints in the level
        playerSpawnTransforms = FindObjectsByType<PawnSpawnPoint>(FindObjectsSortMode.None);
        randomPlayerSpawn = UnityEngine.Random.Range(0, playerSpawnTransforms.Length - 1);

        // Spawn the Pawn and connect it to that controller
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransforms[randomPlayerSpawn].transform.position, playerSpawnTransforms[randomPlayerSpawn].transform.rotation);

        // Get the Pawn component
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        // Hook them up
        pc.pawn = newPawn;
        newPawn.playerController = pc;

        // Reduce Lives by 1
        pc.lives--;

        // Reset pawn alive check
        pc.pawnIsAlive = true;

        // Find the camera point component to know where the camera should be placed
        CameraFollowPoint campoint = newPawnObj.GetComponent<CameraFollowPoint>();
        // Set the Camera to follow this new pawn
        Cam.target = campoint.CameraTransform;
    }

    public void SpawnEnemies(int amount)
    {
        // Find all of the enemy spawn Points in the level
        enemySpawnTransforms = FindObjectsByType<EnemySpawnPoint>(FindObjectsSortMode.None);

        // Put the array into a list to make it easier to access
        List<EnemySpawnPoint> validSpawnPoints = new List<EnemySpawnPoint>();

        foreach (EnemySpawnPoint spawnPoint in enemySpawnTransforms)
        {
            validSpawnPoints.Add(spawnPoint);
        }

        // Choose a random spawn point index
        randomEnemySpawn = UnityEngine.Random.Range(0, validSpawnPoints.Count - 1);

        int i = 0;
        // For the number of enemies we want to spawn
        while (i < amount)
        {
            // If we have run out of valid spawn points, break
            if (validSpawnPoints.Count == 0)
            {
                break;
            }

            // Choose a random AI type
            enemyControllerPrefabIndex = UnityEngine.Random.Range(0, enemyControllerPrefab.Length);

            // Spawn an enemy controller
            GameObject newEnemyController = Instantiate(enemyControllerPrefab[enemyControllerPrefabIndex], Vector3.zero, Quaternion.identity);

            // Spawn the Pawn
            GameObject newEnemyObj = Instantiate(enemyPawnPrefab[0], validSpawnPoints[randomEnemySpawn].transform.position, validSpawnPoints[randomEnemySpawn].transform.rotation);

            // remove the transform we just used
            validSpawnPoints.Remove(validSpawnPoints[randomEnemySpawn]);
            randomEnemySpawn = UnityEngine.Random.Range(0, validSpawnPoints.Count - 1);

            // Get the PlayerController component and Pawn component
            Controller newController = newEnemyController.GetComponent<Controller>();
            Pawn newPawn = newEnemyObj.GetComponent<Pawn>();

            // Hook them up
            newController.pawn = newPawn;

            i++;
        }
    }

    public void UpdateScore()
    {
        scoreDisplay.text = "Score: " + players[0].score;
    }

    #region Gameplay State Scripts

    private void DeactivateAllGameStates()
    {
        // Deactivate all game states
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);

        // Destroy the game world
        DestroyExistingMap();
    }

    public void ActivateTitleScreen()
    {
        // First, make sure all other game states are deactivated
        DeactivateAllGameStates();

        // Then, make the title screen object activate
        TitleScreenStateObject.SetActive(true);

        // TODO : Whatever needs to happen when activating the title screen
    }
    public void ActivateMainMenuScreen()
    {
        // First, make sure all other game states are deactivated
        DeactivateAllGameStates();

        // Then, make the screen object activate
        MainMenuStateObject.SetActive(true);

        // TODO : Whatever needs to happen when activating the screen
    }
    public void ActivateOptionsScreen()
    {
        // First, make sure all other game states are deactivated
        DeactivateAllGameStates();

        // Then, make the screen object activate
        OptionsScreenStateObject.SetActive(true);

        // TODO : Whatever needs to happen when activating the screen
    }
    public void ActivateCreditsScreen()
    {
        // First, make sure all other game states are deactivated
        DeactivateAllGameStates();

        // Then, make the screen object activate
        CreditsScreenStateObject.SetActive(true);

        // TODO : Whatever needs to happen when activating the screen
    }
    public void ActivateGameplayScreen()
    {
        // First, make sure all other game states are deactivated
        DeactivateAllGameStates();

        // Then, make the screen object activate
        GameplayStateObject.SetActive(true);

        // Destroy anyting in the game world that still exists
        DestroyExistingMap();

        // Generate a new map
        MapGen.GenerateMap();

        // Allocate Memory for player list
        players = new List<PlayerController>();
        // Allocate Memory for enemy list
        enemies = new List<AIController>();

        SpawnPlayer();

        SpawnEnemies(3);

        // Update Score
        UpdateScore();
    }
    public void ActivateGameOverScreen()
    {
        // First, make sure all other game states are deactivated
        DeactivateAllGameStates();

        // Then, make the screen object activate
        GameOverScreenStateObject.SetActive(true);

        // Reset Player List
        players = new List<PlayerController>();
        // Reset Enemies List
        enemies = new List<AIController>();
    }

    #endregion

    public void DestroyExistingMap()
    {
        // Destroy anything that is set to be Destroyed upon loading a new map
        DestroyOnNewmap[] objectsToDestroy = FindObjectsByType<DestroyOnNewmap>(FindObjectsSortMode.None);
        if (objectsToDestroy.Length > 0)
        {
            foreach (DestroyOnNewmap a in objectsToDestroy)
            {
                Destroy(a.gameObject);
            }
        }

        // Destroy all of the physical rooms
        MapGen.DestroyExistingMap(); 
    }
}
