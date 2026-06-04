using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float moveSpeed;


    private void Update()
    {
        Vector2 inputVec = Vector2.zero;

        if(Keyboard.current != null)
        {
            float h = 0;
            float v = 0;


            if (Keyboard.current.aKey.isPressed) h = -1;
            if (Keyboard.current.dKey.isPressed) h =  1;
            if (Keyboard.current.sKey.isPressed) v = -1;
            if (Keyboard.current.wKey.isPressed) v =  1;

            
            inputVec = new Vector2(h, v);


        }
        
        Vector3 dir = new Vector3(inputVec.x ,0, inputVec.y).normalized;

        if(dir.magnitude > 0)
        {
            transform.position += dir * moveSpeed * Time.deltaTime;

        }


    }


}
