/** ARGUMENTS **/

var configuration = Argument("Configuration", "Release");
var target = Argument("Target", "Default");

/** TASKS **/

Task("Build")
.Does(() =>
{

});

Task("Default")
.IsDependentOn("Build");

/** EXECUTION **/

RunTarget(target);