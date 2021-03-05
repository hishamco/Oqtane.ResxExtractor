Public Class ProjectFile
    Implements IProjectFile

    Public Sub New(path As String)
        If String.IsNullOrEmpty(path) Then
            Throw New ArgumentException($"'{NameOf(path)}' cannot be null or empty", NameOf(path))
        End If

        Me.Path = path
    End Sub

    Public Property Path() As String Implements IProjectFile.Path

    Public ReadOnly Property Name() As String Implements IProjectFile.Name
        Get
            Return IO.Path.GetFileName(Path)
        End Get
    End Property

    Public ReadOnly Property Extension As String Implements IProjectFile.Extension
        Get
            Return IO.Path.GetExtension(Path)
        End Get
    End Property
End Class
