using ScriptableObjects;
public interface IMovable
{
        public MovementType MovementType { get; set; }
        public MovableOverrideSO movableOverrideSO { get; set; }
        public float overrideCurrentSpeed{ get; set; }
        public PlayerController playerOverrideMovementController { get; set; }
}
