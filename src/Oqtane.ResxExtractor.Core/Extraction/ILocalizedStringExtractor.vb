Namespace Extraction
    Public Interface ILocalizedStringExtractor
        Function ExtractAsync() As Task(Of IEnumerable(Of LocalizedStringOccurence))
    End Interface
End Namespace
