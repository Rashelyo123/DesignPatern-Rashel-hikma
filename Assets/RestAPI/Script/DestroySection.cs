using UnityEngine;

public class DestroySection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player")) return;


        if (other.CompareTag("Track") || other.CompareTag("Obstacle") || other.CompareTag("Coin"))
        {
            Transform target = other.transform;


            if (target.parent != null && target.parent.CompareTag("Track"))
            {
                Destroy(target.parent.gameObject);
            }
            else
            {
                Destroy(target.gameObject);
            }
        }
    }
}
