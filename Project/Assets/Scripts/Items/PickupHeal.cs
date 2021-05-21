using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHeal : MonoBehaviour
{
    public int heal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.Instance.playerStat.GiveHealth(heal);
            PlayerManager.Instance.UpdateHealthSlider();

            Destroy(gameObject);
        }
    }
}
