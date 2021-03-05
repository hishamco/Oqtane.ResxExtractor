Imports System.IO

Public MustInherit Class Project
    Implements IProject

    Private ReadOnly _files As New List(Of IProjectFile)

    Public Sub New(path As String)
        If String.IsNullOrEmpty(path) Then
            Throw New ArgumentException($"'{NameOf(path)}' cannot be null or empty", NameOf(path))
        End If

        If IO.Path.GetExtension(path) <> Extension Then
            Throw New ArgumentException($"'{NameOf(path)}' should has '{Extension}' extension", NameOf(path))
        End If

        Me.Path = path
        Name = IO.Path.GetFileName(path)

        LoadFiles()
    End Sub

    Public Property Path As String Implements IProject.Path

    Public ReadOnly Property Name As String Implements IProject.Name

    Public MustOverride ReadOnly Property Extension() As String Implements IProject.Extension

    Public MustOverride ReadOnly Property FilesExtension() As String Implements IProject.FilesExtension

    Public ReadOnly Property Files As IEnumerable(Of IProjectFile) Implements IProject.Files
        Get
            Return _files
        End Get
    End Property

    Public Sub LoadFiles()
        Dim folderPath As String = New FileInfo(Path).DirectoryName
        For Each file In Directory.EnumerateFiles(folderPath, $"*{FilesExtension}")
            _files.Add(New ProjectFile(file))
        Next
    End Sub
End Class
