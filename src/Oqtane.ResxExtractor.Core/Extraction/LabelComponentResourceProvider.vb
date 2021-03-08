Imports System.Text.RegularExpressions

Namespace Extraction
    Public Class LabelComponentResourceProvider
        Inherits ComponentResourceProvider

        Private Shared ReadOnly _regularExpression As New Regex("<Label(.*)?>(.*)?</Label>", RegexOptions.Compiled)
        Private Shared ReadOnly _properties As New List(Of String) From {"HelpText", "Text"}

        Public Sub New()
            Expression = _regularExpression
            LocalizableProperties = _properties
        End Sub

        Public Overrides ReadOnly Property Expression() As Regex

        Public Overrides ReadOnly Property LocalizableProperties() As IEnumerable(Of String)

        Public Overrides Function DetermineProviderResourceResult(contents As String) As ProviderResourceResult
            Dim result As ProviderResourceResult = MyBase.DetermineProviderResourceResult(contents)
            Dim resources As List(Of ResourceEntry) = result.Resources
            For Each match As Match In Expression.Matches(contents)
                Dim value As String = match.Groups(1).Value
                If value.Contains(ResourceKeyPropertyName) Then
                    Dim resourceKey As String = GetResourceKey(value)
                    Dim resource As New ResourceEntry With {
                        .Name = $"{resourceKey}.{LocalizableProperties.Last()}",
                        .Value = match.Groups(2).Value
                    }

                    resources.Add(resource)
                End If
            Next

            Return New ProviderResourceResult(resources)
        End Function
    End Class
End Namespace
