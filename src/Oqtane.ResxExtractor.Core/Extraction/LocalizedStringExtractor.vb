Imports System.IO
Imports System.Text.RegularExpressions

Namespace Extraction
    Public Class LocalizedStringExtractor
        Implements ILocalizedStringExtractor

        Private Shared ReadOnly _localizerIdentifierNameRegularExpression As New Regex($"@{LocalizerIdentifierName.ViewLocalizer}\[""([\w\s\.!@,{{}}]+)""(,[\w\s]+)?\]", RegexOptions.Compiled)

        Private ReadOnly _projects As IEnumerable(Of IProject)

        Public Sub New(projects As IEnumerable(Of IProject))
            If projects Is Nothing Then
                Throw New ArgumentNullException(NameOf(projects))
            End If

            _projects = projects
        End Sub

        Public Async Function ExtractAsync() As Task(Of IEnumerable(Of LocalizedStringOccurence)) Implements ILocalizedStringExtractor.ExtractAsync
            Dim occurences As New List(Of LocalizedStringOccurence)()
            For Each project As IProject In _projects
                For Each projectFile As IProjectFile In project.Files
                    Dim fileLines() As String = Await File.ReadAllLinesAsync(projectFile.Path)
                    For i As Integer = 0 To fileLines.Length - 1
                        Dim line As String = fileLines(i)
                        For Each match As Match In _localizerIdentifierNameRegularExpression.Matches(line)
                            Dim value = match.Groups(1).Value
                            Dim occurence As New LocalizedStringOccurence With {
                                .Location = New LocalizedStringLocation With {
                                    .File = projectFile,
                                    .Line = i + 1,
                                    .Column = line.IndexOf(value) + 1
                                },
                                .Text = New LocalizedString(value)
                            }
                            occurences.Add(occurence)
                        Next
                    Next
                Next
            Next

            Return occurences
        End Function
    End Class
End Namespace
