Imports System.IO
Imports System.Text.RegularExpressions

Namespace Extraction
    Public Class LocalizedStringExtractor
        Implements ILocalizedStringExtractor

        Private Shared ReadOnly _localizerIdentifierNameRegularExpression As New Regex("@" + LocalizerIdentifierName.ViewLocalizer + "\[""(?<Key>.+)""(,.+)?\]", RegexOptions.Compiled)

        Private ReadOnly _projects As IEnumerable(Of IProject)
        Private ReadOnly _resourceProviders As IEnumerable(Of IResourceProvider)

        Public Sub New(projects As IEnumerable(Of IProject))
            If projects Is Nothing Then
                Throw New ArgumentNullException(NameOf(projects))
            End If

            _projects = projects
            _resourceProviders = New List(Of IResourceProvider) From {
                New LabelComponentResourceProvider(),
                New ActionLinkComponentResourceProvider(),
                New ActionDialogComponentResourceProvider(),
                New SectionComponentResourceProvider(),
                New TabPanelComponentResourceProvider()
            }
        End Sub

        Public Async Function ExtractAsync() As Task(Of IEnumerable(Of LocalizedStringOccurence)) Implements ILocalizedStringExtractor.ExtractAsync
            Dim occurences As New List(Of LocalizedStringOccurence)()
            For Each project As IProject In _projects
                For Each projectFile As IProjectFile In project.Files
                    Dim contents As String = Await File.ReadAllTextAsync(projectFile.Path)
                    Dim fileLines() As String = contents.Split(Environment.NewLine)
                    For i As Integer = 0 To fileLines.Length - 1
                        Dim line As String = fileLines(i)
                        For Each match As Match In _localizerIdentifierNameRegularExpression.Matches(line)
                            Dim value As String = match.Groups("Key").Value
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

                    For Each resourceProvider As IResourceProvider In _resourceProviders
                        Dim resourceResult As ProviderResourceResult = resourceProvider.DetermineProviderResourceResult(contents)
                        For Each resource As ResourceEntry In resourceResult.Resources
                            ' TODO: Get the resource location
                            Dim occurence As New LocalizedStringOccurence With {
                                .Location = New LocalizedStringLocation With {
                                    .File = projectFile,
                                    .Line = 0,
                                    .Column = 0
                                },
                                .Text = New LocalizedString(resource.Name, resource.Value)
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
