using UnityEngine;

public interface IThrowable
{
    public bool InThrow { get; set; }

    public GameObject PlayerThrow { get; set; }
    
    public void OnThrow(GameObject playerThrow);
    
}
