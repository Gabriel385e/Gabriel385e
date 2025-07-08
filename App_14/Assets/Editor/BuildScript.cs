using UnityEditor;
using UnityEditor.Build.Reporting;
using System.IO;

public class BuildScript
{
    public static void BuildiOS()
    {
        string buildPath = "ios_build";

        if (!Directory.Exists(buildPath))
            Directory.CreateDirectory(buildPath);

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        
        buildPlayerOptions.scenes = new[] {
            "Assets/Scenes/Game.unity",
            "Assets/Scenes/Main.unity",
            "Assets/Scenes/Preload.unity"
        };
        
        buildPlayerOptions.locationPathName = buildPath;
        buildPlayerOptions.target = BuildTarget.iOS;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        
        if (report.summary.result != BuildResult.Succeeded)
        {
            throw new System.Exception("iOS build failed");
        }
    }
}