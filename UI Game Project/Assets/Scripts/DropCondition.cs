using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropCondition
{
    public abstract bool Check(DraggableComponent draggable);
}
