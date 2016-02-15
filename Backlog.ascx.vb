#Region "Imports"

Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Security.Principal
Imports System.Web.UI.Page
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

#End Region

Partial Class BacklogToo
    Inherits System.Web.UI.UserControl

    Dim pubUser As String
    Dim pubDestin As String
    Dim pubConnStr As String
    Dim pubUserBranch As String
    Dim pubOrderType As String
    Dim pubProductGroup As String
    Dim pubSales As Double = 0
    Dim pubDiscount As Double = 0
    Dim pubSpare As Double = 0
    Dim pubSalesDiscount As Double = 0
    Dim pubCurrentSum As Double = 0
    Dim pubCurrentOb As Double = 0
    Dim pubLoadSwitch As String
    Dim pubGridSales As Double = 0
    Dim pubGridOB As Double = 0
    Dim pubGridBacklog As Double = 0
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
                DeletefromAndBacklog()
                ProcessBacklog()
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
                            GridView1.DataSourceID = "ObjectDataSource3"
                        Case "013"
                            pubConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"
                            GridView1.DataSourceID = "ObjectDataSource2"
                        Case Else
                            pubConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
                            GridView1.DataSourceID = "ObjectDataSource1"
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

    Private Sub DeletefromAndBacklog()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "delete from SES.scheme.and_backlog where comp_name = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ProcessBacklog()

        InsertIntoBacklogOne()
        InsertIntoBacklogTwo()
        InsertIntoBacklogThree()
        UpdateIntoBacklog()
        GetSalesDiscount()
        If pubOrderType.Trim = "ACAC" Then
            CheckIfCurrentAccount()
        End If
        UpdateFinalBacklog()

    End Sub

    Private Sub InsertIntoBacklogOne()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into SES.scheme.and_backlog(order_no,comp_name,product_group,description,orders_booked,sales,backlog) " & _
                                "Select a.order_no, '" & pubUser.Trim & "' as comp_name, a.transaction_anals3 as prd_grp,c.dsc as description," & _
                                "sum(case when spare1=0 then a.val else spare1*a.val end) as orders_booked, '0' as sales,'0' as backlog " & _
                                "from SES.scheme.opdetm a inner join SES.scheme.opheadm b on a.order_no=b.order_no inner join " & _
                                "SES.scheme.stkpgm c on a.transaction_anals3=c.product_group where a.order_no='" & Label2.Text.Trim & _
                                "' group by a.order_no, a.transaction_anals3,c.dsc order by min(a.order_line_no)"
        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertIntoBacklogTwo()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "Insert into SES.scheme.and_backlog(order_no,comp_name,product_group," & _
                                "description,orders_booked,sales,backlog) values " & _
                                "('" & Label2.Text.Trim & "','" & pubUser.Trim & "','ZZ','DISCOUNT',0,0,0)"
        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertIntoBacklogThree()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select analysis3 as prd_grp, sum(amount*-1) as sales from SES.scheme.prtrm_a where " & _
                    "(project = '" & Label2.Text.Trim & "' or project = '" & Label2.Text.Trim & "/I' or project = '" & _
                     Label2.Text.Trim & "/S') and expense_code in ('TRF-TRFSAL','TRF-MNTSAL','TRF-MATSAL','TRF-LABSAL') " & _
                    "group by analysis3"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubProductGroup = Trim(mReader("prd_grp"))
                    pubSales = mReader("sales")

                    UpdateSales()

                End While

            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub UpdateSales()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "Update SES.scheme.and_backlog set sales=" & pubSales & " where product_group='" & pubProductGroup.Trim & _
                                "' and comp_name = '" & pubUser.Trim & "' and order_no = '" & Label2.Text.Trim & "'"
        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateIntoBacklog()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select order_discount, spare1 from SES.scheme.opheadm where order_no='" & Label2.Text.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubSpare = mReader("spare1")
                    pubDiscount = mReader("order_discount")

                    If mReader("spare1") = 0 Then
                        UpdateDiscount1()
                    Else
                        UpdateDiscount2()
                    End If
                End While

            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub UpdateDiscount1()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "Update SES.scheme.and_backlog set orders_booked=" & pubDiscount & _
            " where description='DISCOUNT' and order_no = '" & Label2.Text.Trim & _
            "' and comp_name = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateDiscount2()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "Update SES.scheme.and_backlog set orders_booked=" & pubDiscount * pubSpare & _
            " where description='DISCOUNT' and order_no = '" & Label2.Text.Trim & _
            "' and comp_name = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub GetSalesDiscount()
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select sum(amount*-1) as sales_disc from SES.scheme.prtrm_a where " & _
                        "(project = '" & Label2.Text.Trim & "' or project = '" & Label2.Text.Trim & "/I' or project = '" & _
                            Label2.Text.Trim & "/S') and expense_code='DSC-SALDSC'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If Not IsDBNull(mReader("sales_disc")) Then
                        pubSalesDiscount = mReader("sales_disc")
                        UpdateSalesDiscount()
                    End If
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub UpdateSalesDiscount()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "Update SES.scheme.and_backlog set sales=" & pubSalesDiscount & _
                                " where description='DISCOUNT' and order_no = '" & Label2.Text.Trim & _
                                "' and comp_name = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub CheckIfCurrentAccount()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select project_code from SES.scheme.projects where integration_code1 <> 'STD' and integration_code1 <> 'ASUP' " & _
                    "and integration_code1 <> 'CLSE' and project_type = 'ACAC' and project_code = '" & Label2.Text.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    ProcessCurrentOne()

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try


    End Sub

    Private Sub ProcessCurrentOne()
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select sum(amount) as sumamount from SES.scheme.prtrm_a where substring(project,1,6) = '" & _
                    Label2.Text.Trim & "' and (expense_code = 'TRF-TRFSAL' or expense_code = 'TRF-MNTSAL' or " & _
                    "expense_code = 'TRF-LABSAL' or expense_code = 'TRF-MATSAL' or expense_group = 'ADV' " & _
                    "or expense_group = 'DSC') and remap <> 'y'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If Not IsDBNull(mReader("sumamount")) Then

                        pubCurrentSum = mReader("sumamount")

                        ProcessCurrentTwo()

                        If pubCurrentSum < 0 Then
                            pubCurrentSum = pubCurrentSum * -1
                        End If

                        If pubCurrentOb < 0 Then
                            pubCurrentOb = pubCurrentOb * -1
                        End If

                        If Fix(pubCurrentSum) = Fix(pubCurrentOb) Then
                            UpdateCurrentSales()
                            'set mCurrAccount to true
                            'mCurrAccount = True
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

    Private Sub ProcessCurrentTwo()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select sum(orders_booked) as sumbooked from SES.scheme.and_backlog where order_no = '" & _
                                Label2.Text.Trim & "' and comp_name = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If Not IsDBNull(mReader("sumbooked")) Then
                        pubCurrentOb = mReader("sumbooked")
                    End If
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub UpdateCurrentSales()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update SES.scheme.and_backlog set sales = orders_booked where " & _
                                "comp_name = '" & pubUser.Trim & "' and order_no = '" & Label2.Text.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateFinalBacklog()
        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update SES.scheme.and_backlog set backlog=orders_booked-sales where " & _
                                "comp_name = '" & pubUser.Trim & "' and order_no = '" & Label2.Text.Trim & "'"

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
                sSql = "Select order_type, substring(loadaccess_bwar,8,1) as loadaccess_bwar, substring(loadswitch,8,1) as loadswitches " & _
                        "from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"
            Else
                sSql = "Select substring(loadswitch,8,1) as loadswitches from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"
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

            pubWipCounter = pubWipCounter + 1
            If pubWipCounter = 20 Then
                Panel1.ScrollBars = ScrollBars.Vertical
            End If

            ' add the backlogTotal to the running total variables
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "product_group")) Then

                If DataBinder.Eval(e.Row.DataItem, "product_group").ToString.Trim = "ZZ" Then
                    e.Row.Cells(0).Text = " "
                End If

                If DataBinder.Eval(e.Row.DataItem, "orders_booked") = 0 Then
                    e.Row.Cells(2).Text = " "
                Else
                    pubGridOB = pubGridOB + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "orders_booked")))
                    e.Row.Cells(2).Text = Format(DataBinder.Eval(e.Row.DataItem, "orders_booked"), "##,##0.00;(##,##0.00)")
                End If

                If DataBinder.Eval(e.Row.DataItem, "sales") = 0 Then
                    e.Row.Cells(3).Text = " "
                Else
                    pubGridSales = pubGridSales + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "sales")))
                    e.Row.Cells(3).Text = Format(DataBinder.Eval(e.Row.DataItem, "sales"), "##,##0.00;(##,##0.00)")
                End If

                If DataBinder.Eval(e.Row.DataItem, "backlog") = 0 Then
                    e.Row.Cells(4).Text = " "
                Else
                    pubGridBacklog = pubGridBacklog + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "backlog")))
                    e.Row.Cells(4).Text = Format(DataBinder.Eval(e.Row.DataItem, "backlog"), "##,##0.00;(##,##0.00)")
                End If

            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "Total :"
            ' for the Footer, display the running totals
            e.Row.Cells(2).Text = Format(pubGridOB, "##,##0.00;(##,##0.00)")
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells(3).Text = Format(pubGridSales, "##,##0.00;(##,##0.00)")
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells(4).Text = Format(pubGridBacklog, "##,##0.00;(##,##0.00)")
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right

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
