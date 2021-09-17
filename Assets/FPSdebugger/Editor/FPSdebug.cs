using UnityEngine;
using UnityEditor;

public sealed class FPSdebug : EditorWindow
{
    private bool Enabled = false;
    private int FPSLimit = 60;

    [MenuItem("Tools/FPS_debug")]
    public static void Init()
    {
        GetWindow<FPSdebug>().Show();
    }
    public void OnGUI()
    {
        Enabled = EditorGUILayout.Toggle("Limit", Enabled);
        FPSLimit = EditorGUILayout.IntSlider("FPS Limitation", FPSLimit, 10, 1000);

        if (Enabled)
        {
            Application.targetFrameRate = FPSLimit;
        }
    }
}
