public interface ISaveable
{
    void LoadData(GameData data);
    void SaveGame(ref GameData data);
}
