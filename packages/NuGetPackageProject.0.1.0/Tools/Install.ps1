param($installPath, $toolsPath, $package, $project)

Write-Host "Installing Project Template! not executing now";

# copy project template
# http://www.clariusconsulting.net/blogs/kzu/archive/2010/12/08/HowtocreatelightweightreusablesourcecodewithNuGet.aspx
#  snipet installing in install.ps1, modify to install project template.

Copy-Item $toolsPath\*.zip -destination ([System.Environment]::ExpandEnvironmentVariables("%VisualStudioDir%\Templates\ProjectTemplates\Visual C#\"))