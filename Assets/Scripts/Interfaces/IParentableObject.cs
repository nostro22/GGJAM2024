using UnityEngine;

public interface IParentableObject
{
    public void SetParentObject(GameObject followTransform);

    public void RemoveParentObject();
    
}
