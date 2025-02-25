Imports System.Diagnostics
Imports System.IO
Imports System.Runtime.InteropServices

Module Module1

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function Wow64DisableWow64FsRedirection(ByRef oldValue As IntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function Wow64RevertWow64FsRedirection(ByVal oldValue As IntPtr) As Boolean
    End Function
    Sub Main()
        Dim oldValue As IntPtr

        If Wow64DisableWow64FsRedirection(oldValue) Then
            Console.WriteLine("Start scanning...")
            Dim psi As New ProcessStartInfo()
            psi.FileName = "cmd.exe"
            psi.Arguments = "/c sfc /scannow"
            psi.UseShellExecute = False
            psi.RedirectStandardOutput = True
            psi.CreateNoWindow = True

            Try
                Dim process As Process = Process.Start(psi)
                Dim reader As StreamReader = process.StandardOutput
                While Not process.HasExited
                    If Not reader.EndOfStream Then
                        Dim output As String = reader.ReadLine()
                        If output IsNot Nothing Then
                            Console.WriteLine(output)
                        End If
                    End If
                End While
                Console.WriteLine("End scanning.")
            Catch ex As Exception
                Console.WriteLine("Error: " & ex.Message)
            End Try
        End If
    End Sub
End Module