using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewTimerSO",menuName = "ScriptableObjects/TimerSO",order = 0)]
    public class TimerSO : ScriptableObject
    {
        public float CountdownToStartTimer;
        public float TotalPlayingTimer;
        public float SafePlayingTimer;
        public string TextToAppearInStartTime;
    }
}