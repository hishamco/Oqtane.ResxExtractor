Public Structure ResourceEntry
    Public Shared Empty As New ResourceEntry(Nothing, Nothing)

    Public Sub New(name As String, value As String)
        Me.Name = name
        Me.Value = value
    End Sub

    Public Sub New(name As String, value As String, comment As String)
        Me.New(name, value)
        Me.Comment = comment
    End Sub

    Public Property Name() As String

    Public Property Value() As String

    Public Property Comment() As String
End Structure
