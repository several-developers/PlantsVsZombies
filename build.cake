#addin nuget:?package=Cake.Unity&version=0.9.0.0

var target = Argument("target", "Build-Android");

Task("Clean-Artifacts")
    .Does(() =>
{
    CleanDirectory($"./artifacts");
});

Task("Build-Android")
    .IsDependentOn("Clean-Artifacts")
    .Does(() =>
{
    var editor = FindUnityEditor();

    if (editor != null)
        Information("Found Unity Editor {0} at path {1}", editor.Version, editor.Path);
    else
        Warning("Cannot find Unity Editor");

    UnityEditor(
        new UnityEditorArguments
        {
            ExecuteMethod = "Core.Editor.Builder.BuildAndroid",
            BuildTarget = BuildTarget.Android,
            LogFile = "./artifacts/unity.log"
        },
        new UnityEditorSettings
        {
            RealTimeLog = true
        });
});

RunTarget(target);