Imports MySql.Data.MySqlClient
Public Class Form1
    Dim noPendaftaran As String
    Dim programStudi As String
    Dim namaMahasiswa As String
    Dim jenisKelamin As String
    Dim tempatLahir As String
    Dim tanggalLahir As String
    Dim agama As String
    Dim alamat As String
    Dim noTelepon As String
    Dim email As String
    Dim search As String

    Dim sql As String
    Sub getInput()
        noPendaftaran = txt_nodaftar.Text
        programStudi = cmb_studi.Text
        namaMahasiswa = txt_nama.Text
        tempatLahir = txt_tmptlahir.Text
        tanggalLahir = txt_tgllahir.Text
        agama = cmb_agama.Text
        alamat = txt_alamat.Text
        noTelepon = txt_notelp.Text
        email = txt_email.Text
    End Sub
    Sub displayData()
        Call connection()
        da = New MySqlDataAdapter("SELECT * FROM mahasiswa ORDER BY no_pendaftaran ASC", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds)
        dgv_mahasiswa.DataSource = ds.Tables(0)
        Call autoNumber()
        Call clear()
        Call cmbItems()
        btn_edit.Enabled = False
        btn_delete.Enabled = False
    End Sub

    Sub cmbItems()
        cmb_studi.Items.Add("Manajemen Rumah Sakit (MRS)")
        cmb_studi.Items.Add("Rekam Medis & Info Kesehatan (RMIK)")
        cmb_studi.Items.Add("Informatika Rekam Medis (IRM)")
        cmb_studi.Items.Add("Farmasi (FAR)")
        cmb_studi.Items.Add("Fisioterapi (FIS)")
        cmb_studi.Items.Add("Analisis Kesehatan (AKS)")

        cmb_agama.Items.Add("Islam")
        cmb_agama.Items.Add("Kristen")
        cmb_agama.Items.Add("Hindu")
        cmb_agama.Items.Add("Budha")
    End Sub

    Sub saveData()
        If txt_nodaftar.Text = "" Or cmb_studi.Text = "" Or txt_nama.Text = "" Or jenisKelamin = "" Or txt_tmptlahir.Text = "" Or cmb_agama.Text = "" Or txt_alamat.Text = "" Or txt_notelp.Text = "" Or txt_email.Text = "" Then
            MsgBox("Isi data dengan lengkap, dan pastikan tidak ada yang kosong!", MsgBoxStyle.Information, "ERROR")
        Else
            Call connection()
            Call getInput()
            sql = "INSERT INTO mahasiswa values('" & noPendaftaran & "', '" & programStudi & "', '" & namaMahasiswa & "', '" & jenisKelamin & "', '" & tempatLahir & "', '" & tanggalLahir & "', '" & agama & "', '" & alamat & "', " & noTelepon & ", '" & email & "')"
            cmd = New MySqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Kamu telah berhasil menginputkan data", MsgBoxStyle.MsgBoxRight, "SUCCESS")
            Call displayData()
        End If
    End Sub

    Sub autoNumber()
        Call connection()
        sql = "SELECT * FROM mahasiswa ORDER BY no_pendaftaran DESC"
        cmd = New MySqlCommand(sql, conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            txt_nodaftar.Text = "MHS00001"
        Else
            txt_nodaftar.Text = Microsoft.VisualBasic.Mid(rd.Item("no_pendaftaran").ToString, 6, 4) + 1
            If Len(txt_nodaftar.Text) = 1 Then
                txt_nodaftar.Text = "MHS0000" + txt_nodaftar.Text
            ElseIf Len(txt_nodaftar.Text) = 2 Then
                txt_nodaftar.Text = "MHS000" + txt_nodaftar.Text
            ElseIf Len(txt_nodaftar.Text) = 3 Then
                txt_nodaftar.Text = "MHS00" + txt_nodaftar.Text
            ElseIf Len(txt_nodaftar.Text) = 4 Then
                txt_nodaftar.Text = "MHS0" + txt_nodaftar.Text
            ElseIf Len(txt_nodaftar.Text) = 5 Then
                txt_nodaftar.Text = "MHS" + txt_nodaftar.Text

            End If
        End If

    End Sub

    Sub getData()
        With dgv_mahasiswa
            txt_nodaftar.Text = .Item(0, .CurrentRow.Index).Value.ToString
            cmb_studi.Text = .Item(1, .CurrentRow.Index).Value.ToString
            txt_nama.Text = .Item(2, .CurrentRow.Index).Value.ToString
            If .Item(3, .CurrentRow.Index).Value = "Perempuan" Then
                RadioButton2.Checked = True
            ElseIf .Item(3, .CurrentRow.Index).Value = "Laki - Laki" Then
                RadioButton1.Checked = True
            End If
            txt_tmptlahir.Text = .Item(4, .CurrentRow.Index).Value.ToString
            txt_tgllahir.Text = .Item(5, .CurrentRow.Index).Value.ToString
            cmb_agama.Text = .Item(6, .CurrentRow.Index).Value.ToString
            txt_alamat.Text = .Item(7, .CurrentRow.Index).Value.ToString
            txt_notelp.Text = .Item(8, .CurrentRow.Index).Value
            txt_email.Text = .Item(9, .CurrentRow.Index).Value.ToString
        End With
        btn_edit.Enabled = True
        btn_delete.Enabled = True
    End Sub

    Sub clear()
        cmb_studi.Text = ""
        txt_nama.Text = ""
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        txt_tmptlahir.Text = ""
        txt_tgllahir.Text = Now
        cmb_agama.Text = ""
        txt_alamat.Text = ""
        txt_notelp.Text = ""
        txt_email.Text = ""
    End Sub

    Sub editData()
        If txt_nodaftar.Text = "" Or cmb_studi.Text = "" Or txt_nama.Text = "" Or jenisKelamin = "" Or txt_tmptlahir.Text = "" Or cmb_agama.Text = "" Or txt_alamat.Text = "" Or txt_notelp.Text = "" Or txt_email.Text = "" Then
            MsgBox("Isi data dengan lengkap, dan pastikan tidak ada yang kosong!", MsgBoxStyle.Information, "ERROR")
        Else
            Call connection()
            Call getInput()
            sql = "UPDATE mahasiswa SET program_studi = '" & programStudi & "', nama_siswa = '" & namaMahasiswa & "', jenis_kelamin = '" & jenisKelamin & "', tempat_lahir = '" & tempatLahir & "', tanggal_lahir = '" & tanggalLahir & "', agama = '" & agama & "', alamat = '" & alamat & "', no_telepon = '" & noTelepon & "', email = '" & email & "' WHERE no_pendaftaran = '" & noPendaftaran & "'"
            cmd = New MySqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Data berhasil di ubah.", MsgBoxStyle.MsgBoxRight, "SUCCESS")
            displayData()
        End If
    End Sub

    Sub deleteData()
        If txt_nodaftar.Text = "" Or cmb_studi.Text = "" Or txt_nama.Text = "" Or jenisKelamin = "" Or txt_tmptlahir.Text = "" Or cmb_agama.Text = "" Or txt_alamat.Text = "" Or txt_notelp.Text = "" Or txt_email.Text = "" Then
            MsgBox("Isi data dengan lengkap, dan pastikan tidak ada yang kosong!", MsgBoxStyle.Information, "ERROR")
        Else
            Call getInput()
            Dim dialogRes As DialogResult
            dialogRes = MessageBox.Show("Apakah anda yakin ingin menghapus mahasiswa bernama " & namaMahasiswa & "?", "DELETE", MessageBoxButtons.YesNo)
            If dialogRes = DialogResult.Yes Then
                Call connection()
                sql = "DELETE FROM mahasiswa WHERE no_pendaftaran = '" & noPendaftaran & "'"
                cmd = New MySqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Data dengan nama '" & namaMahasiswa & "' telah terhapus", MsgBoxStyle.MsgBoxRight, "SUCCESS")
                Call displayData()
            End If
        End If
    End Sub

    Sub searchData()
        Call connection()
        search = txt_search.Text
        sql = "SELECT * FROM mahasiswa WHERE no_pendaftaran LIKE '%" & search & "%' OR program_studi LIKE '%" & search & "%' OR nama_siswa LIKE '%" & search & "%' OR jenis_kelamin LIKE '%" & search & "%' OR tempat_lahir LIKE '%" & search & "%' OR tanggal_lahir LIKE '%" & search & "%' OR agama LIKE '%" & search & "%' OR alamat LIKE '%" & search & "%' OR no_telepon LIKE '%" & search & "%' OR email LIKE '%" & search & "%'"
        cmd = New MySqlCommand(sql, conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            Call connection()
            da = New MySqlDataAdapter(sql, conn)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds)
            dgv_mahasiswa.DataSource = ds.Tables(0)
        Else
            MsgBox("Data tidak ditemukan!", MsgBoxStyle.Information, "ERROR")
        End If
    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call displayData()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        jenisKelamin = "Laki - Laki"
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        jenisKelamin = "Perempuan"
    End Sub

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        saveData()
    End Sub

    Private Sub txt_notelp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_notelp.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then
            e.Handled = True
        End If
    End Sub

    Private Sub dgv_mahasiswa_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_mahasiswa.CellContentClick
        Call getData()
    End Sub

    Private Sub dgv_mahasiswa_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_mahasiswa.RowHeaderMouseClick
        Call getData()
    End Sub

    Private Sub btn_edit_Click(sender As Object, e As EventArgs) Handles btn_edit.Click
        Call editData()
    End Sub

    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        Call deleteData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call clear()
    End Sub

    Private Sub btn_exit_Click(sender As Object, e As EventArgs) Handles btn_exit.Click
        Close()
    End Sub

    Private Sub btn_search_Click(sender As Object, e As EventArgs) Handles btn_search.Click
        Call searchData()
    End Sub
End Class
