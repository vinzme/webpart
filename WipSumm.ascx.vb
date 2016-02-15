#Region "Imports"

Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Security.Principal
Imports System.Web.UI.Page
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

#End Region

Partial Class WipSumm
    Inherits System.Web.UI.UserControl

    Dim pubUser As String
    Dim pubDestin As String
    Dim pubConnStr As String
    Dim pubUserBranch As String
    Dim pubOrderType As String
    Dim pubExpGroup As String
    Dim pubSumAmount As Double = 0
    Dim pubCheckglcode As String
    Dim pubWipsummdes As String
    Dim pubDebit As Double = 0
    Dim pubCredit As Double = 0
    Dim pubLoadSwitch As String
    Dim pubDebitTotal As Double = 0
    Dim pubCreditTotal As Double = 0
    Dim pubDebitTotal2 As Double = 0
    Dim pubCreditTotal2 As Double = 0
    Dim pubCountOrderProcess As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim sUser() As String = Split(Page.User.Identity.Name, "\")

        Dim sDomain As String = sUser(0)
        Dim sUserId As String = sUser(1)

        pubUser = UCase(sUserId)
        'pubuser = "HUSSEIN.MOHAMMED"

        Label5.Text = pubUser.Trim

        CheckConnectionString()

        CheckProcessedOrder()

        If pubCountOrderProcess = False Then
            CheckLoadSwitch()
            If pubLoadSwitch = "1" Then
                CheckOrderNo()

                DeletefromAndWipSumm()

                ProcessWipSumm()
                ProcessWipSummTwo()
                ProcessWipSummThree()
                ProcessWipSummFour()
            End If
        End If

    End Sub

    Private Sub CheckConnectionString()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try

            sSql = "Select SES.scheme.and_users.destin, SES.scheme.and_users.user_branch from SES.scheme.and_users WHERE " & _
                    "SES.scheme.and_users.userid='" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    pubDestin = mReader("destin")
                    pubUserBranch = mReader("user_branch")

                    Select Case mReader("user_branch")
                        Case "012"
                            pubConnStr = "Data Source=SESISVJBO;User ID=scheme;Password=Er1c550n2"
                            GridView1.DataSourceID = "ObjectDataSource5"
                            GridView2.DataSourceID = "ObjectDataSource6"
                        Case "013"
                            pubConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"
                            GridView1.DataSourceID = "ObjectDataSource3"
                            GridView2.DataSourceID = "ObjectDataSource4"
                        Case Else
                            pubConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
                            GridView1.DataSourceID = "ObjectDataSource1"
                            GridView2.DataSourceID = "ObjectDataSource2"
                    End Select
                End While
            Else
                Response.Redirect("MgtUnauthorized.aspx")
            End If

        Catch ex As Exception
            MySqlConn.Close()
            Response.Redirect("MgtUnauthorized.aspx")
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckOrderNo()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select order_no, order_type from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    Label2.Text = mReader("order_no")
                    pubOrderType = mReader("order_type")
                    Select Case pubOrderType.Trim
                        Case "ACAC"
                            Label4.Text = "Current Account"
                        Case "AGSM"
                            Label4.Text = "GSM"
                        Case "AINS"
                            Label4.Text = "Installation"
                        Case "AREP"
                            Label4.Text = "Repair Workshop"
                        Case "ASIN"
                            Label4.Text = "Supply & Installation"
                        Case "ASUP"
                            Label4.Text = "Supply Only"
                        Case "BCON"
                            Label4.Text = "Consignment"
                        Case "BEXP"
                            Label4.Text = "Expense"
                        Case "BSPR"
                            Label4.Text = "Spare"
                        Case "AMNC"
                            Label4.Text = "Maintenance"
                        Case "BWAR"
                            Label4.Text = "Warranty"
                    End Select
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub ProcessWipSumm()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "SELECT expense_group, glcode, Sum(amount) AS SumOfamount From SES.scheme.prtrm_a Where " & _
                    "substring(project, 1, 6) = '" & Label2.Text.Trim & "' and remap <> 'y' GROUP BY expense_group, glcode"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    If pubOrderType.Trim <> "ASIN" Then

                        If Trim(mReader("expense_group")) <> "ADV" And _
                            Trim(mReader("expense_group")) <> "TRF" And _
                            Trim(mReader("expense_group")) <> "DSC" And _
                            Trim(mReader("expense_group")) <> "DIR" Then

                            pubExpGroup = Trim(mReader("expense_group"))
                            pubSumAmount = mReader("SumOfamount")
                            If Mid(mReader("glcode"), 13, 4) = "4022" Then
                                pubCheckglcode = "Y"
                            Else
                                pubCheckglcode = "N"
                            End If

                            WipSummDes()

                            'add records to and_wipsumm
                            If pubSumAmount > 0 Then
                                pubDebit = pubSumAmount
                                pubCredit = 0
                            Else
                                pubCredit = pubSumAmount
                                pubDebit = 0
                            End If

                            InsertIntoWipSumm()

                        End If

                    Else

                        If Trim(mReader("expense_group")) <> "ADV" And _
                            Trim(mReader("expense_group")) <> "TRF" And _
                            Trim(mReader("expense_group")) <> "DSC" And _
                            Trim(mReader("expense_group")) <> "DIR" And _
                            Trim(mReader("expense_group")) <> "PEX" And _
                            Trim(mReader("expense_group")) <> "PRV" Then

                            pubExpGroup = Trim(mReader("expense_group"))
                            pubSumAmount = mReader("SumOfamount")

                            If Mid(mReader("glcode"), 13, 4) = "4022" Then
                                pubCheckglcode = "Y"
                            Else
                                pubCheckglcode = "N"
                            End If

                            WipSummDes()

                            'add records to and_wipsumm
                            If pubSumAmount > 0 Then
                                pubDebit = pubSumAmount
                                pubCredit = 0
                            Else
                                pubCredit = pubSumAmount
                                pubDebit = 0
                            End If
                            InsertIntoWipSumm()

                        End If
                    End If

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub WipSummDes()
        Select Case pubExpGroup
            Case "ABS"
                pubWipsummdes = "ABSORBED BURDEN"
            Case "COM"
                pubWipsummdes = "COMMISSION"
            Case "DIR"
                pubWipsummdes = "DIRECT COST :"
            Case "EXP"
                pubWipsummdes = "CHARGE TO EXPENSE"
            Case "INV"
                pubWipsummdes = "INVENTORY"
            Case "ITB"
                pubWipsummdes = "TRANSFER FROM ORDER"
            Case "PEX"
                pubWipsummdes = "CHARGE TO PROVISION :"
            Case "PRV"
                pubWipsummdes = "PROVISION :"
            Case "WAR"
                pubWipsummdes = "WARRANTY"
            Case "WAX"
                pubWipsummdes = "CHARGE TO WARRANTY"
        End Select
    End Sub

    Private Sub InsertIntoWipSumm()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into SES.scheme.and_wipsumm(expense_code,description,debit,credit," & _
                                "orderno,cost_after,compname) values('" & pubExpGroup.Trim & "','" & pubWipsummdes.Trim & "'," & _
                                pubDebit & "," & pubCredit & ",'" & Label2.Text.Trim & "','" & pubCheckglcode.Trim & "','" & _
                                pubUser.Trim & "')"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ProcessWipSummTwo()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "SELECT expense_group, glcode, Sum(amount) AS SumOfamount " & _
                    "From SES.scheme.prtrm_a Where project = '" & Label2.Text.Trim & _
                    "/I' and (expense_group = 'DIR' or expense_group = 'PEX' " & _
                    "or expense_group = 'PRV') and remap <> 'y' GROUP BY expense_group, glcode"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then

                While mReader.Read()

                    pubExpGroup = Trim(mReader("expense_group"))
                    pubSumAmount = mReader("SumOfamount")

                    If Mid(mReader("glcode"), 13, 4) = "4022" Then
                        pubCheckglcode = "Y"
                    Else
                        pubCheckglcode = "N"
                    End If

                    WipSummDes()

                    pubWipsummdes = pubWipsummdes + " IMPLEMENTATION"

                    'add records to and_wipsumm
                    If pubSumAmount > 0 Then
                        pubDebit = pubSumAmount
                        pubCredit = 0
                    Else
                        pubCredit = pubSumAmount
                        pubDebit = 0
                    End If

                    InsertIntoWipSumm()

                End While

            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub ProcessWipSummThree()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "SELECT expense_group, glcode, Sum(amount) AS SumOfamount " & _
                    "From SES.scheme.prtrm_a Where project = '" & Label2.Text.Trim & _
                    "/S' and (expense_group = 'DIR' or expense_group = 'PEX' " & _
                    "or expense_group = 'PRV') and remap <> 'y' GROUP BY expense_group, glcode"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    pubExpGroup = Trim(mReader("expense_group"))
                    pubSumAmount = mReader("SumOfamount")
                    If Mid(mReader("glcode"), 13, 4) = "4022" Then
                        pubCheckglcode = "Y"
                    Else
                        pubCheckglcode = "N"
                    End If

                    WipSummDes()

                    If pubExpGroup = "DIR" Then
                        pubWipsummdes = pubWipsummdes + " SALES DIVISION"
                    Else
                        pubWipsummdes = pubWipsummdes + " MATERIAL"
                    End If

                    'add records to and_wipsumm
                    If pubSumAmount > 0 Then
                        pubDebit = pubSumAmount
                        pubCredit = 0
                    Else
                        pubCredit = pubSumAmount
                        pubDebit = 0
                    End If

                    InsertIntoWipSumm()

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub ProcessWipSummFour()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "SELECT expense_group, glcode, Sum(amount) AS SumOfamount " & _
                    "From SES.scheme.prtrm_a Where project = '" & Label2.Text.Trim & _
                    "' and expense_group = 'DIR' and remap <> 'y' GROUP BY expense_group, glcode"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    pubExpGroup = Trim(mReader("expense_group"))
                    pubSumAmount = mReader("SumOfamount")


                    If Mid(mReader("glcode"), 13, 4) = "4022" Then
                        pubCheckglcode = "Y"
                    Else
                        pubCheckglcode = "N"
                    End If

                    pubWipsummdes = "DIRECT COST : MATERIAL"

                    'add records to and_wipsumm
                    If pubSumAmount > 0 Then
                        pubDebit = pubSumAmount
                        pubCredit = 0
                    Else
                        pubCredit = pubSumAmount
                        pubDebit = 0
                    End If

                    InsertIntoWipSumm()

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub DeletefromAndWipSumm()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "delete from SES.scheme.and_wipsumm where compname = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub CheckLoadSwitch()
        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            If pubDestin = "I" Then
                sSql = "Select order_type, substring(loadaccess_bwar,12,1) as loadaccess_bwar, substring(loadswitch,12,1) as loadswitches " & _
                        "from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"
            Else
                sSql = "Select substring(loadswitch,12,1) as loadswitches from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"
            End If

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If pubDestin = "I" Then
                        If Trim(mReader("order_type")) = "BWAR" Then
                            pubLoadSwitch = mReader("loadaccess_bwar")
                        Else
                            pubLoadSwitch = mReader("loadswitches")
                        End If
                    Else
                        pubLoadSwitch = mReader("loadswitches")
                    End If
                End While
            Else
                pubLoadSwitch = "0"
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            ' add the backlogTotal to the running total variables
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "expense_code")) Then

                If DataBinder.Eval(e.Row.DataItem, "debit") = 0 Then
                    e.Row.Cells(2).Text = " "
                Else
                    pubDebitTotal = pubDebitTotal + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "debit")))
                    e.Row.Cells(2).Text = Format(DataBinder.Eval(e.Row.DataItem, "debit"), "##,##0.00;(##,##0.00)")
                End If

                If DataBinder.Eval(e.Row.DataItem, "credit") = 0 Then
                    e.Row.Cells(3).Text = " "
                Else
                    pubCreditTotal = pubCreditTotal + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "credit")))
                    e.Row.Cells(3).Text = Format(DataBinder.Eval(e.Row.DataItem, "credit"), "##,##0.00;(##,##0.00)")
                End If

            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Total :"
            ' for the Footer, display the running totals
            e.Row.Cells(2).Text = Format(pubDebitTotal, "##,##0.00;(##,##0.00)")
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            'e.Row.Cells(5).Font.Size = 8

            e.Row.Cells(3).Text = Format(pubCreditTotal, "##,##0.00;(##,##0.00)")
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            'e.Row.Cells(6).Font.Size = 8

        End If
    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            ' add the backlogTotal to the running total variables
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "expense_code")) Then

                If DataBinder.Eval(e.Row.DataItem, "debit") = 0 Then
                    e.Row.Cells(2).Text = " "
                Else
                    pubDebitTotal2 = pubDebitTotal2 + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "debit")))
                    e.Row.Cells(2).Text = Format(DataBinder.Eval(e.Row.DataItem, "debit"), "##,##0.00;(##,##0.00)")
                End If

                If DataBinder.Eval(e.Row.DataItem, "credit") = 0 Then
                    e.Row.Cells(3).Text = " "
                Else
                    pubCreditTotal2 = pubCreditTotal2 + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "credit")))
                    e.Row.Cells(3).Text = Format(DataBinder.Eval(e.Row.DataItem, "credit"), "##,##0.00;(##,##0.00)")
                End If

            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Total :"
            ' for the Footer, display the running totals
            e.Row.Cells(2).Text = Format(pubDebitTotal2, "##,##0.00;(##,##0.00)")
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            'e.Row.Cells(5).Font.Size = 8

            e.Row.Cells(3).Text = Format(pubCreditTotal2, "##,##0.00;(##,##0.00)")
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            'e.Row.Cells(6).Font.Size = 8

        End If
    End Sub

    Private Sub CheckProcessedOrder()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select order_process, click from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If mReader("click") = "G" Then
                        pubCountOrderProcess = False
                    Else
                        If mReader("order_process") > 2 Then
                            pubCountOrderProcess = True
                        Else
                            pubCountOrderProcess = False
                        End If
                    End If
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

End Class