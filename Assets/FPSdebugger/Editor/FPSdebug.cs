using UnityEngine;
using UnityEditor;

public sealed class FPSdebug : EditorWindow
{
    private bool Enabled = false;

    private const int MinFPSlimit = 10;
    private const int MaxFPSlimit = 400;

    private int FPSLimit = 60;
    private const int StableFPS = 60;

    private readonly int[] Presets = new int[4] 
    { 
        30, 
        50, 
        60, 
        144 
    };

    private Color Red = new Color(0.9f, 0.4f, 0.4f, 1);
    private Color Green = new Color(0.4f, 0.9f, 0.4f, 1);

    [MenuItem("Tools/FPS_debug")]
    public static void Init()
    {
        GetWindow<FPSdebug>().Show();
    }
    public void OnGUI()
    {
        GUILayout.BeginHorizontal("box");

        foreach (int preset in Presets)
        {
            if (GUILayout.Button(preset.ToString()))
            {
                FPSLimit = preset;
                break;
            }
        }

        GUILayout.EndHorizontal();

        var backgroundColor = GUI.backgroundColor;
        GUI.backgroundColor = Enabled ? Green : Red;

        if (GUILayout.Button(Enabled ? "Disable" : "Enable"))
        {
            Enabled = !Enabled;
        }
        GUI.backgroundColor = backgroundColor;

        GUILayout.BeginHorizontal("box");

        FPSLimit = EditorGUILayout.IntSlider(FPSLimit, MinFPSlimit, MaxFPSlimit);
        if (GUILayout.Button("X", GUILayout.Width(20)))
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
