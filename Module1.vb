Imports System.Diagnostics

Module Module1
    Sub Main()
        Try
            Dim proc As New Process()
            proc.StartInfo.FileName = "cmd.exe"
            proc.StartInfo.Verb = "runas"
            proc.StartInfo.Arguments = "/c sfc /scannow"
            proc.StartInfo.RedirectStandardOutput = True
            proc.StartInfo.RedirectStandardError = True
            proc.StartInfo.UseShellExecute = False
            proc.StartInfo.CreateNoWindow = True

            proc.Start()
            Console.WriteLine("Start scanning")

            Dim output As String = proc.StandardOutput.ReadToEnd()
            Dim errorOutput As String = proc.StandardError.ReadToEnd()

            proc.WaitForExit()

            Console.WriteLine("SFC Output:")
            Console.WriteLine(output)

            Console.WriteLine("End scanning")

            If Not String.IsNullOrEmpty(errorOutput) Then
                Console.WriteLine("Errors:")
                Console.WriteLine(errorOutput)
            End If

        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try
    End Sub
End Module