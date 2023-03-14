using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    void Start()
    {
        GetComponent<CutsceneTrigger>().StartCutscene();
    }

    bool locShown = false;

    void Update()
    {
        if(!CutsceneManager.isActive && !locShown)
        {
            locShown = true;
            GetComponent<LocationLabelTrigger>().StartShowLocation();
        }
    }
}
