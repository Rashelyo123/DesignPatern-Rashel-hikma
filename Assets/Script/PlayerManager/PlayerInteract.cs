using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float Distance = 3f;
    [SerializeField] private LayerMask Mask;
    private PlayerUI playerUI;

    void Start()
    {
        cam = GetComponent<PlayerController>().playerCamera;
        playerUI = GetComponent<PlayerUI>();

    }

    void Update()
    {

        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * Distance);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo, Distance, Mask))
        {
            if (hitinfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitinfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.PromptMessage);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.BaseInteract();
                }

            }
        }
    }
}
