using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [Header("Footstep")]
    public GameObject footstepSand;
    public GameObject footstepGrass;
    public GameObject footstepConc;
    public Transform footstepLoc;

    public FootstepInstantiator groundHitbox;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            groundHitbox = col.gameObject.GetComponent<FootstepInstantiator>();
        }
    }

    void Footstep()
    {
        
        if(groundHitbox == null) return;

        switch (groundHitbox.footsteps)
        {
            case FootstepInstantiator.Footsteps.Sand:
                FindObjectOfType<AudioManager>().PlayOneShotRandom("fsand");
                Instantiate(footstepSand, footstepLoc.position, footstepLoc.rotation);
                break;

            case FootstepInstantiator.Footsteps.Grass:
                FindObjectOfType<AudioManager>().PlayOneShotRandom("fgrass");
                Instantiate(footstepGrass, footstepLoc.position, footstepLoc.rotation);
                break;

            case FootstepInstantiator.Footsteps.Concrete:
                FindObjectOfType<AudioManager>().PlayOneShotRandom("fconc");
                Instantiate(footstepConc, footstepLoc.position, footstepLoc.rotation);
                break;


        }
        
    }
}
