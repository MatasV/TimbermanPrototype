using UnityEngine;

[CreateAssetMenu(menuName = "Data/Appearance Data")]
public class AppearanceData : ScriptableObject
{
    public WoodcutterData woodCutter;
    public TreeData treeData;
    public BackgroundData backgroundData;
    public int scoreRequired;
}