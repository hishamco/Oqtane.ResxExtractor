Imports Oqtane.ResxExtractor.Core.Generation

Public Class ProviderResourceResult
    Public Sub New()
        Me.New(New List(Of ResourceEntry)())
    End Sub

    Public Sub New(resources As IEnumerable(Of ResourceEntry))
        Me.Resources = resources
    End Sub

    Public ReadOnly Property Resources() As IList(Of ResourceEntry)
End Class
