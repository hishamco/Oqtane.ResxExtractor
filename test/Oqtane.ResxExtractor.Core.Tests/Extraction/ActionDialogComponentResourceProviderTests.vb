Imports Oqtane.ResxExtractor.Core.Extraction
Imports Xunit

Public Class ActionDialogComponentResourceProviderTests
    <Fact>
    Public Sub GetResourcesFromComponent()
        ' Arrange
        Dim provider As New ActionDialogComponentResourceProvider()
        Dim contents As String = "<ActionDialog Header=""Delete Item"" Message=""Are you sure you wish to delete this item?"" ResourceKey=""DeleteItem""/>"

        ' Act
        Dim result As ProviderResourceResult = provider.DetermineProviderResourceResult(contents)

        ' Assert
        Assert.NotEmpty(result.Resources)
        Assert.Equal("DeleteItem.Header", result.Resources.First().Name)
        Assert.Equal("Delete Item", result.Resources.First().Value)
        Assert.Equal("DeleteItem.Message", result.Resources.Last().Name)
        Assert.Equal("Are you sure you wish to delete this item?", result.Resources.Last().Value)
    End Sub
End Class
