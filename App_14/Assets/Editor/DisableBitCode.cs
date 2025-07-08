using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace EditorExtensions
{
    public static class DisableBitCode
    {
        [PostProcessBuild(999)]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
        {
#if UNITY_IOS
            if (buildTarget != BuildTarget.iOS)
                return;

            string projectPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
            PBXProject project = new PBXProject();
            project.ReadFromFile(projectPath);

            //main
            string target = project.GetUnityMainTargetGuid();
            project.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

            //test
            target = project.TargetGuidByName(PBXProject.GetUnityTestTargetName());
            project.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

            //unity framework
            target = project.GetUnityFrameworkTargetGuid();
            project.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

            project.WriteToFile(projectPath);
#endif
        }
    }
}