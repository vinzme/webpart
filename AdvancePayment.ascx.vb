#Region "Imports"

Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Security.Principal
Imports System.Web.UI.Page
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

#End Region

Partial Class AdvancePayment
    Inherits System.Web.UI.UserControl

    Dim pubUser As String
    Dim pubDestin As String
    Dim pubConnStr As String
    Dim pubUserBranch As String
    Dim pubOrderType As String
    Dim pubSales As Double = 0
    Dim pubAdvance As Double = 0
    Dim pubDiscount As Double = 0
    Dim pubExpGroup As String
    Dim pubReference As String
    Dim pubPeriod As String
    Dim pubAnalysis3 As String
    Dim pubAmount As Double = 0

    Dim pubCurrentSum As Double = 0
    Dim pubCurrentOb As Double = 0
    Dim pubCurrAccount As Boolean = False

    Dim pubLoadSwitch As String

    Dim pubGridSales As Double = 0
    Dim pubGridAP As Double = 0
    Dim pubGridDiscount As Double = 0
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
                DeletefromAndAdvance()

                If pubOrderType.Trim = "ACAC" Then
                    CheckIfCurrentAccount()
                End If

                ProcessAdvance()
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

    Private Sub DeletefromAndAdvance()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "delete from SES.scheme.and_advance where compname = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ProcessAdvance()

        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select expense_group, trans_reference, substring(period,1,4)+'/'+substring(period,5,2) as period, " & _
                    "analysis3, amount from SES.scheme.prtrm_a where substring(project,1,6) = '" & Label2.Text.Trim & "' and " & _
                    "(expense_code = 'TRF-TRFSAL' or expense_code = 'TRF-MNTSAL' or expense_code = 'TRF-LABSAL' or " & _
                    "expense_code = 'TRF-MATSAL' or expense_group = 'ADV' or expense_group = 'DSC') and remap <> 'y' order by period"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    pubSales = 0
                    pubAdvance = 0
                    pubDiscount = 0
                    pubExpGroup = Trim(mReader("expense_group"))
                    pubReference = Trim(mReader("trans_reference"))
                    pubPeriod = Trim(mReader("period"))
                    pubAnalysis3 = Trim(mReader("analysis3"))
                    pubAmount = mReader("amount")

                    Select Case pubExpGroup.Trim
                        Case "TRF"
                            pubSales = pubAmount
                        Case "ADV"
                            pubAdvance = pubAmount
                        Case "DSC"
                            pubDiscount = pubAmount
                    End Select

                    If pubCurrAccount = True Then
                        pubSales = pubAdvance
                        pubAdvance = 0
                    End If

                    InsertIntoAdvance()

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub InsertIntoAdvance()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into SES.scheme.and_advance(orderno,reference,period,analysis3,sales,advance," & _
                            "discount,compname) values('" & Label2.Text.Trim & "','" & pubReference.Trim & "','" & _
                            pubPeriod.Trim & "','" & pubAnalysis3.Trim & "'," & pubSales & "," & pubAdvance & _
                            "," & pubDiscount & ",'" & pubUser.Trim & "')"

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
                            'set pubCurrAccount to true
                            pubCurrAccount = True
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

    Private Sub CheckLoadSwitch()
        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            If pubDestin = "I" Then
                sSql = "Select order_type, substring(loadaccess_bwar,10,1) as loadaccess_bwar, substring(loadswitch,10,1) as loadswitches " & _
                        "from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"
            Else
                sSql = "Select substring(loadswitch,10,1) as loadswitches from SES.scheme.and_soweb_users where userid = '" & pubUser.Trim & "'"
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

            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "period")) Then

                If DataBinder.Eval(e.Row.DataItem, "sales") = 0 Then
                    e.Row.Cells(3).Text = " "
                Else
                    pubGridSales = pubGridSales + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "sales")) * -1)
                    e.Row.Cells(3).Text = Format(DataBinder.Eval(e.Row.DataItem, "sales") * -1, "##,##0.00;(##,##0.00)")
                End If

                If DataBinder.Eval(e.Row.DataItem, "advance") = 0 Then
                    e.Row.Cells(4).Text = " "
                Else
                    pubGridAP = pubGridAP + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "advance")))
                    e.Row.Cells(4).Text = Format(DataBinder.Eval(e.Row.DataItem, "advance"), "##,##0.00;(##,##0.00)")
                End If

                If DataBinder.Eval(e.Row.DataItem, "discount") = 0 Then
                    e.Row.Cells(5).Text = " "
                Else
                    pubGridDiscount = pubGridDiscount + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "discount")))
                    e.Row.Cells(5).Text = Format(DataBinder.Eval(e.Row.DataItem, "discount"), "##,##0.00;(##,##0.00)")
                End If

            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Total :"
            ' for the Footer, display the running totals
            e.Row.Cells(3).Text = Format(pubGridSales, "##,##0.00;(##,##0.00)")
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            'e.Row.Cells(5).Font.Size = 8

            e.Row.Cells(4).Text = Format(pubGridAP, "##,##0.00;(##,##0.00)")
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            'e.Row.Cells(6).Font.Size = 8

            e.Row.Cells(5).Text = Format(pubGridDiscount, "##,##0.00;(##,##0.00)")
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right

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
