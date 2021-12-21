Imports System.Data.SqlClient

Public Class DashBoard
    'Connections
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\BookShopVB.mdf;Integrated Security=True;Connect Timeout=30")

    Private Sub BookCount()
        Dim BookNum As Integer
        Con.Open()

        Dim sql = "select COUNT(*) from BookTB1"
        Dim cmd As SqlCommand
        cmd = New SqlCommand(sql, Con)
        BookNum = cmd.ExecuteScalar
        BookLB.Text = BookNum
        Con.Close()
    End Sub
    Private Sub UserCount()
        Dim UserNum As Integer
        Con.Open()

        Dim sql = "select COUNT(*) from UserTB1"
        Dim cmd As SqlCommand
        cmd = New SqlCommand(sql, Con)
        UserNum = cmd.ExecuteScalar
        UserLB.Text = UserNum
        Con.Close()
    End Sub


    Private Sub AmountCount()
        Dim Amount As Integer
        Con.Open()

        Dim sql = "select Sum(Amount) from BillTB1"
        Dim cmd As SqlCommand
        cmd = New SqlCommand(sql, Con)
        Amount = cmd.ExecuteScalar
        AmountLB.Text = Amount
        Con.Close()
    End Sub

    Private Sub DashBoard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BookCount()
        UserCount()
        AmountCount()

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Dim obj = New Books()
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        Dim obj = New Users()
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Dim obj = New DashBoard()
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Dim obj = New Login()
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Application.Exit()
    End Sub
End Class