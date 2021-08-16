using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    public enum PlatformState
    {
        RASIED,
        LIGHT_OFF,
        LIGHT_ON
    }

    public GameObject rasiedPlatformObj;
    public GameObject lightOffObj;
    public GameObject lightOnObj;

    public PlatformState _state = PlatformState.RASIED;
    public PlatformState state
    {
        get { return _state; }
        set { _state = value; UpdatePlatformState(); }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlatformState();
    }

    public void ToggleLight()
    {
        if(state == PlatformState.LIGHT_ON)
        {
            state = PlatformState.LIGHT_OFF;
        }
        else if(state == PlatformState.LIGHT_OFF)
        {
            state = PlatformState.LIGHT_ON;
        }
    }

    void UpdatePlatformState()
    {
        rasiedPlatformObj.SetActive(false);
        lightOffObj.SetActive(false);
        lightOnObj.SetActive(false);

        switch (_state)
        {
            case PlatformState.RASIED: rasiedPlatformObj.SetActive(true); break;
            case PlatformState.LIGHT_OFF: lightOffObj.SetActive(true); break;
            case PlatformState.LIGHT_ON: lightOnObj.SetActive(true); break;
        }
    }
}
