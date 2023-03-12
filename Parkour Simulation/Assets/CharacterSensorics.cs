using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    MOVE_LEFT, MOVE_RIGHT,JUMP,JUMP_RIGHT,JUMP_LEFT
}
public class CharacterSensorics : MonoBehaviour
{
    public int sequenceLength;
    public ActionType[] actionSequence;

    CharacterController cc;
    float downwardsPull;
    float horizontalMovement;
    
    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        RaycastHit2D rightCheck = Physics2D.Raycast(transform.position, new Vector2(transform.position.x + 1.5f, transform.position.y));
        RaycastHit2D leftCheck = Physics2D.Raycast(transform.position, new Vector2(transform.position.x - 1.5f, transform.position.y));

        downwardsPull += Physics.gravity.y * Time.deltaTime;

        for(int i = 0; i == sequenceLength; i++)
        {
            switch (actionSequence[i])
            {
                case ActionType.MOVE_LEFT:
                    if(rightCheck == false && cc.isGrounded)
                    {
                        StartCoroutine(moveAction(-5));
                    }
                    break;
                case ActionType.MOVE_RIGHT:
                    if (leftCheck == false && cc.isGrounded)
                    {
                        StartCoroutine(moveAction(5));
                    }
                    break;
            }
            
        }

        Vector2 pM = new Vector2(horizontalMovement, downwardsPull);
        cc.Move(pM * Time.deltaTime);

        
    }

    IEnumerator moveAction(float speed)
    {
        horizontalMovement = speed;
        yield return new WaitForSeconds(1);
        horizontalMovement = 0;
    }
}
