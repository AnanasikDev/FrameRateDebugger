using UnityEngine;
using UnityEditor;

public sealed class FPSdebug : EditorWindow
{
    private bool Enabled = false;

    private const int MinFPSlimit = 10;
    private const int MaxFPSlimit = 400;

    private int FPSLimit = 60;
    private const int StableFPS = 60;

    [MenuItem("Tools/FPS_debug")]
    public static void Init()
    {
        GetWindow<FPSdebug>().Show();
    }
    public void OnGUI()
    {
        Enabled = EditorGUILayout.Toggle("Limit", Enabled);
        
        GUILayout.BeginHorizontal("box");

        FPSLimit = EditorGUILayout.IntSlider("FPS Limitation", FPSLimit, MinFPSlimit, MaxFPSlimit);
        if (GUILayout.Button("X"))
        {
            FPSLimit = StableFPS;
        }
        
        GUILayout.EndHorizontal();

        if (Enabled)
        {
            Application.targetFrameRate = FPSLimit; // stable
        }
        if (!Enabled)
        {
            Application.targetFrameRate = 1000; // floating
        }
    }
}
