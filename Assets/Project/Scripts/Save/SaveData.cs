using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public float[] playerPosition;
    public float[] playerRotation;

    public List<string> clearedObjects = new List<string>();

    public List<string> notes = new List<string>();

    public List<string> activatingObjects = new List<string>();
}
