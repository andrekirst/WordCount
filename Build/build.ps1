# # Install sonarqube runner

# choco install "msbuild-sonarqube-runner" -y

# MSBuild.SonarQube.Runner.exe begin /n:"WordCount" /k:"andrekirst:WordCount" /d:"sonar.organization=andrekirst-github" /d:"sonar.host.url=https://sonarcloud.io" /d:"sonar.login=$($env:SONARQUBE_TOKEN)" /v:"$($env:appveyor_build_version)"