Imports Microsoft.VisualBasic.ApplicationServices
Imports System.Reflection
Imports System.IO

Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Dim a As New OKMsgboxWindow("Fatal Error", 1, "Something went wrong.")

            Dim strFile As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) & "/ErrorLog_" & DateTime.Today.ToString("dd-MMM-yyyy") & ".txt"
            Dim sw As StreamWriter
            Dim fs As FileStream = Nothing
            sw = File.AppendText(strFile)
            sw.WriteLine("[" & DateTime.Now & "]: " & e.Exception.Message)
            sw.Close()

            a.ShowDialog()
        End Sub
    End Class


End Namespace

