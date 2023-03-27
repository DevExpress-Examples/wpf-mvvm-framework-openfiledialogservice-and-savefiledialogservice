Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports System.IO
Imports System.Linq

Namespace FileDialogServicesSample.ViewModels

    Public Class FileDialogsViewModel
        Inherits ViewModelBase

'#Region "Common properties"
        Public Property Filter As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property FilterIndex As Integer
            Get
                Return GetValue(Of Integer)()
            End Get

            Set(ByVal value As Integer)
                SetValue(value)
            End Set
        End Property

        Public Property Title As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property DialogResult As Boolean
            Get
                Return GetValue(Of Boolean)()
            End Get

            Protected Set(ByVal value As Boolean)
                SetValue(value)
            End Set
        End Property

        Public Property ResultFileName As String
            Get
                Return GetValue(Of String)()
            End Get

            Protected Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property FileBody As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

'#End Region
'#Region "SaveFileDialogService specific properties"
        Public Property DefaultExt As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property DefaultFileName As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property OverwritePrompt As Boolean
            Get
                Return GetValue(Of Boolean)()
            End Get

            Set(ByVal value As Boolean)
                SetValue(value)
            End Set
        End Property

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

        <Command>
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

        <Command>
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
