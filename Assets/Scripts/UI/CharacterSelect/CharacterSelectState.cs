using System;

[Serializable]
public class CharacterSelectState
{
    public ulong ClientId;
    public int CharacterId;
    public bool IsLockedIn;

    public CharacterSelectState(ulong clientId, int characterId = -1, bool isLockedIn = false)
    {
        ClientId = clientId;
        CharacterId = characterId;
        IsLockedIn = isLockedIn;
    }
    
    public bool Equals(CharacterSelectState other)
    {
        return ClientId == other.ClientId &&
            CharacterId == other.CharacterId &&
            IsLockedIn == other.IsLockedIn;
    }
}
