using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningObserver : MonoBehaviour
{
    Animator anim;
    //Door opening on chomper death
    public ChomperDeath chomper;
    public CutSceneTravel travel;

    private void Start()
    {
        anim = GetComponent<Animator>();
        chomper.RegisterOnDeath(OpenDoor);
    }

    //funciton to open the door and call the cutscene travel
    void OpenDoor()
    {
        StartCoroutine(OpenDoorWithCutscene());
    }

    IEnumerator OpenDoorWithCutscene()
    {

        StartCoroutine(travel.Travel());
        yield return new WaitForSeconds(1);
        anim.SetTrigger("Open");
    }
}
