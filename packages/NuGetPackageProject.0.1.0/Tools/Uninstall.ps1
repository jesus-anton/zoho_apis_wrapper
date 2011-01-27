param($installPath, $toolsPath, $package, $project)
  remove-item ([System.Environment]::ExpandEnvironmentVariables("%VisualStudioDir%\Templates\ProjectTemplates\Visual C#\NuGetPackageProject.zip"))
