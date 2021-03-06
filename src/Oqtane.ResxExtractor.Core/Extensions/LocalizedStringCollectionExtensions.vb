Imports System.Runtime.CompilerServices

Public Module LocalizedStringCollectionExtensions
    <Extension>
    Public Sub Add(collection As LocalizedStringCollection, occurence As LocalizedStringOccurence)
        If collection Is Nothing Then
            Throw New ArgumentNullException(NameOf(collection))
        End If

        If occurence Is Nothing Then
            Throw New ArgumentNullException(NameOf(occurence))
        End If

        collection.Add(New LocalizedString(occurence))
    End Sub

    <Extension>
    Public Sub AddRange(collection As LocalizedStringCollection, occurences As IEnumerable(Of LocalizedStringOccurence))
        If collection Is Nothing Then
            Throw New ArgumentNullException(NameOf(collection))
        End If

        If occurences Is Nothing Then
            Throw New ArgumentNullException(NameOf(occurences))
        End If

        For Each occurence As LocalizedStringOccurence In occurences
            collection.Add(occurence)
        Next
    End Sub
End Module
