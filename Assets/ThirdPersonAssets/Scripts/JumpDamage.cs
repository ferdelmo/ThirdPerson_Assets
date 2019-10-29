using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.ThirdPerson;

public class JumpDamage : MonoBehaviour
{
    ThirdPersonCharacter tpc;
    // Start is called before the first frame update
    void Start()
    {
        tpc = GetComponent<ThirdPersonCharacter>();
        //tpc.RegisterOnJumpComprobation(CheckChomper);
    }

    void CheckChomper()
    {
        /*
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up,  out hit, 0.25f, LayerMask.GetMask("Enemy")))
        {
            hit.collider.gameObject.SendMessage("OnDeath");
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && !tpc.m_IsGrounded)
        {
            other.gameObject.transform.parent.gameObject.SendMessage("OnDeath");
            tpc.Jump();
        }
    }
}
