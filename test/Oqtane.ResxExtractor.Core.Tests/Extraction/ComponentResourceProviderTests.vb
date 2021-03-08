Imports System.Text.RegularExpressions
Imports Oqtane.ResxExtractor.Core.Extraction
Imports Xunit

Public Class ComponentResourceProviderTests
    <Fact>
    Public Sub GetResourcesFromComponent()
        ' Arrange
        Dim provider As New ParagraphComponentResourceProvider()
        Dim contents As String = "<Paragraph Style=""color:red"" ResourceKey=""Para1"">Hi there ...</Paragraph>
<h1>Header</h1>
<Paragraph Style=""color:red"" ResourceKey=""Para2""></Paragraph>"

        ' Act
        Dim result As ProviderResourceResult = provider.DetermineProviderResourceResult(contents)

        ' Assert
        Assert.NotEmpty(result.Resources)
        Assert.Equal(result.Resources.Count, 2)
    End Sub

    <Fact>
    Public Sub GetResourceDetailsFromComponent()
        ' Arrange
        Dim provider As New ParagraphComponentResourceProvider()
        Dim contents As String = "<Paragraph Style=""color:red"" ResourceKey=""Para1"">Hi there ...</Paragraph>"

        ' Act
        Dim result As ProviderResourceResult = provider.DetermineProviderResourceResult(contents)

        ' Assert
        Assert.Equal(result.Resources.First().Name, "Para1.Style")
    End Sub

    Private Class ParagraphComponentResourceProvider
        Inherits ComponentResourceProvider

        Public Overrides ReadOnly Property Expression As Regex
            Get
                Return New Regex("<Paragraph(.*)?>(.*)?</Paragraph>", RegexOptions.Compiled)
            End Get
        End Property

        Public Overrides ReadOnly Property LocalizableProperties As IEnumerable(Of String)
            Get
                Return New List(Of String) From {"Style"}
            End Get
        End Property
    End Class
End Class
