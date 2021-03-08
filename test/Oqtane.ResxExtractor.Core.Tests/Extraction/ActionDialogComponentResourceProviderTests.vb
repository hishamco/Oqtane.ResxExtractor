Imports Oqtane.ResxExtractor.Core.Extraction
Imports Xunit

Public Class ActionDialogComponentResourceProviderTests
    <Fact>
    Public Sub GetResourcesFromComponent()
        ' Arrange
        Dim provider As New ActionDialogComponentResourceProvider()
        Dim contents As String = "<ActionDialog Header=""Delete Item"" Text=""Delete"" Message=""Are you sure you wish to delete this item?"" ResourceKey=""DeleteItem""/>"

        ' Act
        Dim result As ProviderResourceResult = provider.DetermineProviderResourceResult(contents)

        ' Assert
        Assert.NotEmpty(result.Resources)
        Assert.Equal("DeleteItem.Header", result.Resources.ElementAt(0).Name)
        Assert.Equal("Delete Item", result.Resources.ElementAt(0).Value)
        Assert.Equal("DeleteItem.Message", result.Resources.ElementAt(1).Name)
        Assert.Equal("Are you sure you wish to delete this item?", result.Resources.ElementAt(1).Value)
        Assert.Equal("DeleteItem.Text", result.Resources.ElementAt(2).Name)
        Assert.Equal("Delete", result.Resources.ElementAt(2).Value)
    End Sub
End Class
