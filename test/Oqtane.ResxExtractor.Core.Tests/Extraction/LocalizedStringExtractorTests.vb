Imports System.IO
Imports System.Reflection
Imports Oqtane.ResxExtractor.Core.Extraction
Imports Oqtane.ResxExtractor.Razor
Imports Xunit

Public Class LocalizedStringExtractorTests
    <Fact>
    Public Async Sub ExtractLocalizationStrings()
        ' Arrange
        Dim executionPath As String = Assembly.GetExecutingAssembly().FullName
        Dim rootPath As String = New DirectoryInfo(executionPath).Parent.Parent.Parent.Parent.FullName
        Dim projectPath As String = Path.Combine(rootPath, "SampleProjects\Test1", "Test1.csproj")
        Dim project As New RazorProject(projectPath)
        Dim extractor As New LocalizedStringExtractor(New List(Of IProject) From {project})

        ' Act
        Dim occurrences As IEnumerable(Of LocalizedStringOccurence) = Await extractor.ExtractAsync()

        ' Assert
        Assert.Equal(4, occurrences.Count())
        Assert.Contains(occurrences, Function(o) o.Text = "Loading...")
        Assert.Contains(occurrences, Function(o) o.Text = "About")
        Assert.Contains(occurrences, Function(o) o.Text = "This is About page.")
        Assert.Contains(occurrences, Function(o) o.Text = "Index from inner folder")
    End Sub

    <Fact>
    Public Async Sub GetLocalizedStringDetails()
        ' Arrange
        Dim executionPath As String = Assembly.GetExecutingAssembly().FullName
        Dim rootPath As String = New DirectoryInfo(executionPath).Parent.Parent.Parent.Parent.FullName
        Dim projectPath As String = Path.Combine(rootPath, "SampleProjects\Test1", "Test1.csproj")
        Dim project As New RazorProject(projectPath)
        Dim extractor As New LocalizedStringExtractor(New List(Of IProject) From {project})

        ' Act
        Dim occurrences As IEnumerable(Of LocalizedStringOccurence) = Await extractor.ExtractAsync()

        ' Assert
        Dim occurence As LocalizedStringOccurence = occurrences.First()
        Assert.Equal("About", occurence.Text)
        Assert.Equal("About.razor", occurence.Location.File.Name)
        Assert.Equal(17, occurence.Location.Column)
        Assert.Equal(3, occurence.Location.Line)
    End Sub
End Class
