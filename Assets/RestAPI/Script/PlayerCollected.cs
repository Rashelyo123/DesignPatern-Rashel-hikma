using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollected : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            ScoreManager.instance.AddCoin();
            Destroy(other.gameObject);
        }
    }
}
