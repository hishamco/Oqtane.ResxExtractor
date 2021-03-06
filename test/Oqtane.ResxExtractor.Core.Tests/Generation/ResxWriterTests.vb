Imports System.IO
Imports Oqtane.ResxExtractor.Core.Generation
Imports Xunit

Public Class ResxWriterTests
    <Fact>
    Public Sub WriteResources()
        ' Arrange
        Dim fileName As String = "Test.ar.resx"
        Dim resources As New Dictionary(Of String, String) From {
            {"Hello", "مرحبا"},
            {"Bye", "مع السلامة"}
        }
        Dim writer As New ResxWriter(fileName)

        ' Act
        writer.AddResource(resources.First().Key, resources.First().Value)
        writer.AddResource(resources.Last().Key, resources.Last().Value)
        writer.Generate()

        ' Assert
        Dim doc As XDocument = XDocument.Load(fileName)

        For Each element As XElement In doc.Root.<data>
            Assert.Contains(element.@name, resources.Keys)
            Assert.Contains(element.<value>.Value, resources.Values)
        Next

        File.Delete(fileName)
    End Sub
End Class
