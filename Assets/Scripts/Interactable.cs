using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact(){
        //method is going to be overwritten\
        #if UNITY_EDITOR
        Debug.Log("Interacting with " + transform.name);
        #endif
    }

    public void OnFocused (Transform playerTransform){
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused(){
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void Update() {
        if (isFocus && !hasInteracted){
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius){
                Interact();
                hasInteracted = true;
            }
        }
    }

    void OnDrawGizmosSelected() {
        if(interactionTransform == null){
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
    
}
