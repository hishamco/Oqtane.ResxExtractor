Imports Oqtane.ResxExtractor.Core.Extraction
Imports Xunit

Public Class SectionComponentResourceProviderTests
    <Fact>
    Public Sub GetResourcesFromComponent()
        ' Arrange
        Dim provider As New SectionComponentResourceProvider()
        Dim contents As String = "<Section Heading=""Site Settings"" Name=""Settings"" ResourceKey=""SiteSettings""></Section>"

        ' Act
        Dim result As ProviderResourceResult = provider.DetermineProviderResourceResult(contents)

        ' Assert
        Assert.NotEmpty(result.Resources)
        Assert.Equal("SiteSettings.Heading", result.Resources.First().Name)
        Assert.Equal("Site Settings", result.Resources.First().Value)
        Assert.Equal("SiteSettings.Name", result.Resources.Last().Name)
        Assert.Equal("Settings", result.Resources.Last().Value)
    End Sub
End Class
