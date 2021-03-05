Imports System.IO
Imports System.Reflection
Imports Xunit

Public Class ProjectTests
    <Fact>
    Public Sub GetProjectFiles()
        ' Arrange
        Dim executionPath As String = Assembly.GetExecutingAssembly().FullName
        Dim rootPath As String = New DirectoryInfo(executionPath).Parent.Parent.Parent.Parent.FullName
        Dim projectPath As String = Path.Combine(rootPath, "Oqtane.ResxExtractor.Core.Tests.vbproj")

        ' Act
        Dim project As New VisualBasicProject(projectPath)

        ' Assert
        Dim files As IEnumerable(Of String) = project.Files.Select(Function(f) f.Name)
        Assert.Contains("ProjectTests.vb", files)
        Assert.True(files.Count() > 1)
    End Sub
End Class
