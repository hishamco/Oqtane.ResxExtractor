Public Class VisualBasicProject
    Inherits Project

    Public Sub New(path As String)
        MyBase.New(path)
    End Sub

    Public Overrides ReadOnly Property Extension() As String
        Get
            Return ".vbproj"
        End Get
    End Property

    Public Overrides ReadOnly Property FilesExtension() As String
        Get
            Return ".vb"
        End Get
    End Property
End Class
