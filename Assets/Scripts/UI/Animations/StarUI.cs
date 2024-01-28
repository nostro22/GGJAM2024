using UnityEngine;

public class StarUI : MonoBehaviour
{
    public float delayActive = 0;
    public bool isScaleLoop = false;
    [SerializeField] private bool ignoreTimeScale;
    public void OnEnable()
    {
        transform.localScale = Vector3.zero;
        if (ignoreTimeScale)
        {
            LeanTween.scale(gameObject, Vector3.one, 0.2f).setEaseSpring().setDelay(delayActive).setOnComplete(()=>
            {
                if (isScaleLoop)
                {
                    var newScale = new Vector3(1.1f,1.1f,1.1f);
                    LeanTween.scale(gameObject, newScale, 0.5f).setDelay(delayActive).setLoopPingPong();
                }
            }).setIgnoreTimeScale(ignoreTimeScale);
        }
        else
        {
            LeanTween.scale(gameObject, Vector3.one, 0.2f).setEaseSpring().setDelay(delayActive).setOnComplete(() =>
            {
                if (isScaleLoop)
                {
                    var newScale = new Vector3(1.1f, 1.1f, 1.1f);
                    LeanTween.scale(gameObject, newScale, 0.5f).setDelay(delayActive).setLoopPingPong();
                }
            });
        }
    }
    
    private void OnDisable()
    {
        LeanTween.cancel(gameObject);
    }  
}
