Imports Oqtane.ResxExtractor.Core.Extraction
Imports Xunit

Public Class TabPanelComponentResourceProviderTests
    <InlineData("<TabPanel Name=""Upload"" ResourceKey=""UploadItem""></TabPanel>")>
    <InlineData("<TabPanel Name=""Upload"" ResourceKey=""UploadItem"">
    <p>Test</p>
</TabPanel>")>
    <Theory>
    Public Sub GetResourcesFromComponent(contents As String)
        ' Arrange
        Dim provider As New TabPanelComponentResourceProvider()

        ' Act
        Dim result As ProviderResourceResult = provider.DetermineProviderResourceResult(contents)

        ' Assert
        Assert.NotEmpty(result.Resources)
        Assert.Equal("UploadItem.Name", result.Resources.First().Name)
        Assert.Equal("Upload", result.Resources.First().Value)
    End Sub
End Class
