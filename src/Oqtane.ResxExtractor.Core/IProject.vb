Public Interface IProject
    Inherits IProjectFile

    ReadOnly Property FilesExtension() As String

    ReadOnly Property Files() As IEnumerable(Of IProjectFile)
End Interface
