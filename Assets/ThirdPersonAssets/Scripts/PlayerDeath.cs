using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    private GameObject m_GameManager = null;

    private bool ignore = false;


    void Start()
    {

        // TODO 1 - Buscamos un GameObject cuyo tag sea "GameManager"
        m_GameManager = GameObject.FindGameObjectWithTag("GameManager");

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (!ignore)
            {
                m_GameManager.SendMessage("RespawnPlayer");
            }
        }
    }

    public IEnumerator Ignore()
    {
        ignore = true;
        yield return new WaitForSeconds(1);
        ignore = false;
    }
}
