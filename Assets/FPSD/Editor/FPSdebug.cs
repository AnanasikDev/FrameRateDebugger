using UnityEngine;
using UnityEditor;

/// <summary>
/// FPS (Frames per second) debugger utility for Unity
/// </summary>
public sealed class FPSdebug : EditorWindow
{
    private const string WindowPath = "Tools/FPSD";
    private const string WindowTitle = "FPSD";

    private bool Enabled = false;

    private const int MinFPSlimit = 10;
    private const int MaxFPSlimit = 400;

    private int FPSLimit = 60;
    private const int StableFPS = 60;
    private const int FloatingFPS = 1000;

    private readonly int[] Presets = new int[4] 
    { 
        30, 
        50, 
        60, 
        144 
    };

    private readonly Color Red =    new Color(0.9f, 0.4f, 0.4f, 1);
    private readonly Color Green =  new Color(0.4f, 0.9f, 0.4f, 1);

    [MenuItem(WindowPath)]
    public static void Init()
    {
        EditorWindow.GetWindow(typeof(FPSdebug), false, WindowTitle);
    }
    private void ReadPresets()
    {
        foreach (int preset in Presets)
        {
            if (GUILayout.Button(preset.ToString()))
            {
                FPSLimit = preset;
                return;
            }
        }
    }
    private void DrawActivationButton()
    {
        var backgroundColor = GUI.backgroundColor;
        GUI.backgroundColor = Enabled ? Green : Red;

        if (GUILayout.Button(Enabled ? "Disable" : "Enable"))
        {
            Enabled = !Enabled;
        }
        GUI.backgroundColor = backgroundColor;
    }
    private void SetFrameRate(int targetFPS)
    {
        Application.targetFrameRate = targetFPS;
    }
    public void OnGUI()
    {

        GUILayout.BeginHorizontal("box");

        ReadPresets();

        GUILayout.EndHorizontal();


        DrawActivationButton();


        GUILayout.BeginHorizontal("box");

        FPSLimit = EditorGUILayout.IntSlider(FPSLimit, MinFPSlimit, MaxFPSlimit);

        if (GUILayout.Button("X", GUILayout.Width(20)))
            FPSLimit = StableFPS;
        
        GUILayout.EndHorizontal();


        if (Enabled)
            SetFrameRate(FPSLimit); // stable

        else
            SetFrameRate(FloatingFPS); // floating
    }
}
