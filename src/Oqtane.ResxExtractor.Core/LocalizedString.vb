Public Class LocalizedString
    Public Sub New(name As String)
        Me.New(name, name)
    End Sub

    Public Sub New(name As String, value As String)
        If String.IsNullOrEmpty(name) Then
            Throw New ArgumentException($"'{NameOf(name)}' cannot be null or empty", NameOf(name))
        End If

        Me.Name = name
        Me.Value = value
        Locations = New List(Of LocalizedStringLocation)()
    End Sub

    Public Sub New(localizedStringOcurrences As LocalizedStringOccurence)
        Me.New(localizedStringOcurrences.Text.Name, localizedStringOcurrences.Text.Value)

        Locations.Add(localizedStringOcurrences.Location)
    End Sub

    Public ReadOnly Property Locations() As List(Of LocalizedStringLocation)

    Public ReadOnly Property Name() As String

    Public ReadOnly Property Value() As String

    Public Overrides Function ToString() As String
        Return Value
    End Function
End Class
