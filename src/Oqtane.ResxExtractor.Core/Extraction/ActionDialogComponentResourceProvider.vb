Imports System.Text.RegularExpressions

Namespace Extraction
    Public Class ActionDialogComponentResourceProvider
        Inherits ComponentResourceProvider

        Private Shared ReadOnly _regularExpression As New Regex("<ActionDialog(.*)?/>", RegexOptions.Compiled)
        Private Shared ReadOnly _properties As New List(Of String) From {"Header", "Message", "Text"}

        Public Sub New()
            Expression = _regularExpression
            LocalizableProperties = _properties
        End Sub

        Public Overrides ReadOnly Property Expression() As Regex

        Public Overrides ReadOnly Property LocalizableProperties() As IEnumerable(Of String)

        Protected Overrides Function GetPropertySpan(propertyName As String, tag As String) As (Start As Integer, [End] As Integer)
            Dim propertySpan As (Integer, Integer) = (-1, -1)
            Dim propertyText As String = $"{propertyName}="
            Dim propertyIndex As Integer = tag.IndexOf(propertyText)
            If propertyIndex > -1 Then
                Dim propertyStartIndex As Integer = propertyIndex + propertyText.Length + 1
                Dim propertyEndIndex As Integer = tag.IndexOf("""", propertyStartIndex) - propertyStartIndex
                If propertyName = _properties(1) Then
                    If tag.Substring(propertyStartIndex, propertyEndIndex) = $"@{LocalizerIdentifierName.ViewLocalizer}[" Then
                        propertyStartIndex += 12
                        propertyEndIndex = tag.IndexOf("""", propertyStartIndex) - propertyStartIndex
                    End If
                End If

                propertySpan = (propertyStartIndex, propertyEndIndex)
            End If

            Return propertySpan
        End Function
    End Class
End Namespace
