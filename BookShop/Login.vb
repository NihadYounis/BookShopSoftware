
Imports System.Data.SqlClient
Public Class Login
    'Connections
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\BookShopVB.mdf;Integrated Security=True;Connect Timeout=30")
    Dim cmd As SqlCommand

    Private Sub LoginBtn_Click(sender As Object, e As EventArgs) Handles LoginBtn.Click
        If UserNameTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("Enter UserName And Password")
        Else
            Con.Open()
            Dim query = "select * from UserTB1 where Name='" & UserNameTb.Text & "' and Password= '" & PasswordTb.Text & "' "
            cmd = New SqlCommand(query, Con)
            Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim ds As DataSet = New DataSet()
            sda.Fill(ds)
            Dim a As Integer
            a = ds.Tables(0).Rows.Count
            If a = 0 Then
                MsgBox("Wrong UserName OR Password")
            Else
                Dim Bill = New Bills
                Bill.UserName = UserNameTb.Text
                Bill.Show()
                Me.Hide()
            End If
            Con.Close()

        End If
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Dim obj = New Admin()
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Application.Exit()

    End Sub


End Class