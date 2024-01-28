using System;
using UnityEngine;

    public class AnimationEventHandler : MonoBehaviour
    {
        private AnimationEvent animationEvent;
        private Action animationEventAction;
        private Action animationEventEnded;
        private string animationEventName;
        private void InvokeAnimationActionEvent()
        {
            animationEventAction?.Invoke();
        }
        
        private void InvokeAnimationEndedEvent()
        {
            animationEventEnded?.Invoke();
        }
        
        public void AddAnimationActionEvent(Action methodToCall, float time,string animationName,Animator animator)
        {
            animationEvent = new AnimationEvent();
            animationEvent.functionName = nameof(InvokeAnimationActionEvent);
            animationEvent.time = time;
            animationEventAction += methodToCall;
            foreach (var animation in animator.runtimeAnimatorController.animationClips)
            {
                if (animation.name != animationName) continue;
                animationEventName = animationName;
                animation.AddEvent(animationEvent);
            }
        }
        
        public void AddAnimationEndedEvent(Action methodToCall, float time,string animationName,Animator animator)
        {
            animationEvent = new AnimationEvent();
            animationEvent.functionName = nameof(InvokeAnimationEndedEvent);
            animationEvent.time = time;
            animationEventAction += methodToCall;
            foreach (var animation in animator.runtimeAnimatorController.animationClips)
            {
                if (animation.name != animationName) continue;
                animationEventName = animationName;
                animation.AddEvent(animationEvent);
            }
        }

        public void RemoveAllEvents(Animator animator)
        {
            foreach (var animation in animator.runtimeAnimatorController.animationClips)
            {
                if (animation.name != animationEventName) continue;
                animation.events = Array.Empty<AnimationEvent>();
                animationEventAction = default;
            }
        }
    }
