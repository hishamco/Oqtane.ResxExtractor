Imports Oqtane.ResxExtractor.Core.Extraction
Imports Xunit

Public Class ActionLinkComponentResourceProviderTests
    <Fact>
    Public Sub GetResourcesFromComponent()
        ' Arrange
        Dim provider As New ActionLinkComponentResourceProvider()
        Dim contents As String = "<ActionLink Action=""Add"" Text=""Add Item"" ResourceKey=""AddItem""/>"

        ' Act
        Dim result As ProviderResourceResult = provider.DetermineProviderResourceResult(contents)

        ' Assert
        Assert.NotEmpty(result.Resources)
        Assert.Equal("AddItem.Text", result.Resources.First().Name)
        Assert.Equal("Add Item", result.Resources.First().Value)
    End Sub
End Class
