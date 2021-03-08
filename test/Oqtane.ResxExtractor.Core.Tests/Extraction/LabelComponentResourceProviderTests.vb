Imports Oqtane.ResxExtractor.Core.Extraction
Imports Xunit

Public Class LabelComponentResourceProviderTests
    <Fact>
    Public Sub GetResourcesFromComponent()
        ' Arrange
        Dim provider As New LabelComponentResourceProvider()
        Dim contents As String = "<Label HelpText=""Enter your name?"" ResourceKey=""Name"">Name:</Label>"

        ' Act
        Dim result As ProviderResourceResult = provider.DetermineProviderResourceResult(contents)

        ' Assert
        Assert.NotEmpty(result.Resources)
        Assert.Equal("Name.HelpText", result.Resources.First().Name)
        Assert.Equal("Enter your name?", result.Resources.First().Value)
        Assert.Equal("Name.Text", result.Resources.Last().Name)
        Assert.Equal("Name:", result.Resources.Last().Value)
    End Sub
End Class
