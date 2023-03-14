using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLabelTrigger : MonoBehaviour
{   
    public LocationLabel locationLabel;

    public void StartShowLocation(){
        FindObjectOfType<LocationLabelManager>().ShowLocation(locationLabel);
    }
}

[System.Serializable]
public class LocationLabel {
    public Sprite locationLabelImage;
}
