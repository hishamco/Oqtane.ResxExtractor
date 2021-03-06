Public Class LocalizedStringCollection
    Implements ICollection(Of LocalizedString)

    Private ReadOnly _localizedStringsDictionary As IDictionary(Of String, LocalizedString)

    Public Sub New()
        _localizedStringsDictionary = New Dictionary(Of String, LocalizedString)()
    End Sub

    Public ReadOnly Property Count As Integer Implements ICollection(Of LocalizedString).Count
        Get
            Return _localizedStringsDictionary.Values.Count
        End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of LocalizedString).IsReadOnly
        Get
            Return _localizedStringsDictionary.Values.IsReadOnly
        End Get
    End Property

    Public Sub Add(item As LocalizedString) Implements ICollection(Of LocalizedString).Add
        If item Is Nothing Then
            Throw New ArgumentNullException(NameOf(item))
        End If

        Dim key As String = item.Value
        Dim localizedString As LocalizedString = Nothing
        If _localizedStringsDictionary.TryGetValue(key, localizedString) Then
            localizedString.Locations.AddRange(item.Locations)
        Else
            _localizedStringsDictionary.Add(key, item)
        End If
    End Sub

    Public Sub Clear() Implements ICollection(Of LocalizedString).Clear
        _localizedStringsDictionary.Clear()
    End Sub

    Public Sub CopyTo(array() As LocalizedString, arrayIndex As Integer) Implements ICollection(Of LocalizedString).CopyTo
        Throw New NotImplementedException()
    End Sub

    Public Function Contains(item As LocalizedString) As Boolean Implements ICollection(Of LocalizedString).Contains
        Return _localizedStringsDictionary.Values.Contains(item)
    End Function

    Public Function Remove(item As LocalizedString) As Boolean Implements ICollection(Of LocalizedString).Remove
        Return _localizedStringsDictionary.Remove(item.Value)
    End Function

    Public Function GetEnumerator() As IEnumerator(Of LocalizedString) Implements IEnumerable(Of LocalizedString).GetEnumerator
        Return _localizedStringsDictionary.Values.GetEnumerator()
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function
End Class
