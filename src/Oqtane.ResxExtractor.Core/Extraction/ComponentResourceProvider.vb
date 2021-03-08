Imports System.Text.RegularExpressions

Namespace Extraction
    Public MustInherit Class ComponentResourceProvider
        Implements IResourceProvider

        Private Shared ReadOnly DoubleQuotation As String = """"

        Protected Shared ReadOnly ResourceKeyPropertyName As String = "ResourceKey"

        Public MustOverride ReadOnly Property Expression() As Regex

        Public MustOverride ReadOnly Property LocalizableProperties() As IEnumerable(Of String)

        Public Overridable Function DetermineProviderResourceResult(contents As String) As ProviderResourceResult Implements IResourceProvider.DetermineProviderResourceResult
            Dim resources As New List(Of ResourceEntry)()
            For Each match As Match In Expression.Matches(contents)
                Dim value As String = match.Groups(1).Value
                If value.Contains(ResourceKeyPropertyName) Then
                    Dim resourceKey As String = GetResourceKey(value)

                    For Each [property] As String In LocalizableProperties
                        Dim resource As ResourceEntry = GetResource([property], value)
                        If Not resource.Equals(ResourceEntry.Empty) Then
                            resources.Add(resource)
                        End If
                    Next
                End If
            Next

            Return New ProviderResourceResult(resources)
        End Function

        Protected Function GetResource(propertyName As String, tag As String) As ResourceEntry
            Dim resource As ResourceEntry = ResourceEntry.Empty
            Dim propertySpan = GetPropertySpan(propertyName, tag)

            If propertySpan.Start <> -1 AndAlso propertySpan.End <> -1 Then
                Dim resourceKey As String = GetResourceKey(tag)
                resource = New ResourceEntry With {
                    .Name = $"{resourceKey}.{propertyName}",
                    .Value = tag.Substring(propertySpan.Start, propertySpan.End)
                }
            End If

            Return resource
        End Function

        Protected Function GetResourceKey(tag As String) As String
            Dim resourceKey As String = Nothing
            Dim propertySpan = GetPropertySpan(ResourceKeyPropertyName, tag)

            If propertySpan.Start <> -1 AndAlso propertySpan.End <> -1 Then
                resourceKey = tag.Substring(propertySpan.Start, propertySpan.End)
            End If

            Return resourceKey
        End Function

        Protected Overridable Function GetPropertySpan(propertyName As String, tag As String) As (Start As Integer, [End] As Integer)
            Dim propertySpan As (Integer, Integer) = (-1, -1)
            Dim propertyText As String = $"{propertyName}="
            Dim propertyIndex As Integer = tag.IndexOf(propertyText)
            If propertyIndex > -1 Then
                Dim propertyStartIndex As Integer = propertyIndex + propertyText.Length + 1
                Dim propertyEndIndex As Integer = tag.IndexOf(DoubleQuotation, propertyStartIndex) - propertyStartIndex

                propertySpan = (propertyStartIndex, propertyEndIndex)
            End If

            Return propertySpan
        End Function
    End Class
End Namespace
