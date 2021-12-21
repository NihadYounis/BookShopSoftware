
Imports System.Data.SqlClient
Public Class Bills

    'Connections
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\BookShopVB.mdf;Integrated Security=True;Connect Timeout=30")


    Public Property UserName As String
    'To Insert Into Grid TAble
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

    Private Sub Update()
        Dim NewQuan = Stock - Convert.ToInt32(QuanTb.Text)
        Con.Open()
        Dim query As String
        query = "Update BookTB1 set Quantity=" & NewQuan & "  where Bid='" & Key & "'"

        Dim cmd As SqlCommand
        cmd = New SqlCommand(query, Con)
        cmd.ExecuteNonQuery()
        MsgBox("Book Updated Successfully")
        Con.Close()
        Populate()

    End Sub

    Private Sub Reset()
        Key = 0
        QuanTb.Text=""
        PriceTb.Text = ""
        ClientTb.Text = ""
        BNameTb.Text = ""
    End Sub
    Private Sub Bills_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Populate()
        UNameL.Text = UserName
    End Sub
    Dim Key = 0, Stock = 0, i = 0, GrdTotal = 0

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawString("BookShop", New Font("Century Gothic", 25), Brushes.MidnightBlue, 350, 40)
        e.Graphics.DrawString("=======Your Bill=======", New Font("Century Gothic", 16), Brushes.MidnightBlue, 300, 70)
        Dim bm As New Bitmap(Me.BillDGV.Width, Me.BillDGV.Height)
        BillDGV.DrawToBitmap(bm, New Rectangle(0, 0, Me.BillDGV.Width, Me.BillDGV.Height))
        e.Graphics.DrawImage(bm, 60, 120)
        e.Graphics.DrawString("Total Amount Rs " + GrdTotal.ToString, New Font("Century Gothic", 15), Brushes.MidnightBlue, 280, 500)
        e.Graphics.DrawString("==========Thanks For Buying In Our Shop==========", New Font("Century Gothic", 15), Brushes.MidnightBlue, 150, 580)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        PrintPreviewDialog1.Show()
        AddBill()

    End Sub

    Private Sub AddBill()
        Con.Open()

        Dim query As String
        query = "insert into BillTB1 values ('" & UNameL.Text & "','" & ClientTb.Text & "'," & GrdTotal & " )"
        Dim cmd As SqlCommand
        cmd = New SqlCommand(query, Con)
        cmd.ExecuteNonQuery()
        Con.Close()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Application.Exit()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Dim obj = New Login()
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub UNameL_Click(sender As Object, e As EventArgs) Handles UNameL.Click

    End Sub

    Private Sub BNameTb_TextChanged(sender As Object, e As EventArgs) Handles BNameTb.TextChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Reset()

    End Sub

    Private Sub AddToBill_Click(sender As Object, e As EventArgs) Handles AddToBill.Click
        If PriceTb.Text = "" Or QuanTb.Text = "" Then
            MsgBox("Enter the Quantity")

        ElseIf Key = 0 Then
            MsgBox("Select The Book")
        ElseIf Convert.ToInt32(QuanTb.Text) > Stock Then
            MsgBox("Not Enough Stock")
        Else
            Dim rnum As Integer = BillDGV.Rows.Add()
            i = i + 1
            Dim Total = Convert.ToInt32(QuanTb.Text) * Convert.ToInt32(PriceTb.Text)
            BillDGV.Rows.Item(rnum).Cells("Column1").Value = i
            BillDGV.Rows.Item(rnum).Cells("Column2").Value = BNameTb.Text
            BillDGV.Rows.Item(rnum).Cells("Column3").Value = PriceTb.Text
            BillDGV.Rows.Item(rnum).Cells("Column4").Value = QuanTb.Text
            BillDGV.Rows.Item(rnum).Cells("Column5").Value = Total
            GrdTotal = GrdTotal + Total
            TotalL.Text = "Rs" + Convert.ToString(GrdTotal)
            Update()

        End If
    End Sub

    Private Sub BooksDGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BooksDGV.CellContentClick
        Dim row As DataGridViewRow = BooksDGV.Rows(e.RowIndex)
        BNameTb.Text = row.Cells(1).Value.ToString
        PriceTb.Text = row.Cells(5).Value.ToString
        Stock = Convert.ToInt32(row.Cells(4).Value.ToString)


        If BNameTb.Text = "" Then
            Key = 0
        Else
            Key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub
End Class