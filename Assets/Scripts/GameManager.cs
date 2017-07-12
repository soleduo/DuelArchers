using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public const float gravity = 10f;
    public const float windForce = 100f;
    public static float windDir = 0;
    static float windDirTurn;

    public static int turn;

    public static PhysicsData physData = new PhysicsData();

    public Character chara1;
    public Character chara2;

    public Slider wind1;
    public Slider wind2;

    public static string physDataPath = "/physData.json";

    private void Awake()
    {
        LoadPhysicsDataFromJSON();
        turn = 1;
        

        if (Instance == null)
            Instance = this;
    }
    // Use this for initialization
    void Start () {
        print(physData.throwVelocity);

        GenerateWind();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadPhysicsDataFromJSON()
    {

        string filePath = Application.dataPath + physDataPath;

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            physData = JsonUtility.FromJson<PhysicsData>(jsonData);

            Debug.Log("Data Loaded");
        }

    }

    public void GenerateWind()
    {
        float random = Random.Range(-10, 11);
        windDir = random / 10;

        if(windDir <= 0)
        {
            wind2.value = -1 * windDir;
            wind1.value = 0f;
        }
        else
        {
            wind1.value = windDir;
            wind2.value = 0f;
        }
        //print(windDir);

        float random2 = Random.Range(1, 6);
        windDirTurn = random2;

        chara1.projectile.force = chara1.projectile.CalculateForce();
        chara2.projectile.force = chara2.projectile.CalculateForce();
    }

    public static void NextTurn()
    {

        if (turn == 2)
            turn = 1;
        else
        if (turn == 1)
            turn = 2;

        if (windDirTurn >= 1)
            windDirTurn--;
        else
            Instance.GenerateWind();


        
    }

    public void OnHealthUpdate(float health)
    {
        if (CheckIfDead(health))
            GameOver();
    }

    public bool CheckIfDead(float health)
    {
        if (health <= 0)
            return true;

        else return false;
    }

    public void GameOver()
    {

    }
}

public interface GameState{


}

[System.Serializable]
public class PhysicsData
{
    public Vector2 throwVelocity;

    
}
