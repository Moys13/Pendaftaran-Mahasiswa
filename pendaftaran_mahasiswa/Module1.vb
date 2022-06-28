Imports MySql.Data.MySqlClient
Module Module1
    Public conn As MySqlConnection
    Public cmd As MySqlCommand
    Public da As MySqlDataAdapter
    Public ds As DataSet
    Public rd As MySqlDataReader

    Public Sub connection()
        Try
            conn = New MySqlConnection("server=localhost; userid=root; password=; database=pendaftaran")
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch
            MsgBox("database ga konek!")
            End
        End Try
    End Sub
End Module
