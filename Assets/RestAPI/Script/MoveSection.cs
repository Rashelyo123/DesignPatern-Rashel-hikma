using UnityEngine;

public class MoveSection : MonoBehaviour
{
    bool isMove = true;

    void Update()
    {
        if (!isMove) return;


        float currentSpeed = DifficultyManager.Instance.baseSpeed;


        transform.position += new Vector3(0, 0, currentSpeed) * Time.deltaTime;
    }

    public void NotMove() => isMove = false;
    public void Move() => isMove = true;
}
