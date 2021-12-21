
Imports System.Data.SqlClient
Public Class Books
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\BookShopVB.mdf;Integrated Security=True;Connect Timeout=30")

    Private Sub Populate()
        Con.Open()
        Dim query = "select * from BookTB1"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        BooksDGV.DataSource = ds.Tables(0)

        Con.Close()
    End Sub
    Private Sub Fillter()
        Con.Open()
        Dim query = "select * from BookTB1 where Category= '" & FillterCb.SelectedItem & "'"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        BooksDGV.DataSource = ds.Tables(0)

        Con.Close()
    End Sub


    Private Sub reset()
        BookNameTb.Text = ""
        AuthorTb.Text = ""
        QuaTb.Text = ""
        PriceTb.Text = ""
        CatCb.SelectedIndex = -1
        Key = 0
    End Sub

    'Save
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If BookNameTb.Text = "" Or AuthorTb.Text = "" Or QuaTb.Text = "" Or PriceTb.Text = "" Or CatCb.SelectedIndex = -1 Then
            MsgBox("Missing Information")
        Else
            Con.Open()

            Dim query As String
            query = "insert into BookTB1 values ('" & BookNameTb.Text & "','" & AuthorTb.Text & "','" & CatCb.SelectedItem.ToString & "','" & QuaTb.Text & "' ," & PriceTb.Text & ")"

            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Book Saved Successfully")
            Con.Close()
            Populate()
            Reset()

        End If
    End Sub


    Private Sub BooksDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles BooksDGV.CellMouseClick
        Dim row As DataGridViewRow = BooksDGV.Rows(e.RowIndex)
        BookNameTb.Text = row.Cells(1).Value.ToString
        AuthorTb.Text = row.Cells(2).Value.ToString
        CatCb.SelectedItem = row.Cells(3).Value.ToString
        QuaTb.Text = row.Cells(4).Value.ToString
        PriceTb.Text = row.Cells(5).Value.ToString
        If BookNameTb.Text = "" Then
            Key = 0
        Else
            Key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub Books_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Populate()

    End Sub

    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        reset()

    End Sub

    Dim Key = 0
    'Delete
    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If Key = 0 Then
            MsgBox("Select The Book To Be Deleted")
        Else
            Con.Open()

            Dim query As String
            query = "Delete from BookTB1 where Bid=" & Key & ""

            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Book Deleted Successfully")
            Con.Close()
            Populate()
            reset()

        End If
    End Sub

    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
        If BookNameTb.Text = "" Or AuthorTb.Text = "" Or QuaTb.Text = "" Or PriceTb.Text = "" Or CatCb.SelectedIndex = -1 Then
            MsgBox("Missing Information")
        Else
            Con.Open()

            Dim query As String
            query = "Update BookTB1 set Title='" & BookNameTb.Text & "', Author='" & AuthorTb.Text & "', Category='" & CatCb.SelectedItem.ToString & "', Quantity='" & QuaTb.Text & "', Price='" & PriceTb.Text & "' where Bid='" & Key & "'"

            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Book Updated Successfully")
            Con.Close()
            Populate()
            reset()

        End If
    End Sub

    Private Sub ComboBox2_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles FillterCb.SelectionChangeCommitted
        Fillter()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Populate()
    End Sub

    Private Sub BookNameTb_TextChanged(sender As Object, e As EventArgs) Handles BookNameTb.TextChanged

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

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub
End Class