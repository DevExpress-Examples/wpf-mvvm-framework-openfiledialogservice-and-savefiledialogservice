Imports System.Windows

Namespace FileDialogServicesSample
    Partial Public Class App
        Inherits Application

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            MyBase.OnStartup(e)
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName()
        End Sub
        Protected Overrides Sub OnExit(ByVal e As ExitEventArgs)
            MyBase.OnExit(e)
            DevExpress.Xpf.Core.ApplicationThemeHelper.SaveApplicationThemeName()
        End Sub
        Private Sub OnAppStartup_UpdateThemeName(ByVal sender As Object, ByVal e As StartupEventArgs)
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName()
        End Sub
    End Class
End Namespace
