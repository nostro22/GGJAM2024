using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character")]
public class Character : ScriptableObject
{
    [SerializeField] private int id = -1;
    [SerializeField] private string displayName = "New Display Name";
    [SerializeField] private GameObject introPrefab;
    [SerializeField] private GameObject gameplayPrefab;

    public int Id => id;
    public string DisplayName => displayName;
    public GameObject IntroPrefab => introPrefab;
    public GameObject GameplayPrefab => gameplayPrefab;
}
