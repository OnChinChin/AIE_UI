using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public enum ActionType
    {
        MOVE_FORWARD,
        ROTATE_LEFT,
        ROTATE_RIGHT,
        TOGGLE_LIGHT,
    }

    public float lerpSpeed = 1.0f;

    Vector3 currentPostion;
    Vector3 targetPostion;

    float currentYRotation;
    float targetYRotation;

    float lerptime = 0.0f;

    public List<ActionType> actions = new List<ActionType>();
    public int currentActionIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentPostion = transform.position;
        targetPostion = transform.position;

        currentYRotation = transform.rotation.eulerAngles.y;
        targetYRotation = transform.rotation.eulerAngles.y;

        SnapToPlatform();

        actions.Clear();
        actions.Add(ActionType.MOVE_FORWARD);
        actions.Add(ActionType.ROTATE_LEFT);
        actions.Add(ActionType.MOVE_FORWARD);
        actions.Add(ActionType.ROTATE_RIGHT);
        actions.Add(ActionType.MOVE_FORWARD);
        actions.Add(ActionType.TOGGLE_LIGHT);
    }

    // Update is called once per frame
    void Update()
    {
        ActionController();
        TempKeyboardController();

        lerptime += Time.deltaTime * lerpSpeed;
        if(lerptime > 1.0f)
        {
            lerptime = 1.0f;
            currentPostion = targetPostion;
            currentYRotation = targetYRotation;
            SnapToPlatform();
        }

        transform.position = Vector3.Lerp(currentPostion, targetPostion, lerptime);
        transform.rotation = Quaternion.Euler(0, Mathf.Lerp(currentYRotation, targetYRotation, lerptime), 0);
    }

    public void MoveForward()
    {
        targetPostion = currentPostion + transform.forward;
        lerptime = 0.0f;
    }

    public void RotatRight()
    {
        targetYRotation += 90.0f;
        lerptime = 0.0f;
    }
    public void RotateLeft()
    {
        targetYRotation -= 90.0f;
        lerptime = 0.0f;
    }


    void TempKeyboardController()
    {
        if(CanPreformAction() == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && CanPreformAction())
        {
            MoveForward();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RotatRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotateLeft();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLight();
        }
    }

    void ActionController()
    {

        if(CanPreformAction() == false)
        {
            return;
        }

        if (currentActionIndex >= actions.Count)
        {
            return;
        }

        var action = actions[currentActionIndex];
        currentActionIndex += 1;

        switch (action)
        {
            case ActionType.MOVE_FORWARD: MoveForward(); break;
            case ActionType.ROTATE_LEFT: RotateLeft(); break;
            case ActionType.ROTATE_RIGHT: RotatRight(); break;
            case ActionType.TOGGLE_LIGHT: ToggleLight(); break;
        }
        
    }

    bool CanPreformAction()
    {
        return lerptime == 1.0f;
    }

    void SnapToPlatform()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            targetPostion = hit.point + (Vector3.up * 0.01f);
        }
    }

    void ToggleLight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            var platform = hit.collider.gameObject.GetComponent<PlatformController>();
            if (platform)
            {
                platform.ToggleLight();
            }
        }
    }

}
