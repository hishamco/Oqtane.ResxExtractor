Imports Oqtane.ResxExtractor.Core

Public Class CSharpProject
    Inherits Project

    Public Sub New(path As String)
        MyBase.New(path)
    End Sub

    Public Overrides ReadOnly Property Extension() As String = ".csproj"

    Public Overrides ReadOnly Property FilesExtension() As String = ".cs"
End Class
