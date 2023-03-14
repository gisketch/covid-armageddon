using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{

    public int progress;

    public int hp;
    public int armor;

    public int gunAmmo;
    public int flameAmmo;

    public bool hasMelee;
    public bool hasGun;
    public bool hasFlamethrower;

    public float posX;
    public float posY;
    public float posZ;

    public int scene;

    public int needsToLoad;

    [SerializeField] PlayerController player;
    [SerializeField] QuestLog questLog;


    void Start()
    {
        
        if(PlayerPrefs.GetInt("needsToLoad") == 1)
        {
            LoadProgress();
            PlayerPrefs.SetInt("needsToLoad", 0);
        }

    }

    public void SaveProgress()
    {
        hp = player.hp;
        armor = player.armor;
        gunAmmo = player.gunAmmo;
        flameAmmo = player.flameAmmo;
        hasMelee = player.hasMelee;
        hasGun = player.hasGun;
        hasFlamethrower = player.hasFlamethrower;
        posX = player.transform.position.x;
        posY = player.transform.position.y;
        posZ = player.transform.position.z;

        scene = SceneManager.GetActiveScene().buildIndex;

        progress = questLog.progress;


        PlayerPrefs.SetInt("progress", progress);

        PlayerPrefs.SetInt("hp", hp);

        PlayerPrefs.SetInt("armor", armor);

        PlayerPrefs.SetInt("gunAmmo", gunAmmo);

        PlayerPrefs.SetInt("flameAmmo", flameAmmo);

        PlayerPrefs.SetInt("hasMelee", ConvertToInt(hasMelee));

        PlayerPrefs.SetInt("hasGun", ConvertToInt(hasGun));

        PlayerPrefs.SetInt("hasFlamethrower", ConvertToInt(hasFlamethrower));

        //POSITION
        PlayerPrefs.SetFloat("posX", posX);
        PlayerPrefs.SetFloat("posY", posY);
        PlayerPrefs.SetFloat("posZ", posZ);

        PlayerPrefs.SetInt("scene", scene);

    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("needsToLoad", 1);
        if(PlayerPrefs.GetInt("scene") ==  SceneManager.GetActiveScene().buildIndex)
        {
            Debug.Log("SAME SCENE, RELOADING...");
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            Resources.UnloadUnusedAssets();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
        else if(PlayerPrefs.GetInt("scene") !=  SceneManager.GetActiveScene().buildIndex)
        {
            FindObjectOfType<LevelManager>().LoadScene(PlayerPrefs.GetInt("scene"));
            Debug.Log("NEW SCENE, RELOADING...");
        }
    }

    public void LoadProgress()
    {

        float newPosX = PlayerPrefs.GetFloat("posX");
        float newPosY = PlayerPrefs.GetFloat("posY");
        float newPosZ = PlayerPrefs.GetFloat("posZ");

        player.gameObject.transform.position = new Vector3(newPosX, newPosY, newPosZ); 
        Debug.Log("Loaded player position!");

        player.hp = PlayerPrefs.GetInt("hp");
        player.armor = PlayerPrefs.GetInt("armor");
        player.gunAmmo = PlayerPrefs.GetInt("gunAmmo");
        player.flameAmmo = PlayerPrefs.GetInt("flameAmmo");
        player.hasMelee = ConvertToBool(PlayerPrefs.GetInt("hasMelee"));
        player.hasGun = ConvertToBool(PlayerPrefs.GetInt("hasGun"));
        player.hasFlamethrower = ConvertToBool(PlayerPrefs.GetInt("hasFlamethrower"));
        Debug.Log("Loaded player stats!");

        questLog.progress = PlayerPrefs.GetInt("progress");
        Debug.Log("Loaded player progress");

        questLog.StartQuest(); //Update quest


    }

    public void LoadPlayerStats()
    {
        player.hp = PlayerPrefs.GetInt("hp");
        player.armor = PlayerPrefs.GetInt("armor");
        player.gunAmmo = PlayerPrefs.GetInt("gunAmmo");
        player.flameAmmo = PlayerPrefs.GetInt("flameAmmo");
        player.hasMelee = ConvertToBool(PlayerPrefs.GetInt("hasMelee"));
        player.hasGun = ConvertToBool(PlayerPrefs.GetInt("hasGun"));
        player.hasFlamethrower = ConvertToBool(PlayerPrefs.GetInt("hasFlamethrower"));
        Debug.Log("Loaded player stats!");
    }

    public void ClearAllStats()
    {
        PlayerPrefs.DeleteAll();
    }

    public static bool ConvertToBool(int curInt)
    {
        if(curInt == 0) return false;
        if(curInt == 1) return true;
        return false;
    }

    public static int ConvertToInt(bool curBool)
    {
        if(curBool) return 1;
        if(!curBool) return 0;
        return 0;
    }
}
