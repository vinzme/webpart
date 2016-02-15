#Region "Imports"

Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Security.Principal
Imports System.Web.UI.Page
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

#End Region

Partial Class AccountsRec
    Inherits System.Web.UI.UserControl

    Dim pubUser As String
    Dim pubDestin As String
    Dim pubConnStr As String
    Dim pubUserBranch As String
    Dim pubOrderType As String

    Dim pubArAmountTotal As Double = 0
    Dim pubArPaidTotal As Double = 0
    Dim pubArAdjustmentTotal As Double = 0
    Dim pubArOutstandingTotal As Double = 0

    Dim pubInvAmount As Double
    Dim pubInvPayment As Double


    Dim pubGridInvAmount As Double = 0
    Dim pubGridInvPayment As Double = 0

    Dim pubxMonth As String
    Dim pubStrMonth As String
    Dim pubStrDate As String

    Dim pubKind As String

    Dim pubInvdate As Date
    Dim pubItemno As String
    Dim pubAmount As Double = 0
    Dim pubOpen_indicator As String
    Dim pubCustomer As String
    Dim pubPaid As Double = 0

    Dim pubOutstanding As Double = 0

    Dim pubRecordCounter As Integer = 0

    Dim pubEXDbatchno As String
    Dim pubAdjustment As Double = 0
    Dim pubBatch_amount As Double = 0
    Dim pubForeignCurr As Boolean = False
    Dim pubAllocated As Double = 0
    Dim pubBatchMinusAmount As Double = 0
    Dim pubBatchMinusAmount2 As Double = 0
    Dim pubBatchMinusAmount3 As Double = 0
    Dim pubLoadSwitch As String
    Dim pubCountOrderProcess As Boolean = False
    Dim pubWipCounter As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim sUser() As String = Split(Page.User.Identity.Name, "\")

        Dim sDomain As String = sUser(0)
        Dim sUserId As String = sUser(1)

        pubUser = UCase(sUserId)
        'pubuser = "HUSSEIN.MOHAMMED"

        CheckConnectionString()

        CheckProcessedOrder()

        If pubCountOrderProcess = False Then
            CheckLoadSwitch()
            If pubLoadSwitch = "1" Then
                CheckOrderNo()
                Label5.Text = pubUser.Trim
                DeletefromAndAr()
                ProcessAr()
                Label7.Text = ""
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

    Private Sub DeletefromAndAr()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "delete from SES.scheme.and_ar where compname = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub DeletefromInvDetails()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "delete from SES.scheme.and_soarinv where computername = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ProcessAr()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select dated, item_no, amount, open_indicator, customer from SES.scheme.sldetails where refernce = '" & _
                    Label2.Text.Trim & "' order by dated"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubInvdate = mReader("dated")
                    pubItemno = mReader("item_no")
                    pubAmount = mReader("amount")
                    pubOpen_indicator = mReader("open_indicator")
                    pubCustomer = mReader("customer")

                    ProcessStepOne()

                    ProcessStepFive()

                    If pubForeignCurr = True Then
                        If pubOpen_indicator = "C" Then
                            pubPaid = pubAmount - pubAdjustment
                        End If
                    End If

                    pubForeignCurr = False

                    pubOutstanding = pubAmount - pubPaid - pubAdjustment

                    ProcessInsertAr()

                    'initialize
                    pubOutstanding = 0
                    pubAmount = 0
                    pubPaid = 0
                    pubAdjustment = 0

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub ProcessStepOne()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select substring(batch_item_no,1,2) as batch_item_no, batch_item_no as batchno, " & _
                    "sum(allocated_amount) as allocated_amount from SES.scheme.slcshjrn " & _
                    "where transaction_item = '" & pubItemno.Trim & _
                    "' group by substring(batch_item_no,1,2), batch_item_no"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubEXDbatchno = mReader("batchno")
                    pubAllocated = mReader("allocated_amount")


                    GetREcordCounter()

                    If pubItemno.Trim = "026335" Then

                        pubItemno = "026335"
                    End If


                    If Trim(mReader("batch_item_no")) <> "SC" Then
                        pubAdjustment = pubAdjustment + mReader("allocated_amount")
                    Else
                        If pubRecordCounter >= 1 Then
                            pubBatch_amount = pubBatch_amount + mReader("allocated_amount")

                            pubBatchMinusAmount = pubBatch_amount - pubAmount
                            If pubBatchMinusAmount < 0 Then
                                pubBatchMinusAmount = pubBatchMinusAmount * -1
                            End If

                            If pubBatchMinusAmount < 2 Then

                                ProcessStepTwo()

                            Else
                                ProcessStepThree()

                            End If


                        Else

                            ProcessStepFour()

                        End If


                    End If


                End While
            Else
                If pubOpen_indicator.Trim = "C" Then
                    pubPaid = pubAmount
                Else
                    pubPaid = 0
                End If

            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub ProcessStepTwo()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select amount from SES.scheme.sldetails where item_no like '" & pubEXDbatchno.Trim & "%' and kind = 'EXD'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    pubForeignCurr = True

                    pubBatchMinusAmount2 = pubBatch_amount - pubAmount
                    If pubBatchMinusAmount2 < 0 Then
                        pubBatchMinusAmount2 = pubBatchMinusAmount2 * -1
                    End If

                    If pubBatchMinusAmount2 < 2 Then
                        pubPaid = pubAmount
                    Else
                        pubPaid = pubPaid + pubAllocated
                    End If

                End While

            Else
                pubPaid = pubPaid + pubAllocated
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub ProcessStepThree()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select amount from SES.scheme.sldetails where item_no like '" & _
                        pubEXDbatchno.Trim & "%' and kind = 'EXD' and customer = '" & pubCustomer & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    pubForeignCurr = True
                    If Fix(pubAmount) <> Fix(pubAllocated) Then
                        pubPaid = pubPaid + pubAllocated - mReader("amount")
                    Else
                        pubPaid = pubPaid + pubAllocated
                    End If

                End While

            Else
                pubPaid = pubPaid + pubAllocated
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub ProcessStepFour()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select amount from SES.scheme.sldetails where item_no like '" & pubEXDbatchno.Trim & "%' and kind = 'EXD'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    pubBatchMinusAmount3 = pubAllocated - pubAmount
                    If pubBatchMinusAmount3 < 0 Then
                        pubBatchMinusAmount3 = pubBatchMinusAmount3 * -1
                    End If

                    If pubBatchMinusAmount3 < 2 Then
                        pubPaid = pubPaid + pubAmount
                    Else
                        pubPaid = pubPaid + pubAllocated
                    End If

                End While

            Else
                pubPaid = pubPaid + pubAllocated
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub ProcessStepFive()


        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            'check if mpaid is equal to slcshjrn    
            sSql = "Select sum(allocated_amount) as sumamount from SES.scheme.slcshjrn where transaction_item = '" & pubItemno & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    If Not IsDBNull(mReader("sumamount")) Then

                        If Fix(pubPaid) <> Fix(mReader("sumamount")) Then
                            ProcessStepSix()
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

    Private Sub ProcessStepSix()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select sum(amount) as sumamount from SES.scheme.sldetails where refernce = '" & pubItemno & "' and kind = 'CSH'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    If Not IsDBNull(mReader("sumamount")) Then
                        pubPaid = pubPaid - mReader("sumamount")
                    End If

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub ProcessInsertAr()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into SES.scheme.and_ar(orderno,invoice_no,invdate,amount,paid,adjustment,outstanding,compname) " & _
                                "values('" & Label2.Text.Trim & "','" & pubItemno & "','" & pubInvdate & "'," & pubAmount & _
                                "," & pubPaid & "," & pubAdjustment & "," & pubOutstanding & ",'" & pubUser.Trim & "')"
        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Label7.Text = GridView1.SelectedRow.Cells(2).Text.Trim

        DeletefromInvDetails()
        ProcessInvDetails()
        GridView2.DataBind()

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            pubWipCounter = pubWipCounter + 1
            If pubWipCounter = 20 Then
                Panel1.ScrollBars = ScrollBars.Vertical
            End If

            ' add the backlogTotal to the running total variables
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "invdate")) Then
                e.Row.Cells(1).Text = Year(DataBinder.Eval(e.Row.DataItem, "invdate")).ToString & _
                    "/" & Month(DataBinder.Eval(e.Row.DataItem, "invdate")).ToString & _
                    "/" & Day(DataBinder.Eval(e.Row.DataItem, "invdate")).ToString

                e.Row.Cells(3).Text = Format(Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "aramount")), "##,##0.00")
                e.Row.Cells(4).Text = Format(DataBinder.Eval(e.Row.DataItem, "paid"), "##,##0.00")
                e.Row.Cells(5).Text = Format(DataBinder.Eval(e.Row.DataItem, "adjustment"), "##,##0.00")
                e.Row.Cells(6).Text = Format(DataBinder.Eval(e.Row.DataItem, "outstanding"), "##,##0.00")

                pubArAmountTotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "aramount"))
                pubArPaidTotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "paid"))
                pubArAdjustmentTotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "adjustment"))
                pubArOutstandingTotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "outstanding"))

            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Total :"
            ' for the Footer, display the running totals
            e.Row.Cells(3).Text = Format(pubArAmountTotal, "##,##0.00")
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).Font.Size = 8

            e.Row.Cells(4).Text = Format(pubArPaidTotal, "##,##0.00")
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).Font.Size = 8

            e.Row.Cells(5).Text = Format(pubArAdjustmentTotal, "##,##0.00")
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).Font.Size = 8

            e.Row.Cells(6).Text = Format(pubArOutstandingTotal, "##,##0.00")
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(6).Font.Size = 8

        End If

    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            ' add the backlogTotal to the running total variables
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "invdate")) Then

                e.Row.Cells(0).Text = Year(DataBinder.Eval(e.Row.DataItem, "invdate")).ToString & _
                    "/" & Month(DataBinder.Eval(e.Row.DataItem, "invdate")).ToString & _
                    "/" & Day(DataBinder.Eval(e.Row.DataItem, "invdate")).ToString

                If DataBinder.Eval(e.Row.DataItem, "amount") = 0 Then
                    e.Row.Cells(2).Text = " "
                Else
                    e.Row.Cells(2).Text = Format(DataBinder.Eval(e.Row.DataItem, "amount"), "##,##0.00")
                End If

                If DataBinder.Eval(e.Row.DataItem, "payment") = 0 Then
                    e.Row.Cells(3).Text = " "
                Else
                    e.Row.Cells(3).Text = Format(DataBinder.Eval(e.Row.DataItem, "payment"), "##,##0.00;(##,##0.00)")
                End If

                pubGridInvAmount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "amount"))
                pubGridInvPayment += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "payment"))

            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Total :"
        ' for the Footer, display the running totals
            e.Row.Cells(2).Text = Format(pubGridInvAmount, "##,##0.00")
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).Font.Size = 8

            e.Row.Cells(3).Text = Format(pubGridInvPayment, "##,##0.00;(##,##0.00)")
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).Font.Size = 8

        End If

    End Sub

    Private Sub ProcessInvDetails()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select customer, dated, kind, amount from SES.scheme.sldetails where item_no = '" & Label7.Text.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubCustomer = mReader("customer")
                    pubInvdate = mReader("dated")
                    pubInvAmount = mReader("amount")
                    pubKind = mReader("kind")
                    pubInvPayment = 0

                    InsertInvDetails()

                    ProcessInvDetails2()

                    ProcessInvDetails4()

                End While

            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try


    End Sub

    Private Sub InsertInvDetails()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into SES.scheme.and_soarinv(invoice_no,invdate,kind,amount,payment,computername) " & _
                                "values('" & Label7.Text.Trim & "','" & pubInvdate & "','" & pubKind & "'," & pubInvAmount & _
                                "," & pubInvPayment & ",'" & pubUser.Trim & "')"
        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertInvDetails2()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into SES.scheme.and_soarinv(invoice_no,invdate,kind,amount,payment,computername) " & _
                                "values('" & Label7.Text.Trim & "','" & pubStrDate & "','" & pubKind & "'," & pubInvAmount & _
                                "," & pubInvPayment & ",'" & pubUser.Trim & "')"
        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertInvDetails3()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into SES.scheme.and_soarinv(invoice_no,invdate,kind,amount,payment,computername) " & _
                                "values('" & Label7.Text.Trim & "','" & pubInvdate & "','EXD'," & pubInvAmount & _
                                "," & pubInvPayment & ",'" & pubUser.Trim & "')"
        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ProcessInvDetails2()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            'get data from slcshjrn
            sSql = "Select batch_item_no, allocated_date, allocated_amount from SES.scheme.slcshjrn where " & _
                    "transaction_item = '" & Label7.Text.Trim & "' and customer = '" & _
                    pubCustomer & "'  order by unique_no"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    pubItemno = mReader("batch_item_no")

                    If Mid(mReader("batch_item_no"), 1, 2) = "SJ" Then
                        ProcessInvDetails3()
                    Else
                        pubKind = "CSH"
                    End If

                    pubxMonth = Mid(mReader("allocated_date"), 4, 2)
                    GetMonth()

                    pubStrDate = Mid(mReader("allocated_date"), 1, 2) & " " & _
                          pubStrMonth & " " & Mid(mReader("allocated_date"), 7, 2)


                    If mReader("allocated_amount") > 0 Then
                        pubInvAmount = mReader("allocated_amount") * -1
                    Else
                        pubInvAmount = mReader("allocated_amount")
                    End If

                    pubInvPayment = pubInvAmount
                    pubInvAmount = 0

                    InsertInvDetails2()

                End While

            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub ProcessInvDetails3()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select kind, analysis_codes3 from SES.scheme.sldetails where item_no = '" & _
                pubItemno.Trim & "' and customer = '" & pubCustomer & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubKind = mReader("kind")
                End While
            Else
                pubKind = " "
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetMonth()

        Select Case Val(Trim(pubxMonth))
            Case 1
                pubStrMonth = "January"
            Case 2
                pubStrMonth = "February"
            Case 3
                pubStrMonth = "March"
            Case 4
                pubStrMonth = "April"
            Case 5
                pubStrMonth = "May"
            Case 6
                pubStrMonth = "June"
            Case 7
                pubStrMonth = "July"
            Case 8
                pubStrMonth = "August"
            Case 9
                pubStrMonth = "September"
            Case 10
                pubStrMonth = "October"
            Case 11
                pubStrMonth = "November"
            Case 12
                pubStrMonth = "December"
        End Select
    End Sub

    Private Sub ProcessInvDetails4()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            'get EXD if foreign currency
            sSql = "Select batch_item_no from SES.scheme.slcshjrn where " & _
                "transaction_item = '" & Label7.Text.Trim & "' and customer = '" & _
                pubCustomer & "' and substring(batch_item_no,1,2) = 'SC' order by unique_no"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubItemno = mReader("batch_item_no")

                    ProcessInvDetails5()

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub ProcessInvDetails5()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            'get EXD if foreign currency
            sSql = "Select amount, dated from SES.scheme.sldetails where kind = 'EXD' and item_no = '" & _
                                pubItemno.Trim & "/E'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubInvdate = mReader("dated")
                    pubInvAmount = mReader("amount")
                    pubInvPayment = pubInvAmount
                    pubInvAmount = 0

                    InsertInvDetails3()

                End While
            End If
        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetREcordCounter()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select substring(batch_item_no,1,2) as batch_item_no, batch_item_no as batchno, " & _
                    "sum(allocated_amount) as allocated_amount from SES.scheme.slcshjrn " & _
                    "where transaction_item = '" & pubItemno.Trim & _
                    "' group by substring(batch_item_no,1,2), batch_item_no"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubRecordCounter = pubRecordCounter + 1

                End While
            End If
        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

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
                sSql = "Select order_type, substring(loadaccess_bwar,4,1) as loadaccess_bwar, substring(loadswitch,4,1) as loadswitches " & _
                        "from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"
            Else
                sSql = "Select substring(loadswitch,4,1) as loadswitches from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"
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
