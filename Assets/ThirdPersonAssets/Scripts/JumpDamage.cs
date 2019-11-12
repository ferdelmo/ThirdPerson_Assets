using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.ThirdPerson;

public class JumpDamage : MonoBehaviour
{
    ThirdPersonCharacter tpc;
    PlayerDeath pd;

    // Start is called before the first frame update
    void Start()
    {
        tpc = GetComponent<ThirdPersonCharacter>();
        pd = GetComponent<PlayerDeath>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && !tpc.m_IsGrounded)
        {
            StartCoroutine(pd.Ignore());
            other.gameObject.transform.parent.SendMessage("OnDeath");
            tpc.Jump();
            
        }
    }
}
