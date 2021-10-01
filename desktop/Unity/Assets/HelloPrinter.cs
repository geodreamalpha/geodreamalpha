using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using LoginSystemComponent;
using FirebaseConnectorComponent;
using DataManagerComponent;
using CombatSystemComponent;
using PlayerMovementComponent;
using TerrainGeneratorComponent;
using EnemySpawnerComponent;

public class HelloPrinter : MonoBehaviour
{
    Text text;
    public LoginSystem loginSystem;
    public FirebaseConnector firebaseConnector;
    public DataManager dataManager;
    public CombatSystem combatSystem;
    public PlayerMovement playerMovement;
    public TerrainGenerator terrainGenerator;
    public EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        text.text = terrainGenerator.Hello() + "\n" + enemySpawner.Hello() + "\n" + loginSystem.Hello() + "\n" + firebaseConnector.Hello() + "\n" + dataManager.Hello() + "\n" + combatSystem.Hello() + "\n" + playerMovement.Hello();

        Debug.Log(terrainGenerator.Hello());
        Debug.Log(enemySpawner.Hello());
        Debug.Log(loginSystem.Hello());
        Debug.Log(firebaseConnector.Hello());
        Debug.Log(dataManager.Hello());
        Debug.Log(combatSystem.Hello());
        Debug.Log(playerMovement.Hello());
    }//

    // Update is called once per frame
    void Update()
    {
        
    }//
}//
