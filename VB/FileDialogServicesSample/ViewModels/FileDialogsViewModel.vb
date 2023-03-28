Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports System.IO
Imports System.Linq

Namespace FileDialogServicesSample.ViewModels

    <POCOViewModel>
    Public Class FileDialogsViewModel

'#Region "Common properties"
        Public Overridable Property Filter As String

        Public Overridable Property FilterIndex As Integer

        Public Overridable Property Title As String

        Public Overridable Property DialogResult As Boolean

        Public Overridable Property ResultFileName As String

        Public Overridable Property FileBody As String

'#End Region
'#Region "SaveFileDialogService specific properties"
        Public Overridable Property DefaultExt As String

        Public Overridable Property DefaultFileName As String

        Public Overridable Property OverwritePrompt As Boolean

'#End Region
        Protected ReadOnly Property SaveFileDialogService As ISaveFileDialogService
            Get
                Return GetService(Of ISaveFileDialogService)()
            End Get
        End Property

        Protected ReadOnly Property OpenFileDialogService As IOpenFileDialogService
            Get
                Return GetService(Of IOpenFileDialogService)()
            End Get
        End Property

        Public Sub New()
            Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*"
            FilterIndex = 1
            Title = "Custom Dialog Title"
            DefaultExt = "txt"
            DefaultFileName = "Document1"
            OverwritePrompt = True
        End Sub

        Public Sub Open()
            OpenFileDialogService.Filter = Filter
            OpenFileDialogService.FilterIndex = FilterIndex
            DialogResult = OpenFileDialogService.ShowDialog()
            If Not DialogResult Then
                ResultFileName = String.Empty
            Else
                Dim file As IFileInfo = OpenFileDialogService.Files.First()
                ResultFileName = file.Name
                Using stream = file.OpenText()
                    FileBody = stream.ReadToEnd()
                End Using
            End If
        End Sub

        Public Sub Save()
            SaveFileDialogService.DefaultExt = DefaultExt
            SaveFileDialogService.DefaultFileName = DefaultFileName
            SaveFileDialogService.Filter = Filter
            SaveFileDialogService.FilterIndex = FilterIndex
            DialogResult = SaveFileDialogService.ShowDialog()
            If Not DialogResult Then
                ResultFileName = String.Empty
            Else
                Using stream = New StreamWriter(SaveFileDialogService.OpenFile())
                    stream.Write(FileBody)
                End Using
            End If
        End Sub
    End Class
End Namespace
