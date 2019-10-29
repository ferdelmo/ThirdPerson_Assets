using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.ThirdPerson;

public class JumpDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ThirdPersonCharacter tpc = GetComponent<ThirdPersonCharacter>();
        tpc.RegisterOnJumpComprobation(CheckChomper);
    }

    void CheckChomper()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up,  out hit, 0.25f, LayerMask.GetMask("Enemy")))
        {
            hit.collider.gameObject.SendMessage("OnDeath");
        }
    }
}
