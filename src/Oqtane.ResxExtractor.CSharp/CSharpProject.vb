Imports Oqtane.ResxExtractor.Core

Public Class CSharpProject
    Inherits Project

    Public Sub New(path As String)
        MyBase.New(path)
    End Sub

    Public Overrides ReadOnly Property Extension() As String
        Get
            Return ".csproj"
        End Get
    End Property

    Public Overrides ReadOnly Property FilesExtension() As String
        Get
            Return ".cs"
        End Get
    End Property
End Class
