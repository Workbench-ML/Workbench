/** ARGUMENTS **/

var configuration = Argument("Configuration", "Release");
var target = Argument("Target", "Default");
var rootDirectory = MakeAbsolute(Directory("../")).FullPath;

/** TASKS **/

string RunGit(string command, string separator = "") 
{
    using(var process = StartAndReturnProcess("git", new ProcessSettings { Arguments = command, RedirectStandardOutput = true })) 
    {
        process.WaitForExit();
        return string.Join(separator, process.GetStandardOutput());
    }
}

Task("Pull-Dependencies")
.Does(() => 
{
    Information("Updating git submodules");
    StartProcess("git", string.Format("--git-dir=\"{0}\"/.git submodule update --init --recursive", rootDirectory));
});

Task("Build-Workbench-Mono")
.Does(() =>
{

});

Task("Build")
.Does(() =>
{

});

Task("Default")
.IsDependentOn("Pull-Dependencies")
.Does(() => 
{
    
});

/** EXECUTION **/

RunTarget(target);