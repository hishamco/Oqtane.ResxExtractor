Imports System.Text.RegularExpressions

Namespace Extraction
    Public Class ActionLinkComponentResourceProvider
        Inherits ComponentResourceProvider

        Private Shared ReadOnly _regularExpression As New Regex("<ActionLink(.*)?/>", RegexOptions.Compiled)
        Private Shared ReadOnly _properties As New List(Of String) From {"Text"}

        Public Sub New()
            Expression = _regularExpression
            LocalizableProperties = _properties
        End Sub

        Public Overrides ReadOnly Property Expression() As Regex

        Public Overrides ReadOnly Property LocalizableProperties() As IEnumerable(Of String)
    End Class
End Namespace
