

Imports System.Data.SqlClient

Public Class Users

    'Connections
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\BookShopVB.mdf;Integrated Security=True;Connect Timeout=30")

    'To Insert Into Grid TAble
    Private Sub Populate()
        Con.Open()
        Dim query = "select * from UserTB1"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        UserDGV.DataSource = ds.Tables(0)

        Con.Close()
    End Sub

    Private Sub reset()
        UnameTb.Text = ""
        PhoneTb.Text = ""
        AddressTb.Text = ""
        PasswordTb.Text = ""
        Key = 0
    End Sub

    'Save Button
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If UnameTb.Text = "" Or PhoneTb.Text = "" Or AddressTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Con.Open()

            Dim query As String
            query = "insert into UserTB1 values ('" & UnameTb.Text & "','" & PhoneTb.Text & "','" & AddressTb.Text & "','" & PasswordTb.Text & "')"

            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("User Saved Successfully")
            Con.Close()
            Populate()
            reset()

        End If
    End Sub

    'Load
    Private Sub Users_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Populate()

    End Sub

    'Var For Delete
    Dim Key = 0

    'Delete Button
    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If Key = 0 Then
            MsgBox("Select The User To Be Deleted")
        Else
            Con.Open()

            Dim query As String
            query = "Delete from UserTB1 where Id=" & Key & ""

            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("User Deleted Successfully")
            Con.Close()
            Populate()
            reset()

        End If
    End Sub


    'Click Cells
    Private Sub UserDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles UserDGV.CellMouseClick
        Dim row As DataGridViewRow = UserDGV.Rows(e.RowIndex)
        UnameTb.Text = row.Cells(1).Value.ToString
        PhoneTb.Text = row.Cells(2).Value.ToString
        AddressTb.Text = row.Cells(3).Value.ToString
        PasswordTb.Text = row.Cells(4).Value.ToString

        If UnameTb.Text = "" Then
            Key = 0
        Else
            Key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub


    'Reset Button
    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        reset()

    End Sub

    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
        If UnameTb.Text = "" Or PhoneTb.Text = "" Or AddressTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Con.Open()

            Dim query As String
            query = "Update UserTB1 set Name='" & UnameTb.Text & "', Phone='" & PhoneTb.Text & "', Address='" & AddressTb.Text & "', Password='" & PasswordTb.Text & "' where Id='" & Key & "'"

            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("User Updated Successfully")
            Con.Close()
            Populate()
            reset()

        End If
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Dim obj = New Books()
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Dim obj = New DashBoard()
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        Dim obj = New Users()
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Dim obj = New Login()
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub UnameTb_TextChanged(sender As Object, e As EventArgs) Handles UnameTb.TextChanged

    End Sub
End Class